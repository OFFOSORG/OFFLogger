#region

using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

#endregion

namespace OFF.Logger.Entities.Listeners
{

    /// <summary>
    ///     Исполнитель логирования в файл
    /// </summary>
    public class FileLogListener : ILogListener
    {
        #region Static Fields

        /// <summary>
        ///     Имя каталога файл-логов по умолчанию
        /// </summary>
        public const string DefaultFolderName = "Logs";

        /// <summary>
        ///     Минимальный временной интерфал лог-файлов - 1 секунда
        /// </summary>
        public const int MinTimeInterval = 1;

        /// <summary>
        ///     Максимальный временной интерфал лог-файлов - сутки (24 * 60 * 60 секунл)
        /// </summary>
        public const int MaxTimeInterval = 86400;

        #endregion

        #region Fields

        /// <summary>
        ///     Блокировщик кода по работе с файловым потоком
        /// </summary>
        protected readonly object FileStreamLocker = new();

        private string _logFolderPath;

        ///// <summary>
        ///// Последнее количество временных интервалов
        ///// </summary>
        //protected int LastTimeIntervalCount;

        private TimeSpan _logTimeInterval;
        private bool _maximumFileSizeMode;

        /// <summary>
        ///     Поток работы с файлом
        /// </summary>
        protected FileStream FileStream;

        /// <summary>
        ///     Настройки Json-сериализатора
        /// </summary>
        protected JsonSerializerOptions JsonSerializerOptions;

        /// <summary>
        ///     Необходим новый файловый поток?
        /// </summary>
        protected bool NeedNewFileStream;

        /// <summary>
        ///     Номер интервала последнего актуального времени
        /// </summary>
        protected int NIntervalLastActualTime;

        /// <summary>
        ///     Количество ротаций в режиме ограничения по размеру
        /// </summary>
        protected int SizeModeRotationCount;

        #endregion

        #region Constructors

        /// <summary>
        ///     Создает исполнителя логирования в файл
        /// </summary>
        /// <param name="logFolderPath">Путь к каталогу лог-файлов</param>
        /// <param name="logTimeInterval">Временной интервал лог-файлов</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public FileLogListener(string logFolderPath, TimeSpan? logTimeInterval = null)
        {
            //Даты обновления лога не было
            LastDateTime = DateTime.MinValue;

            //Актуального времени не было
            LastActualTime = TimeSpan.Zero;

            //Устанавливаем каталог лог файлов
            LogFolderPath = logFolderPath;

            //Устанавливаем временной интервал по умолчанию - суточный
            LogTimeInterval = logTimeInterval ?? new TimeSpan(24, 0, 0);

            //Устанавливаем по умолчанию максимальный размер файла - 100МБ, на случай будущего включения
            MaximumFileSize = 100 * 1024 * 1024;

            //Используемый в сообщениях формат дата-времени
            var format = LogMessage.TimestampFormat;

            JsonSerializerOptions = new JsonSerializerOptions
            {
                //Перенос по строкам
                WriteIndented = true,

                //Выключаем экранирование спец символов
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

                //Используем пользовательский конвертер дат
                Converters = {new CustomDateTimeConverter(format)}
            };

            //Делаем пробную запись
            Log(string.Empty);
        }

        #endregion

        #region Interfaces

