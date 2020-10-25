using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using ClosedXML.Excel;
using OFF.Logger.Entities;
using OFF.Logger.Enums;
using OFF.LogViewer.Common;

namespace OFF.LogViewer.Forms
{

    public partial class MainForm : Form
    {
        #region Static Fields

        /// <summary>
        ///     "Оператор не используется"
        /// </summary>
        private const string NoOperator = "Нет";

        /// <summary>
        ///     Имя столбца важности сообщения.
        /// </summary>
        public const string LevelColumnName = nameof(LogMessage.Level);

        /// <summary>
        ///     Имя столбца идентификатора потока.
        /// </summary>
        public const string ThreadIdColumnName = nameof(LogMessage.ThreadId);

        /// <summary>
        ///     Имя столбца отметки времени.
        /// </summary>
        public const string TimestampColumnName = nameof(LogMessage.Timestamp);

        /// <summary>
        ///     Имя столбца полного имени объекта.
        /// </summary>
        public const string TypeObjectColumnName = nameof(LogMessage.TypeObject);

        /// <summary>
        ///     Имя столбца названия метода или свойства.
        /// </summary>
        public const string MemberNameColumnName = nameof(LogMessage.MemberName);

        /// <summary>
        ///     Имя столбца текста сообщения.
        /// </summary>
        public const string TextColumnName = nameof(LogMessage.Text);

        /// <summary>
        ///     Имя столбца исключения.
        /// </summary>
        public const string ExceptionColumnName = nameof(LogMessage.Exception);

        /// <summary>
        ///     Имя столбца информации об исключении.
        /// </summary>
        public const string ExceptionInfoColumnName = nameof(ExceptionInfo);

        /// <summary>
        ///     Тип объекта
        /// </summary>
        public static readonly Type TypeObject = typeof(MainForm);

        #endregion

        #region Fields

        //private readonly BindingListView<LogMessage> _view;

        private readonly DataTable _dataTable;

        /// <summary>
        ///     Операторы над числами
        /// </summary>
        private readonly string[] _numberOperators = {NoOperator, "=", "<>", "<", "<=", ">=", ">"};

        /// <summary>
        ///     Диалоговое окно для загрузки файлов
        /// </summary>
        private readonly OpenFileDialog _ofd;

        /// <summary>
        ///     Диалоговое окно для сохранения файлов
        /// </summary>
        private readonly SaveFileDialog _sfd;

        /// <summary>
        ///     Операторы над строками
        /// </summary>
        private readonly string[] _stringOperators = {NoOperator, "=", "<>"};

        /// <summary>
        ///     Эквиваленты операторов на SQL для строк
        /// </summary>
        private readonly Dictionary<string, string> _stringSqlOperators = new Dictionary<string, string>
        {
            {"=", "LIKE"},
            {"<>", "NOT LIKE"}
        };

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            _ofd = new OpenFileDialog
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = @"Text Files(*.txt)|*.txt",
                ShowReadOnly = false

                //RestoreDirectory = true,
                //InitialDirectory = Application.StartupPath
            };

            _sfd = new SaveFileDialog
            {
                AddExtension = true,
                Filter = @"Excel Files(*.xlsx)|*.xlsx",
                OverwritePrompt = true

                //RestoreDirectory = true,
            };

            //var logMessages = new List<LogMessage>();
            //G.LogMessages = logMessages;
            //_view = new BindingListView<LogMessage>(logMessages);
            //_view.AddingNew += ViewOnAddingNew;

            //_view.ApplyFilter(new IncludeAllItemFilter<LogMessage>());

            //Внутренняя таблица
            _dataTable = new DataTable();

            dgv.ColumnHeadersVisible = true;
            dgv.RowHeadersVisible = true;

            //Авторазмер видимых строк
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            //dgv.RowTemplate.Height = 40;

            //Включаем двойной буфер
            dgv.SetDoubleBuffering(true);

            //Отключаем автатическое создание столбцов для типа сообшения
            dgv.AutoGenerateColumns = false;

