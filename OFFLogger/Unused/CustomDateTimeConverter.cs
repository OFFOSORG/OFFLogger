//using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using OFF.Logger.Entities;

//namespace OFF.Logger.Unused
//{

//    /// <summary>
//    ///     Пользовательский конвертер дат в JSON
//    /// </summary>
//    public class CustomDateTimeConverter : JsonConverter<DateTime>
//    {
//        #region Methods

//        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
//            DateTime.Parse(reader.GetString() ?? throw new InvalidOperationException());

//        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
//            writer.WriteStringValue(value.ToString(LogMessage.TimestampFormat));

//        #endregion
//    }

//}
