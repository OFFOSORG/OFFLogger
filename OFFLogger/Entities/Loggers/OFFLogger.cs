#region

using System;
using System.Collections.Generic;
using System.IO;
using OFF.Logger.Entities.Dispatchers;
using OFF.Logger.Entities.Listeners;

#endregion

namespace OFF.Logger.Entities.Loggers
{

    /// <summary>
    ///     "Однокнопочный" асинхронный логгер от OFF'а :D
    /// </summary>
    public class OFFLogger : AsyncLogger, IDisposable
    {
        #region Fields

        /// <summary>
        ///     Обработчик очереди логирования
        /// </summary>
        protected BCollectionLogDispatcher Dispatcher;

        /// <summary>
        ///     Блокировщик кода управления работой логгера
        /// </summary>
        protected object Locker = new object();

        #endregion

        #region Constructors

        /// <summary>
        ///     Создает асинхронный логгер
        /// </summary>
        /// <param name="path">Путь к папке.</param>
        /// <param name="autoStart">Автостарт логгера. По умолчанию включено.</param>
        /// <param name="logTimeout">Таймаут логирования (время выделяемое на логирование каждому исполнителю).</param>
        /// <param name="logTimeInterval">Временной интервал лог-файлов</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public OFFLogger(string path = FileLogListener.DefaultFolderName, bool autoStart = true,
            TimeSpan? logTimeout = null, TimeSpan? logTimeInterval = null)
        {
            //Инициализируем исполнителя логирования в файл
            var fileLogListener = new FileLogListener(path, logTimeInterval);

            //Инициализируем исполнителя логирования в консоль
            //var consoleLog = new ConsoleLogListener();

            //Добавляем исполнителей логирования
            var logListeners = new List<ILogListener> {fileLogListener};

            //Инициализируем обработчик очереди логирования
            Dispatcher = new BCollectionLogDispatcher(PendingMessages, logListeners, logTimeout);

            //Стартуем если автостарт включен
            if (autoStart)
                Start();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Логгер работает?
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     Логгер остановлен?
        /// </summary>
        public bool IsStopped { get; private set; }

        /// <summary>
        ///     Синхронизатор работы со списком исполнителей логирования.
        /// </summary>
        public object ListenersLocker => Dispatcher.ListenersLocker;

        /// <summary>
        ///     Исполнители логирования
        /// </summary>
        public List<ILogListener> LogListeners
        {
            get => Dispatcher.Listeners;
            set => Dispatcher.Listeners = value;
        }

        /// <summary>
        ///     Таймаут логирования.
        /// </summary>
        public TimeSpan LogTimeout
        {
            get => Dispatcher.LogTimeout;
            set => Dispatcher.LogTimeout = value;
        }

        /// <summary>
        ///     Диспетчер завершил свою работу? (завершенный диспетчер нельзя повторно запустить)
        /// </summary>
        public bool IsCompleted => Dispatcher.IsCompleted;

        #endregion

        #region Interfaces

        /// <summary>
        ///     При удалении объекта останавливается работа логгера, чтобы сохранить данные.
        ///     При закрытии программы может не справляться, поэтому необходимо прописывать самому
        /// </summary>
        public new void Dispose()
        {
            //Останавливаем работу до уничтожения буфера
            Stop();

            PendingMessages?.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Валидирует логгер (выдает исключение если что-то не так)
        /// </summary>
        private void ValidateLog()
        {
            if (!IsRunning && !IsStopped)
                throw new Exception($"{nameof(OFFLogger)} не запущен!");
        }

        /// <summary>
        ///     Запускает процесс логирования.
        /// </summary>
        /// <returns>Запуск произведен?</returns>
        public bool Start()
        {
            lock (Locker)
            {
                if (IsRunning)
                    return false;

                //Если диспетчер завершен, то необходимо создать новый
                if (Dispatcher.IsCompleted)
                    Dispatcher = new BCollectionLogDispatcher(PendingMessages, LogListeners, LogTimeout);

                //Запускаем работу диспетчера
                Dispatcher.Start();

                //Обновляем флаги
                IsRunning = true;
                IsStopped = false;

                return true;
            }
        }

        /// <summary>
        ///     Останавливает процесс логирования, если он еще не остановлен
        /// </summary>
        /// <returns>Останов произведен?</returns>
        public bool Stop()
        {
            lock (Locker)
            {
                if (IsStopped)
                    return false;

                Dispatcher.WaitForCompletion();

                //Обновляем флаги
                IsRunning = false;
                IsStopped = true;

                return true;
            }
        }

        #endregion
    }

}