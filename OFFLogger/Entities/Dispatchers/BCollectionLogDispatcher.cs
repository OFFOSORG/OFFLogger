#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OFF.Logger.Entities.Listeners;

#endregion

namespace OFF.Logger.Entities.Dispatchers
{

    /// <summary>
    ///     Диспетчер очереди логирования на <see cref="BlockingCollection{T}" />
    /// </summary>
    public class BCollectionLogDispatcher
    {
        #region Fields

        /// <summary>
        ///     Синхронизатор работы со списком исполнителей логирования.
        /// </summary>
        public readonly object ListenersLocker = new();

        /// <summary>
        ///     Буфер сообщений.
        /// </summary>
        protected readonly BlockingCollection<LogMessage> PendingMessages;

        private List<ILogListener> _listeners;

        /// <summary>
        ///     Источник сигнала отмены взятия элементов из буфера сообщений.
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource;

        /// <summary>
        ///     Поток диспетчера.
        /// </summary>
        protected Thread DispatcherThread;

        #endregion

        #region Constructors

        /// <summary>
        ///     Создает диспетчер очереди логирования.
        /// </summary>
        /// <param name="pendingMessages">Буфер сообщений.</param>
        /// <param name="listeners">Исполнители логирования.</param>
        /// <param name="logTimeout">Таймаут логирования. Если не задано, то используется режим бесконечного таймаута.</param>
        public BCollectionLogDispatcher(BlockingCollection<LogMessage> pendingMessages,
            List<ILogListener> listeners, TimeSpan? logTimeout = null)
        {
            PendingMessages = pendingMessages;
            Listeners = listeners;

            LogTimeout = logTimeout ?? new TimeSpan(0, 0, 0, 0, Timeout.Infinite);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Исполнители логирования.
        /// </summary>
        public List<ILogListener> Listeners
        {
            get => _listeners;
            set
            {
                lock (ListenersLocker)
                    _listeners = value ?? new List<ILogListener>();
            }
        }

        /// <summary>
        ///     Таймаут логирования.
        /// </summary>
        public TimeSpan LogTimeout { get; set; }

        /// <summary>
        ///     Исполнители логирования, которые не успевают вовремя.
        /// </summary>
        public ILogListener[] BadListeners { get; private set; }

        /// <summary>
        ///     Диспетчер завершил свою работу? (завершенный диспетчер нельзя повторно запустить)
        /// </summary>
        public bool IsCompleted { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Запускает цикл диспетчера сообщений.
        /// </summary>
        private void MessageLoop()
        {
            try
            {
                while (PendingMessages.TryTake(out var message, Timeout.Infinite, CancellationTokenSource.Token))
                {
                    try
                    {
                        lock (ListenersLocker)
                        {
                            //Parallel.ForEach(Listeners, listener =>
                            //{
                            //    var cts = new CancellationTokenSource(LogTimeout);
                            //    var task = Task.Factory.StartNew(() => listener?.Log(message), cts.Token);
                            //    task.Wait(cts.Token);

                            //});

                            var count = Listeners.Count;
                            var tasks = new Task[count];

                            try
                            {
                                //Запускаем задачи с таймаутом
                                var cts = new CancellationTokenSource(LogTimeout);

                                for (var i = 0; i < count; i++)
                                {
                                    var listener = Listeners[i];
                                    tasks[i] = Task.Factory.StartNew(() => listener?.Log(message), cts.Token);
                                }

                                //Ожидаем завершения
                                Task.WaitAll(tasks);
                            }
                            catch
                            {
                                // ignored
                            }

                            //Получаем список плохих исполнителей (вышедших по таймауту)
                            var badListeners = new List<ILogListener>(count);

                            for (var i = 0; i < count; i++)
                            {
                                var listener = Listeners[i];

                                if (tasks[i].IsCanceled)
                                    badListeners.Add(listener);
                            }

                            //Если мы готовимся к закрытию, то плохих исполнителей нужно исключить
                            if (PendingMessages.IsAddingCompleted && badListeners.Count > 0)
                            {
                                //Необходимо закрыть плохих исполнителей до исключения
                                foreach (var badListener in badListeners)
                                    badListener.OnClose();

                                Listeners.RemoveAll(badListeners.Contains);
                            }

                            BadListeners = badListeners.ToArray();
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Запускает диспетчер очереди логирования. Важно: Нельзя запустить повторно после остановки.
        /// </summary>
        public void Start()
        {
            //Если диспетчер завершен, то ничего не делаем
            if (IsCompleted)
                return;

            CancellationTokenSource = new CancellationTokenSource();

            DispatcherThread = new Thread(MessageLoop)
            {
                Name = nameof(BCollectionLogDispatcher),
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            };

            DispatcherThread.Start();
        }

        /// <summary>
        ///     Завершает работу с буфером сообщений.
        /// </summary>
        private void CompletePending()
        {
            IsCompleted = true;

            //Запрещаем добавление элементов
            PendingMessages.CompleteAdding();

            //Ждем пока оставшиеся элементы не обработаются
            while (!PendingMessages.IsCompleted) { }

            //Отменяем обработку
            CancellationTokenSource.Cancel();

            //Закрываем исполнителей
            foreach (var listener in Listeners)
                listener.OnClose();
        }

        /// <summary>
        ///     Ожидает окончания потока диспетчера.
        /// </summary>
        /// <param name="timeout">Таймаут.</param>
        /// <returns>Поток завершился?</returns>
        public bool WaitForCompletion(TimeSpan timeout)
        {
            new Thread(CompletePending).Start();

            return DispatcherThread.Join(timeout);
        }

        /// <summary>
        ///     Ожидает окончания потока диспетчера.
        /// </summary>
        public void WaitForCompletion()
        {
            CompletePending();

            DispatcherThread.Join();
        }

        #endregion
    }

}