using System;
using System.Runtime.CompilerServices;
using OFF.Logger.Common;
using OFF.Logger.Enums;

namespace OFF.Logger.Entities.Loggers
{

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