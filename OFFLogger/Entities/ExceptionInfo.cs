using System;
using System.Text.Json.Serialization;
using OFF.Logger.Common;

namespace OFF.Logger.Entities
{

    /// <summary>
    ///     Информация об исключении
    /// </summary>
    public class ExceptionInfo
    {
        #region Constructors

        /// <summary>
        ///     Создает объект информации об исключении
        /// </summary>
        /// <param name="message">Сообщение, описывающее исключение.</param>
        /// <param name="stackTrace">Строковое представление непосредственных кадров в стеке вызова.</param>
        /// <param name="innerException">Вложенное исключение.</param>
        [JsonConstructor]
        public ExceptionInfo(string message = null, string stackTrace = null, ExceptionInfo innerException = null)
        {
            Message = message;
            StackTrace = stackTrace;
            InnerException = innerException;
        }

        /// <summary>
        ///     Создает объект информации об исключении
        /// </summary>
        /// <param name="message">Сообщение, описывающее исключение.</param>
        /// <param name="innerException">Вложенное исключение.</param>
        public ExceptionInfo(string message = null, ExceptionInfo innerException = null)
        {
            Message = message;
            InnerException = innerException;
        }

        /// <summary>
        ///     Создает объект информации об исключении
        /// </summary>
        /// <param name="exception">Исключение, по которому будет собрана информация</param>
        public ExceptionInfo(Exception exception) => ReadException(exception);

        #endregion

        #region Properties

        /// <summary>
        ///     Сообщение, описывающее исключение.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; protected set; }

        /// <summary>
        ///     Строковое представление непосредственных кадров в стеке вызова.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string StackTrace { get; protected set; }

        /// <summary>
        ///     Вложенное исключение.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ExceptionInfo InnerException { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Считывает информацию о заданном исключении
        /// </summary>
        /// <param name="exception"></param>
        public void ReadException(Exception exception)
        {
            var info = this;

            while (exception != null)
            {
                info.Message = exception.Message;
                info.StackTrace = exception.StackTrace;

                var innerException = exception.InnerException;

                //Если вложенное исключение существует, то переходим к нему
                if (innerException != null)
                {
                    info.InnerException = new ExceptionInfo(innerException);

                    exception = innerException;
                    info = info.InnerException;

                    continue;
                }

                //Выходим из цикла
                break;
            }
        }

        public override string ToString() => this.GetDetails();

        #endregion
    }

}