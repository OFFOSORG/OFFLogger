#region

using System;

#endregion

namespace OFF.Logger.Enums
{

    /// <summary>
    ///     Важность сообщения
    /// </summary>
    [Flags]
    public enum LoggingLevel
    {
        /// <summary>
        ///     Никакие сообщения
        /// </summary>
        None = 0,

        /// <summary>
        ///     Слежение за участками кода
        /// </summary>
        Trace = 1,

        /// <summary>
        ///     Диагностика и отладка
        /// </summary>
        Debug = 1 << 1,

        /// <summary>
        ///     Обычные уведомления
        /// </summary>
        Info = 1 << 2,

        /// <summary>
        ///     Странное поведение и предупреждения
        /// </summary>
        Warn = 1 << 3,

        /// <summary>
        ///     Ошибки операций
        /// </summary>
        Error = 1 << 4,

        /// <summary>
        ///     Фатальные ошибки программы
        /// </summary>
        Fatal = 1 << 5,

        /// <summary>
        ///     Все виды сообщений
        /// </summary>
        All = Trace | Debug | Info | Warn | Error | Fatal
    }

}