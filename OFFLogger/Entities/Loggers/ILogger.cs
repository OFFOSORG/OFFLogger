using System;
using System.Runtime.CompilerServices;
using OFF.Logger.Common;
using OFF.Logger.Enums;

namespace OFF.Logger.Entities.Loggers
{

    /// <summary>
    ///     Я - логгер
    /// </summary>
    public interface ILogger
    {
        #region Properties

        /// <summary>
        ///     Логируемые типы сообщений
        /// </summary>
        LoggingLevel Level { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Trace(string message);

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Trace(Exception exception);

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Trace(string message, Exception exception);

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Trace(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Trace(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает информацию для слежения за участками кода в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Trace(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Debug(string message);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Debug(Exception exception);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Debug(string message, Exception exception);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Debug(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Debug(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает отладочную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Debug(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Info(string message);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Info(Exception exception);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Info(string message, Exception exception);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Info(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Info(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает обычную информацию в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Info(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Warn(string message);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Warn(Exception exception);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Warn(string message, Exception exception);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Warn(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Warn(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает предупреждения и странное поведение в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Warn(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Error(string message);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Error(Exception exception);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Error(string message, Exception exception);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Error(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Error(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает ошибки операций в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Error(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        void Fatal(string message);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        void Fatal(Exception exception);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Fatal(string message, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Fatal(Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        /// <summary>
        ///     Записывает фатальные ошибки программы в лог
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        void Fatal(string message, Exception exception, Type typeObject, [CallerMemberName] string memberName = null);

        #endregion
    }

    public static class LoggerExtensions
    {
        #region Methods

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="message">Сообщение</param>
        public static void Log(this ILogger logger, LoggingLevel level, string message) =>
            Log(logger, level, message, null, null, null);

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="exception">Исключение</param>
        public static void Log(this ILogger logger, LoggingLevel level, Exception exception) =>
            Log(logger, level, null, exception, null, null);

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        public static void Log(this ILogger logger, LoggingLevel level, string message, Exception exception) =>
            Log(logger, level, message, exception, null, null);

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="message">Сообщение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        public static void Log(this ILogger logger, LoggingLevel level, string message, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Log(logger, level, message, null, typeObject, memberName);

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        public static void Log(this ILogger logger, LoggingLevel level, Exception exception, Type typeObject,
            [CallerMemberName] string memberName = null) =>
            Log(logger, level, null, exception, typeObject, memberName);

        /// <summary>
        ///     Записывает информацию заданной важности
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="level">Важность информации</param>
        /// <param name="message">Сообщение</param>
        /// <param name="exception">Исключение</param>
        /// <param name="typeObject">Тип объекта</param>
        /// <param name="memberName">Имя метода или свойства</param>
        public static void Log(this ILogger logger, LoggingLevel level, string message, Exception exception,
            Type typeObject, [CallerMemberName] string memberName = null)
        {
            //Получаем приоритетную важность
            level = level.GetFirstPriorityLevel();

            switch (level)
            {
                case LoggingLevel.Trace:
                    logger.Trace(message, exception, typeObject, memberName);

                    break;

                case LoggingLevel.Debug:
                    logger.Debug(message, exception, typeObject, memberName);

                    break;

                case LoggingLevel.Info:
                    logger.Info(message, exception, typeObject, memberName);

                    break;

                case LoggingLevel.Warn:
                    logger.Warn(message, exception, typeObject, memberName);

                    break;

                case LoggingLevel.Error:
                    logger.Error(message, exception, typeObject, memberName);

                    break;

                case LoggingLevel.Fatal:
                    logger.Fatal(message, exception, typeObject, memberName);

                    break;
            }
        }

        #endregion
    }

}