            var name = LevelColumnName;
            _dataTable.Columns.Add(name, typeof(LoggingLevel));
            var textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = name;
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = ThreadIdColumnName;
            _dataTable.Columns.Add(name, typeof(int));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = @"TID";
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = TimestampColumnName;
            _dataTable.Columns.Add(name, typeof(DateTime));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = @"Time";
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Format = "yyyy.MM.dd\nHH:mm:ss.FFFK";
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = TypeObjectColumnName;
            _dataTable.Columns.Add(name, typeof(string));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = @"Type";
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            textBoxColumn.FillWeight = 25;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = MemberNameColumnName;
            _dataTable.Columns.Add(name, typeof(string));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = @"Member";
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            textBoxColumn.FillWeight = 25;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = TextColumnName;
            _dataTable.Columns.Add(name, typeof(string));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = name;
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            textBoxColumn.FillWeight = 100;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            name = ExceptionColumnName;
            _dataTable.Columns.Add(name, typeof(string));
            var buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = name;
            buttonColumn.HeaderText = @"Exc";
            buttonColumn.DataPropertyName = name;
            buttonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            buttonColumn.FlatStyle = FlatStyle.System;
            buttonColumn.UseColumnTextForButtonValue = false;
            buttonColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            buttonColumn.FillWeight = 50;

            //column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns.Add(buttonColumn);

            name = ExceptionInfoColumnName;
            _dataTable.Columns.Add(name, typeof(string));
            textBoxColumn = new DataGridViewTextBoxColumn();
            textBoxColumn.Name = name;
            textBoxColumn.HeaderText = @"ExcInfo";
            textBoxColumn.DataPropertyName = name;
            textBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            textBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            textBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            textBoxColumn.FillWeight = 50;
            textBoxColumn.Visible = false;

            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgv.Columns.Add(textBoxColumn);

            //dgv.DataSource = _dataTable;

            //Заполнение выборов
            var cbLevelValues = Enum.GetNames(typeof(LoggingLevel));

            cbLevel1.DataSource = _numberOperators.Clone();
            cbLevel2.DataSource = _numberOperators.Clone();
            cbLevelValue1.DataSource = cbLevelValues.Clone();
            cbLevelValue2.DataSource = cbLevelValues.Clone();
            cbThreadId1.DataSource = _numberOperators.Clone();
            cbThreadId2.DataSource = _numberOperators.Clone();
            cbTimestamp1.DataSource = _numberOperators.Clone();
            cbTimestamp2.DataSource = _numberOperators.Clone();
            cbText1.DataSource = _stringOperators.Clone();
            cbText2.DataSource = _stringOperators.Clone();
            cbTypeObject1.DataSource = _stringOperators.Clone();
            cbTypeObject2.DataSource = _stringOperators.Clone();
            cbMemberName1.DataSource = _stringOperators.Clone();
            cbMemberName2.DataSource = _stringOperators.Clone();
            cbException1.DataSource = _stringOperators.Clone();
            cbException2.DataSource = _stringOperators.Clone();
            cbExceptionInfo1.DataSource = _stringOperators.Clone();
            cbExceptionInfo2.DataSource = _stringOperators.Clone();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Количество считанных сообщений
        /// </summary>
        public int GoodCount { get; private set; }

        /// <summary>
        ///     Количество несчитанных сообщений
        /// </summary>
        public int BadCount { get; private set; }

        /// <summary>
        ///     Текущее количество сообщений в таблице
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Methods

