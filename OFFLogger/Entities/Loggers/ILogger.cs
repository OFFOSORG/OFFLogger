using System;
using System.Runtime.CompilerServices;
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

}