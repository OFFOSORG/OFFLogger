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
        ///     ��� �������
        /// </summary>
        public static readonly Type TypeObject = typeof(Program);

        /// <summary>
        ///     ���������� ��������� ��������� ���� �������.
        /// </summary>
        private static EventHandler _consoleCtrlHandler;

        #endregion

        #region Constructors

        static Program()
        {
            //�������� ������������ ������
            var assembly = Assembly.GetExecutingAssembly();

            //������� ����������� ��������
            var productAttribute =
                assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0] as
                    AssemblyProductAttribute;

            var fileVersionAttribute =
                assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0] as
                    AssemblyFileVersionAttribute;

            //���� ��������� �� ���������� ��� ��� �����, ��������� ��������
            Name = productAttribute?.Product ?? string.Empty;
            Version = fileVersionAttribute?.Version ?? string.Empty;

            //��������� ��� �������� ���������
            ProcessUid = $"OFFOSORG_{Name}";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     �������� ������������� (��� ������� ���� ������ ���������� ���������)
        /// </summary>
        public static Mutex MutexProgram { get; private set; }

        /// <summary>
        ///     ���������� �������� ���������.
        /// </summary>
        public static string Name { get; }

        /// <summary>
        ///     ���������� ������ ���������.
        /// </summary>
        public static string Version { get; }

        /// <summary>
        ///     ���������� ������ �������� ��������� (� �������).
        /// </summary>
        public static string FullName => $"{Name} {Version}";

        /// <summary>
        ///     ���������� ������������� �������� ���������.
        /// </summary>
        public static string ProcessUid { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     ������������� �� ��������� ��������� ���� �������.
        /// </summary>
        /// <param name="handler">����������</param>
        /// <param name="add">���� ���� �������� ����� true, ����������� ����������; ���� false, ���������� ���������.</param>
        /// <returns></returns>
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        /// <summary>
        ///     ��� ������ ������ ���������? (������ ��������� ���������?)
        /// </summary>
        /// <returns></returns>
        private static bool IsFirstRunApplication()
        {
            var mutexName = ProcessUid;

            try
            {
                //��������� �� ������� �������� � �������
                Mutex.OpenExisting(mutexName);
            }
            catch
            {
                //���� �������� ���������� ������ ������ �������� ���, � ��� ����� �������
                MutexProgram = new Mutex(true, mutexName);

                return true;
            }

            //���� ���������� �� ����, �� ������� � ����� ��������� ��� �������
            return false;
        }

        /// <summary>
        ///     ����������� ���������� � ������������ � ����� ����� ����������.
        /// </summary>
        /// <param name="winFormsMode"></param>
        private static void ApplicationSetup(bool winFormsMode)
        {
            if (winFormsMode)
            {
                //���������� ��� �������������� ���������� � ThreadException
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                //����������� ��� �������������� ����������
                Application.ThreadException += ThreadException;
            }

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;

            //������������ ���������� ���������� �����
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            //������������ �������� ������ ���������
            AppDomain.CurrentDomain.DomainUnload += DomainUnload;

            //������������ �������� ���������
            AppDomain.CurrentDomain.ProcessExit += ProcessExit;

            //������������ ��������� ��������� ���� �������
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
        ///     ������� ����� ����� ��� ����������.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!IsFirstRunApplication())
            {
                Show($@"��������� {Name} ��� ��������! ��������� ������ ��������������...");
                Process.GetCurrentProcess().Kill();
            }

            ////������ ������� ��������� ���������
            //Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            //����������� ��������� ���������� ����������
            ApplicationSetup(true);

            //������� ������, �� ���� �� �����������
            var logger = new OFFLogger(G.PathToLogs, false, TimeSpan.FromSeconds(10));

            //�������� ����������� ������������� � ���� (�� ��������� ���� � �������)
            var fileLogListener = (FileLogListener) logger.LogListeners.First(l => l is FileLogListener);

            //������������� ����� � ������������ �� ������� ����� ����� � 500 ��
            fileLogListener.MaximumFileSizeMode = true;
            fileLogListener.MaximumFileSize = 500 * 1024 * 1024;

            logger.Start();
            logger.Info($"������ ��������� {FullName}", TypeObject);

            Application.Run(new MainForm());
        }

        /// <summary>
        ///     ���������� ����� �� ������ ��������� ���������� � �������������� �������������.
        /// </summary>
        /// <param name="message">������������ �����.</param>
        public static void Show(string message)
        {
            MessageBox.Show(message, @"����������", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //TODO ����������� � OFFUtils
            //var messageBox = new MessageBoxLogger();

            //messageBox.Log(level, message);

            //Console.WriteLine(text);

            //Console.Write("������� ��� �����������...");
            //if (Console.ReadKey().Key != ConsoleKey.Enter)
            //    Console.WriteLine();
        }

        /// <summary>
        ///     ��������� ������ ����������� ���������.
        /// </summary>
        public static void Stop()
        {
            var logger = G.Logger;

            try
            {
                logger?.Info($"���������� ������ ����������� ��������� {FullName}.", TypeObject);
            }
            catch (Exception exc)
            {
                logger?.Error("������ ���������� ������ ����������� ���������.", exc, TypeObject);
            }
            finally
            {
                //��������� ������ �������
                logger?.Stop();
            }
        }

        /// <summary>
        ///     �������� ��������� ������ ����������� ���������.
        /// </summary>
        /// <returns>�������� ����������?</returns>
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
        ///     ��������� ������ ��������� ������ � ������������.
        /// </summary>
        /// <param name="exitCode">��� ���������� ���������. ���� null, �� ������� ��������� ����� ������ �������������.</param>
        public static void Close(int? exitCode = 0)
        {
            var code = -1;
            var withCode = exitCode.HasValue;

            if (withCode)
                code = exitCode.Value;

            G.Logger?.Info($"���������� ������ ��������� {FullName} � ����� {code}.", TypeObject);

            //�������� ������� ����������
            SafeStop();

            //���� ���� ��� ������, �� ��������� ��������� ����� ������ ��������
            if (withCode)
            {
                //�� ����� ���������� ������ ������� �������� ���������
                AppDomain.CurrentDomain.ProcessExit -= ProcessExit;

                //��������� ��������� � �������� �����
                Environment.Exit(code);
            }

            //��������� ������� (��� ���������� ����� -1)
            else
                Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        ///     ������������ ��������� ����������.
        /// </summary>
        private static void FatalException(Exception e)
        {
            G.Logger?.Fatal(e, TypeObject);

            //TODO ����������� � OFFUtils
            //var exceptionDialog = new ExceptionDialog(e) { Logger = G.Logger };

            //if (exceptionDialog.ShowDialog() == DialogResult.Abort)
            Close(e?.HResult);
        }

        /// <summary>
        ///     ������������ ���������� ��������� � ��������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ThreadException(object sender, ThreadExceptionEventArgs e) => FatalException(e.Exception);

        /// <summary>
        ///     ������������ ��������������� ����������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e) =>
            FatalException((Exception) e.ExceptionObject);

        /// <summary>
        ///     ������������ ���������� ���������� ������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            FatalException(e.Exception.InnerException ?? e.Exception);
        }

        /// <summary>
        ///     ������������ �������� ������ ���������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DomainUnload(object sender, EventArgs e)
        {
            G.Logger.Info($"����������� ����� ��������� {FullName}.", TypeObject);
            Close();
        }

        /// <summary>
        ///     ������������ �������� ���������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ProcessExit(object sender, EventArgs e) => Close(Environment.ExitCode);

        /// <summary>
        ///     ������������ ��������� ��������� ���� �������.
        /// </summary>
        /// <param name="ctrlType">��� ������������ ������� ��������� ��������� �������</param>
        /// <returns></returns>
        private static bool OnConsoleCtrlChange(CtrlType ctrlType)
        {
            string text = ctrlType switch
            {
                CtrlType.CTRL_C_EVENT => "��� ������� ������ \"Ctrl + C\". ���������� ��������� ������ ���������.",
                CtrlType.CTRL_BREAK_EVENT =>
                    "��� ������� ������ \"Ctrl + Break\". ���������� ��������� ������ ���������.",
                CtrlType.CTRL_LOGOFF_EVENT => "������������ ����� �� �������. ���������� ��������� ������ ���������.",
                CtrlType.CTRL_SHUTDOWN_EVENT => "������� �����������. ���������� ��������� ������ ���������.",
                CtrlType.CTRL_CLOSE_EVENT => "������������ ������ �������. ���������� ��������� ������ ���������.",
                var _ => "�������������� ��������� �������. ���������� ��������� ������ ���������."
            };

            G.Logger.Info(text, TypeObject);

            Close();

            //�� ���������� ������
            return true;
        }

        #endregion

        #region Nested Types

        /// <summary>
        ///     ��� ������������ ������� ��������� ��������� �������.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private enum CtrlType
        {
            /// <summary>
            ///     ������ CTRL + C ��� ������� ���� � ����������, ���� � �������, ���������������� �������� GenerateConsoleCtrlEvent .
            /// </summary>
            CTRL_C_EVENT = 0,

            /// <summary>
            ///     ������ CTRL + BREAK ��� ������� ���� � ����������, ���� � �������, ���������������� GenerateConsoleCtrlEvent .
            /// </summary>
            CTRL_BREAK_EVENT = 1,

            /// <summary>
            ///     ������, ������� ������� ���������� ���� ���������, ������������ � �������, ����� ������������ ��������� �������
            ///     (���� ����� ������ ������� � ���� ���� ���� �������, ���� ����� ������ � ��������� ������ � ���������� �����).
            /// </summary>
            CTRL_CLOSE_EVENT = 2,

            /// <summary>
            ///     ������, ������� ������� ���������� ���� ��������� �������, ����� ������������ ������� �� �������. ���� ������ ��
            ///     ���������, ����� ������������ ������� �� �������, ������� ������� ������������� ������� ������.
            /// </summary>
            CTRL_LOGOFF_EVENT = 5,

            /// <summary>
            ///     ������, ������� ������� ����������, ����� ������� �����������. ������������� ���������� �� ������������ � ����
            ///     �������, ����� ������� ���������� ���� ������, ������� �� ����� ���� ������ ������ �������� � ���� ��������.
            ///     ������� ����� ����� ����������� �������� ����������� � �������� ����������.
            /// </summary>
            CTRL_SHUTDOWN_EVENT = 6
        }

        /// <summary>
        ///     ������� ��������� ��������� ���� �������.
        /// </summary>
        /// <param name="ctrlType">��� ������������ ������� ��������� ��������� �������.</param>
        /// <returns>
        ///     ���� ������� ������������ ����������� ������, ��� ������ ������� true.
        ///     ���� ��� ���������� false, ������������ ��������� ������� ����������� � ������ ������������ ��� ����� ��������.
        /// </returns>
        private delegate bool EventHandler(CtrlType ctrlType);

        #endregion
    }

}