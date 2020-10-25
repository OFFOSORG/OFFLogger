using System;
using System.ComponentModel;
using OFF.Logger.Enums;

namespace OFF.Logger.Common
{

    public static class LogUtils
    {
        #region Methods

        /// <summary>
        ///     Возвращает перечисление важности сообщений от заданных параметров
        /// </summary>
        /// <param name="fatal">Принудительное завершение программы</param>
        /// <param name="error">Фатальные ошибки операций, но не программы</param>
        /// <param name="warn">Странное поведение и предупреждения</param>
        /// <param name="info">Обычные уведомления</param>
        /// <param name="debug">Диагностика и отладка</param>
        /// <param name="trace">Слежение за участками кода</param>
        /// <returns>Перечисление важности сообщений</returns>
        public static LoggingLevel GetLoggingLevel(bool fatal = false, bool error = false, bool warn = false,
            bool info = false, bool debug = false, bool trace = false)
        {
            var level = LoggingLevel.None;

            if (fatal)
                level |= LoggingLevel.Fatal;

            if (error)
                level |= LoggingLevel.Error;

            if (warn)
                level |= LoggingLevel.Warn;

            if (info)
                level |= LoggingLevel.Info;

            if (debug)
                level |= LoggingLevel.Debug;

            if (trace)
                level |= LoggingLevel.Trace;

            return level;
        }

        /// <summary>
        ///     Возвращает приоритеты важности сообщений
        /// </summary>
        /// <returns></returns>
        public static LoggingLevel[] GetLoggingPriorities() => new[]
        {
            LoggingLevel.Fatal, LoggingLevel.Error, LoggingLevel.Warn, LoggingLevel.Info, LoggingLevel.Debug,
            LoggingLevel.Trace
        };

        /// <summary>
        ///     Возвращает первую приоритетную важность из заданной
        /// </summary>
        /// <param name="loggingLevel">Важность, в которой будет произведен поиск приоритетной</param>
        /// <param name="descending">Приоритеты по убыванию? (иначе по возрастанию)</param>
        /// <returns></returns>
        public static LoggingLevel GetFirstPriorityLevel(this LoggingLevel loggingLevel, bool descending = true)
        {
            //Получаем массив приоритетов
            var priorities = GetLoggingPriorities();

            //Если не по убыванию, то меняем порядок на обратный
            if (!descending)
                Array.Reverse(priorities);

            //Перебираем все приоритеты
            foreach (var priority in priorities)
            {
                //Если важность приоритетна, то возвращаем ее
                if (loggingLevel.HasFlag(priority))
                    return priority;
            }

            //Важность по умолчанию
            return LoggingLevel.None;
        }

        /// <summary>
        ///     Возвращает установленные приоритеты важности, начиная с заданной
        /// </summary>
        /// <param name="loggingLevel">Минимальная важность</param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static LoggingLevel GetMinimumLevel(LoggingLevel loggingLevel)
        {
            if (!Enum.IsDefined(typeof(LoggingLevel), loggingLevel))
                throw new InvalidEnumArgumentException(nameof(loggingLevel), (int) loggingLevel, typeof(LoggingLevel));

            //Крайние положения возвращаем без изменения
            if (loggingLevel == LoggingLevel.None || loggingLevel == LoggingLevel.All)
                return loggingLevel;

            //Получаем массив приоритетов
            var priorities = GetLoggingPriorities();

            //Определяем индекс необходимого нам приоритета
            var index = Array.FindIndex(priorities, p => loggingLevel.HasFlag(p));

            //Устанавливаем важности до найденного включительно
            var result = LoggingLevel.None;

            for (var i = 0; i <= index; i++)
                result |= priorities[i];

            return result;
        }

        #endregion
    }

}