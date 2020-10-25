namespace OFF.Logger.Entities.Listeners
{

    /// <summary>
    ///     Я - исполнитель логирования
    /// </summary>
    public interface ILogListener
    {
        #region Methods

        /// <summary>
        ///     Логирует сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Log(string message);

        /// <summary>
        ///     Логирует сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        void Log(LogMessage message);

        /// <summary>
        ///     Метод, вызываемый при завершении работы логгера.
        /// </summary>
        void OnClose();

        #endregion
    }

}