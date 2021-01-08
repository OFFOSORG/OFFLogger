using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OFF.Logger.Entities
{

    /// <summary>
    ///     Пользовательский конвертер дат в JSON
    /// </summary>
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        #region Fields

        private string _format;

        #endregion

        #region Constructors

        /// <summary>
        ///     Создает пользовательский конвертер дат с форматом по умолчанию ISO 8601.
        /// </summary>
        public CustomDateTimeConverter() => Format = null;

        /// <summary>
        ///     Создает пользовательский конвертер дат с заданным форматом.
        /// </summary>
        /// <param name="format"></param>
        public CustomDateTimeConverter(string format) => Format = format;

        #endregion

        #region Properties

        /// <summary>
        ///     Используемый для записи формат. По умолчанию ISO 8601.
        /// </summary>
        public string Format
        {
            get => _format;
            set => _format = value ?? "O";
        }

        #endregion

        #region Methods

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var text = reader.GetString() ?? throw new NullReferenceException();

            var dateTime = DateTime.ParseExact(text, Format, null);

            //var dateTime = DateTime.Parse(text);

            return dateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var text = value.ToString(Format);

            writer.WriteStringValue(text);
        }

        #endregion
    }

}