        private void bOpen_Click(object sender, EventArgs e)
        {
            var logger = G.Logger;

            logger?.Info($"Пользователь нажал на кнопку \"{((Button) sender).Text}\"", TypeObject);

            _ofd.Multiselect = false;

            if (_ofd.ShowDialog() != DialogResult.OK)
                return;

            var path = _ofd.FileName;

            logger?.Info($"Пользователь выбрал файл:\n{path}", TypeObject);

            var b = (Button) sender;
            b.Enabled = false;

            //Блокируем фильтрацию
            bFilter.Enabled = false;

            //Запрещаем сохранение в Эксель
            bExportToExcel.Enabled = false;

            ////Выключаем отрисовку таблицы для ускорения
            //SendMessage(dgv.Handle, WM_SETREDRAW, false, 0);

            //Снимаем привязку для ускорения
            dgv.DataSource = null;

            //dgv.Columns.Clear();
            //dgv.Rows.Clear();

            //Очищаем таблицу от старых данных
            _dataTable.Clear();

            //Запускаем чтение файла
            bw.RunWorkerAsync(path);

            //Ожидаем завершения чтения
            while (bw.IsBusy)
                Application.DoEvents();

            dgv.BeginInit();
            dgv.SetRedraw(false);

            //Привязываемся к данным
            dgv.DataSource = _dataTable;

            dgv.EndInit();
            dgv.SetRedraw(true);

            //Обновляем состояния
            tsslGoodCountValue.Text = GoodCount.ToString();
            tsslBadCountValue.Text = BadCount.ToString();
            Count = _dataTable.DefaultView.Count;
            tsslCountValue.Text = Count.ToString();

            var rows = _dataTable.Rows;
            var nRows = rows.Count;

            //Если элементы в таблице есть, то находим крайние
            if (nRows != 0)
            {
                var minRow = rows[0];
                var minRowValue = minRow[TimestampColumnName];
                var minDateTime = (DateTime) minRowValue;
                dtpTimestamp1.Value = minDateTime;

                var maxRow = rows[nRows - 1];
                var maxRowValue = maxRow[TimestampColumnName];
                var maxDateTime = (DateTime) maxRowValue;
                dtpTimestamp2.Value = maxDateTime;
            }

            ////Включаем отрисовку таблицы
            //SendMessage(dgv.Handle, WM_SETREDRAW, true, 0);
            //dgv.Refresh();

            //if (logger != null)
            //    logger.LogTimeout = new TimeSpan(0, 0, 0);

            //for (int i = 0; i < 10000; i++)
            //{
            //    G.Logger.Info($"test_{i}");
            //}

            b.Enabled = true;

            //Разрешаем фильтрацию
            bFilter.Enabled = true;

            //Разрешает сохранение в Эксель
            bExportToExcel.Enabled = true;

            //dgv.DataSource = logMessages;

            //dgv.Update();
            //dgv.DataSource = logMessages;
            //logMessages.ForEach(AddMessage);
        }

        public void AddMessage(LogMessage logMessage)
        {
            var level = logMessage.Level /*.ToString()*/;
            var threadId = logMessage.ThreadId /*.ToString()*/;
            var timestamp = logMessage.Timestamp /*.ToString(LogMessage.TimestampFormat)*/;
            var typeObject = logMessage.TypeObject;
            var memberName = logMessage.MemberName;
            var text = logMessage.Text;
            var e = logMessage.Exception;
            var exceptionInfo = e?.GetDetails();
            var exception = e?.Message;

            _dataTable.Rows.Add(level, threadId, timestamp, typeObject, memberName, text, exception, exceptionInfo);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var path = e.Argument?.ToString();

            if (string.IsNullOrEmpty(path))
                return;

            //var list = G.LogMessages;

            //var table = _dataTable;
            GoodCount = 0;
            BadCount = 0;

            //Счетчик строк
            var count = 0;

            //Номер строки начала сообщения
            var begin = 1;

            //using (var file = MemoryMappedFile.CreateFromFile(path, FileMode.Open))
            //using (var viewStream = file.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
            //using (var bufferedStream = new BufferedStream(viewStream))

            using var viewStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(viewStream);

            //Буферная строка
            var sb = new StringBuilder();

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                count++;

                sb.AppendLine(line);

                if (line.Length == 0 || line[0] != '}')
                    continue;

                try
                {
                    AddMessage(JsonSerializer.Deserialize<LogMessage>(sb.ToString()));
                    GoodCount++;
                }
                catch (Exception exc)
                {
                    G.Logger?.Error($"Не удалось считать сообщение в файле между строк {begin} и {count}.", exc,
                        TypeObject);

                    BadCount++;
                }

                //Очищаем буферную строку
                sb.Clear();

                //Начало - следующая после конца строка
                begin = count + 1;
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var rowIndex = e.RowIndex;
            var columnIndex = e.ColumnIndex;

            //Если нажата не кнопка, то ничего не делаем
            if (columnIndex != 6 || rowIndex == -1)
                return;

            if (!string.IsNullOrEmpty(dgv.CurrentCell?.Value?.ToString()))
                MessageBox.Show(dgv.Rows[rowIndex].Cells[7].Value.ToString());
        }