        public void Log(string message)
        {
            //Блокируем работу по файловому потоку
            lock (FileStreamLocker)
            {
                //Файловый поток существует?
                var fileStreamIsExist = FileStream != null;

                //По умолчанию новый поток нужен, если потока не существует
                var needNewFileStream = NeedNewFileStream || !fileStreamIsExist;

                //Запоминаем временной интервал
                var interval = LogTimeIntervalInSeconds;

                //Запоминаем текущее дата-время
                var dateTime = DateTime.Now;
                var time = dateTime.TimeOfDay.TotalSeconds;

                //Если пока поток не нужен, то анализируем временной интервал
                if (!needNewFileStream)
                {
                    //Последнее дата-время
                    var lastDateTime = LastDateTime;

                    //Наступили следующие сутки?
                    var isNextDay = lastDateTime.DayOfYear != dateTime.DayOfYear;

                    //Если последний день обновления логов не совпадает с текущим, то нужен другой файловый поток
                    if (isNextDay)
                        needNewFileStream = true;

                    //Проверяем временной интервал
                    else
                    {
                        var lastTime = LastActualTime.TotalSeconds;

                        //Если текущее время вышло за временной диапазон, то нужен другой файловый поток
                        if (time - lastTime > interval)
                            needNewFileStream = true;
                    }
                }

                var maximumFileSizeMode = MaximumFileSizeMode;

                //Если необходим новый файловый поток, то сбрасываем счетчик ротаций текущего временного интервала
                if (needNewFileStream)
                    SizeModeRotationCount = 0;

                //Если включен режим ограничения по размеру файлов, новый поток по времени не нужен
                else if (maximumFileSizeMode)
                {
                    long fileSize;

                    //Пытаемся считать текущий размер файла
                    try
                    {
                        fileSize = FileStream.Length;
                    }
                    catch
                    {
                        //Закрываем файловый поток
                        FileStream.Close();

                        //Говорим, что файлового потока у нас нет
                        FileStream = null;

                        //Не продолжаем
                        return;
                    }

                    //Если текущий размер файла больше максимального, то нужен другой файловый поток
                    if (fileSize > MaximumFileSize)
                        needNewFileStream = true;
                }

                //Если необходим новый файловый поток
                if (needNewFileStream)
                {
                    //Если старый поток существует, необходимо закрыть его
                    if (fileStreamIsExist)
                        FileStream.Close();

                    //Увеличиваем счетчик ротаций текущего временного интервала
                    if (maximumFileSizeMode)
                        SizeModeRotationCount++;

                    //Устанавливаем дату последнего обновления логов
                    LastDateTime = dateTime;

                    //Номер временного интервала, которому принадлежит актуальное время
                    NIntervalLastActualTime = (int) Math.Truncate(time / interval);

                    //Устанавливаем новое актуальное время
                    LastActualTime = new TimeSpan(0, 0, NIntervalLastActualTime * interval);

                    //Получаем актуальное имя файла
                    var fileName = GetActualLogFileName();

                    //Обновляем путь к файлу логов
                    LogFilePath = Path.Combine(LogFolderPath, fileName);

                    //Если каталога не существует, создаем его
                    var directory = new DirectoryInfo(LogFolderPath);

                    if (!directory.Exists)
                        directory.Create();

                    FileStream = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);

                    //Сброс флага необходимости потока
                    NeedNewFileStream = false;

                    //Идем на рекурсию, чтобы повторно проверить валидность выбранного файла
                    Log(message);

                    //После выхода из рекурсии, нужно выйти
                    return;
                }

                //Пытаемся записать данные
                try
                {
                    //Формирование байт сообщения
                    var bytes = Encoding.UTF8.GetBytes(message + Environment.NewLine);

                    FileStream.Write(bytes, 0, bytes.Length);

                    //Опустошаем внутренние буферы файлового потока в ОС
                    FileStream.Flush(AutoFlush);
                }
                catch
                {
                    //Закрываем файловый поток
                    FileStream.Close();

                    //Говорим, что файлового потока у нас нет
                    FileStream = null;
                }
            }
        }

        public void Log(LogMessage message)
        {
            var text = JsonSerializer.Serialize(message, JsonSerializerOptions);

            Log(text);
        }

