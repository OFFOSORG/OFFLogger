#region

using System;
using System.Text;
using System.Text.Json.Serialization;
using OFF.Logger.Common;
using OFF.Logger.Enums;

#endregion

namespace OFF.Logger.Entities
{

    /// <summary>
    ///     Логируемое сообщение
    /// </summary>
    public class LogMessage
    {
        #region Static Fields

        private static string _timestampFormat;

        #endregion

        #region Constructors

        /// <summary>
        ///     Сбрасываем формат отметки времени.
        /// </summary>
        static LogMessage() => TimestampFormat = null;

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        [JsonConstructor]
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, string text, ExceptionInfo exception,
            string typeObject, string memberName)
        {
            Timestamp = timestamp;
            Level = level;
            ThreadId = threadId;
            Text = text;
            Exception = exception;
            TypeObject = typeObject;
            MemberName = memberName;
        }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, string text, Exception exception,
            Type typeObject, string memberName)
        {
            Timestamp = timestamp;
            Level = level;
            ThreadId = threadId;
            Text = text;

            if (exception != null)
                Exception = new ExceptionInfo(exception);

            TypeObject = typeObject?.FullName;
            MemberName = memberName;
        }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="text">Текст сообщения</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, string text) :
            this(timestamp, level, threadId, text, (ExceptionInfo) null, null, null) { }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, Exception exception) :
            this(timestamp, level, threadId, null, exception, null, null) { }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, string text, Exception exception) :
            this(timestamp, level, threadId, text, exception, null, null) { }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, string text, Type typeObject,
            string memberName) :
            this(timestamp, level, threadId, text, null, typeObject, memberName) { }

        /// <summary>
        ///     Создает логируемое сообщение
        /// </summary>
        /// <param name="timestamp">Отметка времени</param>
        /// <param name="level">Важность сообщения</param>
        /// <param name="threadId">Идентификатор потока, в котором было послано сообщение</param>
        /// <param name="exception">Исключение, сопровождаемое вместе с сообщением</param>
        /// <param name="typeObject">Тип объекта, в котором было послано сообщение</param>
        /// <param name="memberName">Название метода или свойства, которое послало сообщение</param>
        public LogMessage(DateTime timestamp, LoggingLevel level, int threadId, Exception exception, Type typeObject,
            string memberName) :
            this(timestamp, level, threadId, null, exception, typeObject, memberName) { }

        #endregion

        #region Properties

        /// <summary>
        ///     Формат отметки времени. По умолчанию ISO 8601.
        /// </summary>
        public static string TimestampFormat
        {
            get => _timestampFormat;
            set => _timestampFormat = value ?? "O";
        }

        /// <summary>
        ///     Отметка времени
        /// </summary>
        //[JsonConverter(typeof(CustomDateTimeConverter))]
        [JsonPropertyName("Time")]
        public DateTime Timestamp { get; }

        /// <summary>
        ///     Важность сообщения
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("Level")]
        public LoggingLevel Level { get; }

        /// <summary>
        ///     Идентификатор потока, в котором было послано сообщение
        /// </summary>
        [JsonPropertyName("TID")]
        public int ThreadId { get; }

        /// <summary>
        ///     Полное имя типа объекта, в котором было послано сообщение
        /// </summary>
        [JsonPropertyName("Type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TypeObject { get; }

        /// <summary>
        ///     Название метода или свойства, которое послало сообщение
        /// </summary>
        [JsonPropertyName("Member")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string MemberName { get; }

        /// <summary>
        ///     Текст сообщения
        /// </summary>
        [JsonPropertyName("Text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Text { get; }

        /// <summary>
        ///     Исключение, сопровождаемое вместе с сообщением
        /// </summary>
        [JsonPropertyName("Exc")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ExceptionInfo Exception { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Возвращает логируемое сообщение в строчном виде
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var text = new StringBuilder();
            text.AppendLine($"[Level: {Level}]\t[TID: {ThreadId}]\t[Time: {Timestamp.ToString(TimestampFormat)}]");

            //Если тип объекта был заполнен, логируем его
            if (TypeObject != null)
                text.AppendLine($"[Type: {TypeObject}]");

            //Если название метода или свойства было заполнено, логируем его
            if (!string.IsNullOrEmpty(MemberName))
                text.AppendLine($"[Member: {MemberName}]");

            //Если сообщение было заполнено, логируем его
            if (!string.IsNullOrEmpty(Text))
                text.AppendLine($"[Text: {Text}]");

            //Если исключение было заполнено, логируем его
            if (Exception != null)
                text.AppendLine($"[Exception: {Exception.GetDetails()}]");

            return text.ToString();
        }

        #endregion
    }

}