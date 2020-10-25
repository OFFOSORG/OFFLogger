#region

using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using OFF.Logger.Enums;

#endregion

namespace OFF.Logger.Entities.Loggers
{

    /// <summary>
    ///     Асинхронный логгер
    /// </summary>
    public class AsyncLogger : ILogger, IDisposable
    {
        #region Constructors

        /// <summary>
        ///     Создает асинхронный логгер
        /// </summary>
        public AsyncLogger()
        {
            PendingMessages = new BlockingCollection<LogMessage>();
            Level = LoggingLevel.All;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Буфер сообщений (очередь логирования)
        /// </summary>
        public BlockingCollection<LogMessage> PendingMessages { get; private set; }

        public LoggingLevel Level { get; set; }

        #endregion

        #region Interfaces

        public void Dispose()
        {
            if (PendingMessages != null)
            {
                PendingMessages.Dispose();
                PendingMessages = null;
            }
        }

        public void Trace(string message) => Push(LoggingLevel.Trace, message);

        public void Trace(Exception exception) => Push(LoggingLevel.Trace, exception);

        public void Trace(string message, Exception exception) => Push(LoggingLevel.Trace, message, exception);

        public void Trace(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Trace, message, typeObject, memberName);

        public void Trace(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Trace, exception, typeObject, memberName);

        public void Trace(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Trace, message, exception, typeObject, memberName);

        public void Debug(string message) => Push(LoggingLevel.Debug, message);

        public void Debug(Exception exception) => Push(LoggingLevel.Debug, exception);

        public void Debug(string message, Exception exception) => Push(LoggingLevel.Debug, message, exception);

        public void Debug(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Debug, message, typeObject, memberName);

        public void Debug(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Debug, exception, typeObject, memberName);

        public void Debug(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Debug, message, exception, typeObject, memberName);

        public void Info(string message) => Push(LoggingLevel.Info, message);

        public void Info(Exception exception) => Push(LoggingLevel.Info, exception);

        public void Info(string message, Exception exception) => Push(LoggingLevel.Info, message, exception);

        public void Info(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Info, message, typeObject, memberName);

        public void Info(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Info, exception, typeObject, memberName);

        public void Info(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Info, message, exception, typeObject, memberName);

        public void Warn(string message) => Push(LoggingLevel.Warn, message);

        public void Warn(Exception exception) => Push(LoggingLevel.Warn, exception);

        public void Warn(string message, Exception exception) => Push(LoggingLevel.Warn, message, exception);

        public void Warn(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Warn, message, typeObject, memberName);

        public void Warn(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Warn, exception, typeObject, memberName);

        public void Warn(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Warn, message, exception, typeObject, memberName);

        public void Error(string message) => Push(LoggingLevel.Error, message);

        public void Error(Exception exception) => Push(LoggingLevel.Error, exception);

        public void Error(string message, Exception exception) => Push(LoggingLevel.Error, message, exception);

        public void Error(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Error, message, typeObject, memberName);

        public void Error(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Error, exception, typeObject, memberName);

        public void Error(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Error, message, exception, typeObject, memberName);

        public void Fatal(string message) => Push(LoggingLevel.Fatal, message);

        public void Fatal(Exception exception) => Push(LoggingLevel.Fatal, exception);

        public void Fatal(string message, Exception exception) => Push(LoggingLevel.Fatal, message, exception);

        public void Fatal(string message, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Fatal, message, typeObject, memberName);

        public void Fatal(Exception exception, Type typeObject, [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Fatal, exception, typeObject, memberName);

        public void Fatal(string message, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Push(LoggingLevel.Fatal, message, exception, typeObject, memberName);

        #endregion

        #region Methods

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        private void Push(LoggingLevel level, string message, Exception exception, Type typeObject, string memberName)
        {
            //Если логировать этот тип сообшения не нужно, то выходим
            if (!Level.HasFlag(level))
                return;

            var pendingMessages = PendingMessages;

            //Если буфера не существует, то выходим
            if (pendingMessages == null)
                return;

            var timestamp = DateTime.Now;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            //Пытаемся добавить
            try
            {
                //Если мы можем добавить в очередь сообщения, то добавляем
                if (!pendingMessages.IsAddingCompleted)
                {
                    pendingMessages.TryAdd(new LogMessage(timestamp, level, threadId, message, exception, typeObject,
                        memberName));
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="message">Текст сообщения</param>
        private void Push(LoggingLevel level, string message) =>
            Push(level, message, null, null, null);

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        private void Push(LoggingLevel level, Exception exception) =>
            Push(level, null, exception, null, null);

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        private void Push(LoggingLevel level, string message, Exception exception) =>
            Push(level, message, exception, null, null);

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        private void Push(LoggingLevel level, string message, Type typeObject, string memberName) =>
            Push(level, message, null, typeObject, memberName);

        /// <summary>
        ///     Вставляет сообщение в буфер сообщений
        /// </summary>
        /// <param name="level">Важность сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        private void Push(LoggingLevel level, Exception exception, Type typeObject, string memberName) =>
            Push(level, null, exception, typeObject, memberName);

        #endregion
    }

}