        /// <summary>
        ///     Возвращает часть сравнения для строк
        /// </summary>
        /// <param name="header">Заголовок</param>
        /// <param name="sqlOperator">Оператор SQL</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        private static string GetSqlStringPart(string header, string sqlOperator, string value) =>
            $"[{header}] {sqlOperator} '%{value}%'";

        /// <summary>
        ///     Возвращает часть сравнения для целых чисел
        /// </summary>
        /// <param name="header">Заголовок</param>
        /// <param name="sqlOperator">Оператор SQL</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        private static string GetSqlStringPart(string header, string sqlOperator, int value) =>
            $"{header} {sqlOperator} {value}";

        /// <summary>
        ///     Возвращает часть сравнения для дата-временеи
        /// </summary>
        /// <param name="header">Заголовок</param>
        /// <param name="sqlOperator">Оператор SQL</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        private static string GetSqlStringPart(string header, string sqlOperator, DateTime value) =>
            $"{header} {sqlOperator} '#{value:dd.MM.yyyy HH:mm:ss}#'";

        private void bFilter_Click(object sender, EventArgs e)
        {
            var columns = _dataTable.Columns;
            var parts = new List<string>();

            string header;
            string sqlOperator;
            string tOperator;

            string sValue;
            int iValue;
            DateTime dValue;

            //Level
            tOperator = cbLevel1.Text;
            sValue = cbLevelValue1.Text;
            header = columns[0].ColumnName;

            if (tOperator != NoOperator)
            {
                var value = (int) Enum.Parse(typeof(LoggingLevel), sValue);

                var part = GetSqlStringPart(header, tOperator, value);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbLevel2.Text;
            sValue = cbLevelValue2.Text;

            if (tOperator != NoOperator)
            {
                var value = (int) Enum.Parse(typeof(LoggingLevel), sValue);

                var part = GetSqlStringPart(header, tOperator, value);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //TID
            tOperator = cbThreadId1.Text;
            iValue = (int) nudThreadId1.Value;
            header = columns[1].ColumnName;

            if (tOperator != NoOperator)
            {
                var part = GetSqlStringPart(header, tOperator, iValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbThreadId2.Text;
            iValue = (int) nudThreadId2.Value;

            if (tOperator != NoOperator)
            {
                var part = GetSqlStringPart(header, tOperator, iValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //Time
            tOperator = cbTimestamp1.Text;
            dValue = dtpTimestamp1.Value;
            header = columns[2].ColumnName;

            if (tOperator != NoOperator)
            {
                var part = GetSqlStringPart(header, tOperator, dValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbTimestamp2.Text;
            dValue = dtpTimestamp2.Value;

            if (tOperator != NoOperator)
            {
                var part = GetSqlStringPart(header, tOperator, dValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //Type
            tOperator = cbTypeObject1.Text;
            sValue = tbTypeObject1.Text;
            header = columns[3].ColumnName;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbTypeObject2.Text;
            sValue = tbTypeObject2.Text;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //Member
            tOperator = cbMemberName1.Text;
            sValue = tbMemberName1.Text;
            header = columns[4].ColumnName;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbMemberName2.Text;
            sValue = tbMemberName2.Text;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //Text
            tOperator = cbText1.Text;
            sValue = tbText1.Text;
            header = columns[5].ColumnName;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbText2.Text;
            sValue = tbText2.Text;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //Exc
            tOperator = cbException1.Text;
            sValue = tbException1.Text;
            header = columns[6].ColumnName;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbException2.Text;
            sValue = tbException2.Text;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            //ExcInfo
            tOperator = cbExceptionInfo1.Text;
            sValue = tbExceptionInfo1.Text;
            header = columns[7].ColumnName;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            tOperator = cbExceptionInfo2.Text;
            sValue = tbExceptionInfo2.Text;

            if (tOperator != NoOperator)
            {
                sqlOperator = _stringSqlOperators[tOperator];
                var part = GetSqlStringPart(header, sqlOperator, sValue);

                if (!string.IsNullOrEmpty(part))
                    parts.Add(part);
            }

            var rowFilter = parts.Count == 0
                ? null //$"[{columns[1].ColumnName}] IS NOT NULL"
                : string.Join(" AND ", parts);

            _dataTable.DefaultView.RowFilter = rowFilter;

            Count = _dataTable.DefaultView.Count;
            tsslCountValue.Text = Count.ToString();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw e.Error;
        }

        public static void ConvertEnumColumnToString(DataTable dt, string columnName, Type enumType)
        {
            if (dt == null)
                throw new ArgumentNullException(nameof(dt));

            if (columnName == null)
                throw new ArgumentNullException(nameof(columnName));

            using var dc = new DataColumn(columnName + "_new", typeof(string));

            var column = dt.Columns[columnName];

            if (column != null)
            {
                var ordinal = column.Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                foreach (DataRow dr in dt.Rows)
                    dr[dc.ColumnName] = Enum.GetName(enumType, dr[columnName]) ?? string.Empty;

                dt.Columns.Remove(columnName);

                dc.ColumnName = columnName;
            }
        }

        private void bExportToExcel_Click(object sender, EventArgs e)
        {
            if (_sfd.ShowDialog() != DialogResult.OK)
                return;

            //Имена столбцов таблицы, которые нам необходимы
            var needColumnNames = new[]
            {
                LevelColumnName, ThreadIdColumnName, TimestampColumnName, TypeObjectColumnName,
                MemberNameColumnName, TextColumnName, ExceptionColumnName
            };

            //Извлекаем подтаблицу из таблицы текущего вида
            var dt = _dataTable.DefaultView.ToTable(false, needColumnNames);

            //Заменяем в таблице числовые значения перечисления на текст
            ConvertEnumColumnToString(dt, LevelColumnName, typeof(LoggingLevel));

            //dt.Columns[LevelColumnName].DataType = typeof(string);

            //Создаем документ Excel
            var wb = new XLWorkbook();

            //Создаем новый лист
            var ws = wb.Worksheets.Add(dt, "Logs");

            //Стиль листа
            var wsStyle = ws.Style;

            ////Устанавливаем перенос по словам
            //wsStyle.Alignment.WrapText = true;

            //Фиксируем строку заголовков
            ws.SheetView.FreezeRows(1);

            //Центрируем значения малых столбцов по горизонтали
            ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //Центрируем значения таблицы по вертикали
            wsStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //Выравниваем все столбцы таблицы по содержимому
            ws.Columns().AdjustToContents(5d, 150);

            //Столбцы текстов не нужно центрировать, но нужен перенос строк
            var columns = ws.Columns(6, 7);

            foreach (var column in columns)
            {
                var columnStyle = column.Style;
                var alignment = columnStyle.Alignment;

                alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
                alignment.WrapText = true;
            }

            //Строку заголовкой центрируем по центру
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //Выравниваем все строки таблицы по содержимому
            ws.Rows().AdjustToContents();

            //Удаляем базовое распределение высот (для правильного автовыравнивания)
            foreach (var r in ws.Rows())
                r.ClearHeight();

            //Выравниваем все столбцы таблицы по содержимому
            ws.Columns().AdjustToContents(5d, 150);

            wb.SaveAs(_sfd.FileName);

            MessageBox.Show(@"Текущая таблица логов успешно экспортирована в Excel файл.");
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e) => this.SuspendPainting();

        private void MainForm_ResizeEnd(object sender, EventArgs e) => this.ResumePainting();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = false;

                Program.Close();
            }
        }

        #endregion

        ///// <summary>
        ///// Минимальное дата-время
        ///// </summary>
        //private DateTime _minDateTime = DateTime.MaxValue;

        ///// <summary>
        ///// Максимальное дата-время
        ///// </summary>
        //private DateTime _maxDateTime = DateTime.MinValue;

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    Program.Stop();
        //}
    }

}