using System;
using System.Text;
using OFF.Logger.Entities;

namespace OFF.Logger.Common
{

    internal static class U
    {
        #region Methods

        /// <summary>
        ///     Преобразует значение объекта изменяемой строки в обычную без последнего перехода на новую строку.
        /// </summary>
        /// <param name="text">Изменяемая строка</param>
        /// <returns></returns>
        public static string ToStringWithoutLastNewLine(this StringBuilder text)
        {
            var result = text.ToString();

            var trimmed = new[] {'\n', '\r'};

            result = result.TrimEnd(trimmed);

            return result;
        }

        /// <summary>
        ///     Возвращает детальную информацию об исключении в удобно-строчном виде.
        /// </summary>
        /// <param name="exc">Исключение</param>
        /// <returns>Текст с детальной информацией об исключении</returns>
        public static string GetDetails(this Exception exc)
        {
            //Вызываем исключение на несуществующее исключение
            if (exc == null)
                throw new ArgumentNullException(nameof(exc));

            var result = new StringBuilder();

            //Пока есть описываемое исключение
            while (exc != null)
            {
                //Добавляем сообщение и стек вызова
                result.AppendLine(exc.Message);
                result.AppendLine(exc.StackTrace);

                //Выбираем вложенное исключение
                exc = exc.InnerException;
            }

            return result.ToStringWithoutLastNewLine();
        }

        /// <summary>
        ///     Возвращает детальную информацию об исключении в удобно-строчном виде.
        /// </summary>
        /// <param name="exc">Исключение</param>
        /// <returns>Текст с детальной информацией об исключении</returns>
        public static string GetDetails(this ExceptionInfo exc)
        {
            //Вызываем исключение на несуществующее исключение
            if (exc == null)
                throw new ArgumentNullException(nameof(exc));

            var result = new StringBuilder();

            //Пока есть описываемое исключение
            while (exc != null)
            {
                //Добавляем сообщение и стек вызова
                result.AppendLine(exc.Message);
                result.AppendLine(exc.StackTrace);

                //Выбираем вложенное исключение
                exc = exc.InnerException;
            }

            return result.ToStringWithoutLastNewLine();
        }

        #endregion
    }

}