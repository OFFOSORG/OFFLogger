using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OFF.Logger.Entities;

namespace OFF.LogViewer.Common
{

    internal static class Utils
    {
        #region Static Fields

        private const int WM_SETREDRAW = 11;

        #endregion

        #region Methods

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        public static void SetRedraw(this DataGridView dgv, bool value)
        {
            if (value)
            {
                SendMessage(dgv.Handle, WM_SETREDRAW, true, 0);
                dgv.Refresh();
            }
            else
                SendMessage(dgv.Handle, WM_SETREDRAW, false, 0);
        }

        public static void SetDoubleBuffering(this DataGridView dgv, bool value)
        {
            // Double buffering can make DGV slow in remote desktop
            if (!SystemInformation.TerminalServerSession)
            {
                var dgvType = dgv.GetType();

                var pi = dgvType.GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);

                pi?.SetValue(dgv, value, null);
            }
        }

        public static void BeginInit(this DataGridView dgv)
        {
            ((ISupportInitialize) dgv).BeginInit();
        }

        public static void EndInit(this DataGridView dgv)
        {
            ((ISupportInitialize) dgv).EndInit();
        }

        /// <summary>
        ///     Преобразует значение объекта изменяемой строки в обычную без последнего перехода на новую строку.
        /// </summary>
        /// <param name="text">Изменяемая строка</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToStringWithoutLastNewLine(this StringBuilder text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text), @"Изменяемая строка не существует.");

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

        /// <summary>
        ///     Приостанавливает отрисовку указанного элемента управления (черный фон)
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SuspendPainting(this Control control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control), @"Элемент управления не существует.");

            control.SuspendLayout();
        }

        /// <summary>
        ///     Возобновляет отрисовку указанного элемента управления
        /// </summary>
        /// <param name="control">Элемент управления</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ResumePainting(this Control control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control), @"Элемент управления не существует.");

            control.ResumeLayout();
            control.Refresh();
        }

        #endregion
    }

}