        public void OnClose()
        {
            //Закрываем файловый поток, если он существует
            lock (FileStreamLocker)
            {
                if (FileStream != null)
                {
                    //Закрываем файловый поток
                    FileStream.Close();

                    //Говорим, что файлового потока у нас нет
                    FileStream = null;
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Путь к каталогу лог-файлов
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public string LogFolderPath
        {
            get => _logFolderPath;
            set => _logFolderPath = GetFolderPath(value);
        }

        /// <summary>
        ///     Путь лог-файла
        /// </summary>
        public string LogFilePath { get; private set; }

        /// <summary>
        ///     Последняя дата обновления файла лога
        /// </summary>
        public DateTime LastDateTime { get; private set; }

        /// <summary>
        ///     Последнее актуальное время, от которого работает исполнитель
        /// </summary>
        public TimeSpan LastActualTime { get; private set; }

        /// <summary>
        ///     Временной интервал лог-файлов
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimeSpan LogTimeInterval
        {
            get => _logTimeInterval;
            set
            {
                //Всего секунд в задаваемом интервале
                var totalSeconds = value.TotalSeconds;

                //Валидируем значение
                if (totalSeconds < MinTimeInterval || totalSeconds > MaxTimeInterval)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        $"Временной интервал должен принадлежать суточному диапазону секунд [{MinTimeInterval}, {MaxTimeInterval}].");
                }

                if (_logTimeInterval != value)
                {
                    _logTimeInterval = value;

                    //Осущуствляем пересчет в секунды сразу по изменении интервала (вместо постоянного пересчета)
                    LogTimeIntervalInSeconds = (int) Math.Round(value.TotalSeconds);

                    //Необходимо обновить файловый поток
                    NeedNewFileStream = true;
                }
            }
        }

        /// <summary>
        ///     Временной интервал лог-файлов в секундах
        /// </summary>
        public int LogTimeIntervalInSeconds { get; private set; }

        /// <summary>
        ///     Режим ограничения максимального размера файла логов
        /// </summary>
        public bool MaximumFileSizeMode
        {
            get => _maximumFileSizeMode;
            set
            {
                if (_maximumFileSizeMode != value)
                {
                    _maximumFileSizeMode = value;

                    //Необходимо обновить файловый поток
                    NeedNewFileStream = true;
                }
            }
        }

        /// <summary>
        ///     Максимальный размера файла логов в режиме ограничения по размеру (в байтах)
        /// </summary>
        public long MaximumFileSize { get; set; }

        /// <summary>
        ///     Необходимо производить опустошение буферов ОС в файл с каждым сообщением? (иначе ОС обновил данные файла при
        ///     обращении к нему или закрытии потока)
        /// </summary>
        public bool AutoFlush { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Возвращает полный путь к каталогу по заданному пути к каталогу или файлу в нем.
        /// </summary>
        /// <param name="path">Путь к каталогу или к файлу в нем.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static string GetFolderPath(string path = DefaultFolderName)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            //Если пути нет, то используем по умолчанию
            var directoryInfo = new DirectoryInfo(path);

            //Если директория указывается на файл, то переходим на уровень выше
            if (directoryInfo.Extension != string.Empty)
                directoryInfo = directoryInfo.Parent;

            if (directoryInfo == null)
                throw new DirectoryNotFoundException($"Каталог для пути к файлу \"{path}\" не найден.");

            return directoryInfo.FullName;
        }

        /// <summary>
        ///     Пытается получить полный путь к каталогу по заданному пути к каталогу или файлу в нем.
        /// </summary>
        /// <param name="folderPath">Полный путь к каталогу.</param>
        /// <param name="path">Путь к каталогу или к файлу в нем.</param>
        /// <returns>Успешное выполнение?</returns>
        public static bool TryGetFolderPath(out string folderPath, string path = DefaultFolderName)
        {
            try
            {
                folderPath = GetFolderPath(path);

                return true;
            }
            catch
            {
                folderPath = null;

                return false;
            }
        }

        /// <summary>
        ///     Возвращает актуальное имя файла лога.
        /// </summary>
        /// <returns></returns>
        public string GetActualLogFileName()
        {
            //Префикс в начале каждого файла
            const string prefix = "Log";

            //Постфикс для указания ротаций в данном временном интервале
            var postfix = MaximumFileSizeMode ? $"--{SizeModeRotationCount}" : string.Empty;

            //Выбранный временной интервал
            var interval = LogTimeIntervalInSeconds;

            //Интервал суточный?
            var isDayInterval = interval == MaxTimeInterval;

            //Информация о временном диапазоне, по умолчанию не заполняем
            var timeInfo = string.Empty;

            //Если интервал не суточный, то нужно заполнить информацию о временном диапазоне
            if (!isDayInterval)
            {
                //Границы этого интервала
                var begin = NIntervalLastActualTime * interval;
                var end = begin + interval;

                //Если конец вышел из суток, то ограничиваем
                if (end > MaxTimeInterval)
                    end = MaxTimeInterval;

                var timeEnd = new TimeSpan(0, 0, end);

                timeInfo = $"--{LastActualTime:hh'-'mm'-'ss}--{timeEnd:hh'-'mm'-'ss}";
            }

            var fileName = $"{prefix}--{LastDateTime:yyyy-MM-dd}{timeInfo}{postfix}.txt";

            return fileName;
        }

        #endregion
    }

}