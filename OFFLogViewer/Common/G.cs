using System.IO;
using OFF.Logger.Entities.Loggers;

namespace OFF.LogViewer.Common
{

    public static class G
    {
        #region Static Fields

        /// <summary>
        ///     Папка логов
        /// </summary>
        public const string LogsFolderName = "Logs";

        #endregion

        #region Constructors

        static G()
        {
            //Устанавливаем определяем папку с логами
            var dir = new DirectoryInfo(LogsFolderName);
            PathToLogs = dir.FullName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Логгер
        /// </summary>
        public static OFFLogger Logger { get; set; }

        /// <summary>
        ///     Путь к логам
        /// </summary>
        public static string PathToLogs { get; }

        #endregion

        ///// <summary>
        ///// Текущие сообщения логов
        ///// </summary>
        //public static List<LogMessage> LogMessages { get; set; }
    }

}