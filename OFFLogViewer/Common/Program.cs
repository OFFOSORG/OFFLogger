using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OFF.Logger.Entities.Listeners;
using OFF.Logger.Entities.Loggers;
using OFF.Logger.Enums;
using OFF.LogViewer.Forms;

namespace OFF.LogViewer.Common
{

    internal static class Program
    {
        #region Static Fields

        /// <summary>
        ///     Тип объекта
        /// </summary>
        public static readonly Type TypeObject = typeof(Program);

        /// <summary>
        ///     Обработчик изменения состояния типа консоли.
        /// </summary>
        private static EventHandler _consoleCtrlHandler;

        #endregion

        #region Constructors

        static Program()
        {
            //Получаем существующую сборку
            var assembly = Assembly.GetExecutingAssembly();

            //Достаем необходимые атрибуты
            var productAttribute =
                assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0] as
                    AssemblyProductAttribute;

            var fileVersionAttribute =
                assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0] as
                    AssemblyFileVersionAttribute;

            //Если атрибутов не существует или они пусты, заполняем пустотой
            Name = productAttribute?.Product ?? string.Empty;
            Version = fileVersionAttribute?.Version ?? string.Empty;

            //Заполняем имя процесса программы
            ProcessUid = $"OFFOSORG_{Name}";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Примитив синхронизации (для запуска лишь одного экземпляра программы)
        /// </summary>
        public static Mutex MutexProgram { get; private set; }

        /// <summary>
        ///     Возвращает название программы.
        /// </summary>
        public static string Name { get; }

        /// <summary>
        ///     Возвращает версию программы.
        /// </summary>
        public static string Version { get; }

        /// <summary>
        ///     Возвращает полное название программы (с версией).
        /// </summary>
        public static string FullName => $"{Name} {Version}";

