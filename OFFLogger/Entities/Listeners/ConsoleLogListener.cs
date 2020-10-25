#region

using System;

#endregion

namespace OFF.Logger.Entities.Listeners
{

    /// <summary>
    ///     Исполнитель логирования в консоль
    /// </summary>
    public class ConsoleLogListener : ILogListener
    {
        #region Constructors

        /// <summary>
        ///     Создает исполнителя логирования в консоль
        /// </summary>
        public ConsoleLogListener()
        {
            //Делаем пробную запись
            Log(string.Empty);
        }

        #endregion

        #region Interfaces

        /// <summary>
        ///     Записывает сообщение в консоль
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        ///     Записывает сообщение в консоль
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Log(LogMessage message) => Log(message.ToString());

        public void OnClose() { }

        #endregion
    }

}