        /// <summary>
        ///     Уникальный идентификатор процесса программы.
        /// </summary>
        public static string ProcessUid { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Подписывается на изменение состояния типа консоли.
        /// </summary>
        /// <param name="handler">Обработчик</param>
        /// <param name="add">Если этот параметр равен true, добавляется обработчик; если false, обработчик удаляется.</param>
        /// <returns></returns>
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        /// <summary>
        ///     Это первый запуск программы? (первый экземпляр программы?)
        /// </summary>
        /// <returns></returns>
        private static bool IsFirstRunApplication()
        {
            var mutexName = ProcessUid;

            try
            {
                //Проверяем на наличие мьютекса в системе
                Mutex.OpenExisting(mutexName);
            }
            catch
            {
                //Если получили исключение значит такого мьютекса нет, и его нужно создать
                MutexProgram = new Mutex(true, mutexName);

                return true;
            }

            //Если исключения не было, то процесс с таким мьютексом уже запущен
            return false;
        }

        /// <summary>
        ///     Настраивает приложение в соответствие с типом этого приложения.
        /// </summary>
        /// <param name="winFormsMode"></param>
        private static void ApplicationSetup(bool winFormsMode)
        {
            if (winFormsMode)
            {
                //Направлять все необработанные исключения в ThreadException
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                //Отлавливаем все необработанные исключения
                Application.ThreadException += ThreadException;
            }

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            //Обрабатываем исключения автономных задач
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            //Обрабатываем выгрузку домена программы
            AppDomain.CurrentDomain.DomainUnload += DomainUnload;

            //Обрабатываем закрытие программы
            AppDomain.CurrentDomain.ProcessExit += ProcessExit;

            //Обрабатываем изменение состояния типа консоли
            _consoleCtrlHandler += OnConsoleCtrlChange;
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);

            if (winFormsMode)
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
        }

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!IsFirstRunApplication())
            {
                Show($@"Программа {Name} уже запущена! Повторный запуск проигнорирован...");
                Process.GetCurrentProcess().Kill();
            }

            ////Задаем высокий приоритет программе
            //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            //Настраиваем системные компоненты приложения
            ApplicationSetup(true);

            //Создаем логгер, но пока не запускаемся
            var logger = new OFFLogger(G.PathToLogs, false, TimeSpan.FromSeconds(10));

            //Получаем исполнителя записывающего в файл (по умолчанию есть в логгере)
            var fileLogListener = (FileLogListener) logger.LogListeners.First(l => l is FileLogListener);

            //Устанавливаем режим с ограничением по размеру файла логов в 500 Мб
            fileLogListener.MaximumFileSizeMode = true;
            fileLogListener.MaximumFileSize = 500 * 1024 * 1024;

            logger.Start();
            logger.Info($"Запуск программы {FullName}", TypeObject);

            Application.Run(new MainForm());
        }

        /// <summary>
        ///     Отображает текст на экране различную информацию с подтверждением пользователем.
        /// </summary>
        /// <param name="message">Отображаемый текст.</param>
        public static void Show(string message)
        {
            MessageBox.Show(message, @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //TODO привязаться к OFFUtils
            //var messageBox = new MessageBoxLogger();

            //messageBox.Log(level, message);

            //Console.WriteLine(text);

            //Console.Write("Нажмите для продолжения...");
            //if (Console.ReadKey().Key != ConsoleKey.Enter)
            //    Console.WriteLine();
        }

        /// <summary>
        ///     Завершает работу компонентов программы.
        /// </summary>
        public static void Stop()
        {
            var logger = G.Logger;

            try
            {
                logger?.Info($"Завершение работы компонентов программы {FullName}.", TypeObject);
            }
            catch (Exception exc)
            {
                logger?.Error("Ошибка завершения работы компонентов программы.", exc, TypeObject);
            }
            finally
            {
                //Завершаем работу логгера
                logger?.Stop();
            }
        }

        /// <summary>
        ///     Пытается завершить работы компонентов программы.
        /// </summary>
        /// <returns>Успешное выполнение?</returns>
        public static bool SafeStop()
        {
            try
            {
                Stop();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Завершает работу программы вместе с компонентами.
        /// </summary>
        /// <param name="exitCode">Код завершения программы. Если null, то процесс программы будет закрыт принудительно.</param>
        public static void Close(int? exitCode = 0)
        {
            var code = -1;
            var withCode = exitCode.HasValue;

            if (withCode)
                code = exitCode.Value;

            G.Logger?.Info($"Завершение работы программы {FullName} с кодом {code}.", TypeObject);

            //Пытаемся закрыть компоненты
            SafeStop();

            //Если есть код ошибки, то завершаем программу через запрос закрытия
            if (withCode)
            {
                //Не хотим повторного вызова события закрытия программы
                AppDomain.CurrentDomain.ProcessExit -= ProcessExit;

                //Закрываем программу с заданным кодом
                Environment.Exit(code);
            }

            //Закрываем процесс (код завершения равен -1)
            else
                Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        ///     Обратабывает фатальное исключение.
        /// </summary>
        private static void FatalException(Exception e)
        {
            G.Logger?.Fatal(e, TypeObject);

            //TODO привязаться к OFFUtils
            //var exceptionDialog = new ExceptionDialog(e) { Logger = G.Logger };

            //if (exceptionDialog.ShowDialog() == DialogResult.Abort)
            Close(e?.HResult);
        }

        /// <summary>
        ///     Обрабатывает исключение связанное с потоками.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ThreadException(object sender, ThreadExceptionEventArgs e) => FatalException(e.Exception);

        /// <summary>
        ///     Обрабатывает неперехваченное исключение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e) =>
            FatalException((Exception) e.ExceptionObject);

        /// <summary>
        ///     Обрабатывает исключение автономной задачи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            FatalException(e.Exception.InnerException ?? e.Exception);
        }

        /// <summary>
        ///     Обрабатываем выгрузку домена программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DomainUnload(object sender, EventArgs e)
        {
            G.Logger.Info($"Выгружается домен программы {FullName}.", TypeObject);
            Close();
        }

        /// <summary>
        ///     Обрабатывает закрытие программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ProcessExit(object sender, EventArgs e) => Close(Environment.ExitCode);

        /// <summary>
        ///     Обрабатывает изменение состояния типа консоли.
        /// </summary>
        /// <param name="ctrlType">Тип управляющего сигнала изменения состояния консоли</param>
        /// <returns></returns>
        private static bool OnConsoleCtrlChange(CtrlType ctrlType)
        {
            string text = ctrlType switch
            {
                CtrlType.CTRL_C_EVENT => "Был получен сигнал \"Ctrl + C\". Необходимо завершить работу программы.",
                CtrlType.CTRL_BREAK_EVENT =>
                    "Был получен сигнал \"Ctrl + Break\". Необходимо завершить работу программы.",
                CtrlType.CTRL_LOGOFF_EVENT => "Пользователь вышел из системы. Необходимо завершить работу программы.",
                CtrlType.CTRL_SHUTDOWN_EVENT => "Система выключается. Необходимо завершить работу программы.",
                CtrlType.CTRL_CLOSE_EVENT => "Пользователь закрыл консоль. Необходимо завершить работу программы.",
                var _ => "Неопределенное состояние консоли. Необходимо завершить работу программы."
            };

            G.Logger.Info(text, TypeObject);

            Close();

            //Мы обработали сигнал
            return true;
        }

        #endregion

        #region Nested Types

        /// <summary>
        ///     Тип управляющего сигнала изменения состояния консоли.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private enum CtrlType
        {
            /// <summary>
            ///     Сигнал CTRL + C был получен либо с клавиатуры, либо с сигнала, сгенерированного функцией GenerateConsoleCtrlEvent .
            /// </summary>
            CTRL_C_EVENT = 0,

            /// <summary>
            ///     Сигнал CTRL + BREAK был получен либо с клавиатуры, либо с сигнала, сгенерированного GenerateConsoleCtrlEvent .
            /// </summary>
            CTRL_BREAK_EVENT = 1,

            /// <summary>
            ///     Сигнал, который система отправляет всем процессам, подключенным к консоли, когда пользователь закрывает консоль
            ///     (либо нажав кнопку Закрыть в меню окна окна консоли, либо нажав кнопку « Завершить задачу» в диспетчере задач).
            /// </summary>
            CTRL_CLOSE_EVENT = 2,

            /// <summary>
            ///     Сигнал, который система отправляет всем процессам консоли, когда пользователь выходит из системы. Этот сигнал не
            ///     указывает, какой пользователь выходит из системы, поэтому никаких предположений сделать нельзя.
            /// </summary>
            CTRL_LOGOFF_EVENT = 5,

            /// <summary>
            ///     Сигнал, который система отправляет, когда система выключается. Интерактивные приложения не присутствуют к тому
            ///     времени, когда система отправляет этот сигнал, поэтому он может быть принят только службами в этой ситуации.
            ///     Сервисы также имеют собственный механизм уведомления о событиях выключения.
            /// </summary>
            CTRL_SHUTDOWN_EVENT = 6
        }

        /// <summary>
        ///     Делегат изменения состояния типа консоли.
        /// </summary>
        /// <param name="ctrlType">Тип управляющего сигнала изменения состояния консоли.</param>
        /// <returns>
        ///     Если функция обрабатывает управляющий сигнал, она должна вернуть true.
        ///     Если она возвращает false, используется следующая функция обработчика в списке обработчиков для этого процесса.
        /// </returns>
        private delegate bool EventHandler(CtrlType ctrlType);

        #endregion
    }

}