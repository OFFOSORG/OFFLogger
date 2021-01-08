
namespace OFF.LogViewer.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bOpen = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.tsslGoodCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslGoodCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslBadCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslBadCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCountValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.bFilter = new System.Windows.Forms.Button();
            this.cbLevelValue1 = new System.Windows.Forms.ComboBox();
            this.cbLevelValue2 = new System.Windows.Forms.ComboBox();
            this.cbThreadId1 = new System.Windows.Forms.ComboBox();
            this.cbThreadId2 = new System.Windows.Forms.ComboBox();
            this.cbTimestamp1 = new System.Windows.Forms.ComboBox();
            this.cbTimestamp2 = new System.Windows.Forms.ComboBox();
            this.cbTypeObject1 = new System.Windows.Forms.ComboBox();
            this.cbTypeObject2 = new System.Windows.Forms.ComboBox();
            this.cbMemberName1 = new System.Windows.Forms.ComboBox();
            this.cbMemberName2 = new System.Windows.Forms.ComboBox();
            this.cbText1 = new System.Windows.Forms.ComboBox();
            this.cbText2 = new System.Windows.Forms.ComboBox();
            this.cbException1 = new System.Windows.Forms.ComboBox();
            this.cbException2 = new System.Windows.Forms.ComboBox();
            this.cbExceptionInfo1 = new System.Windows.Forms.ComboBox();
            this.cbExceptionInfo2 = new System.Windows.Forms.ComboBox();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bExportToExcel = new System.Windows.Forms.Button();
            this.pOpen = new System.Windows.Forms.Panel();
            this.pFilter = new System.Windows.Forms.Panel();
            this.lLevel = new System.Windows.Forms.Label();
            this.lTimestamp = new System.Windows.Forms.Label();
            this.lTypeObject = new System.Windows.Forms.Label();
            this.lException = new System.Windows.Forms.Label();
            this.lThreadId = new System.Windows.Forms.Label();
            this.lText = new System.Windows.Forms.Label();
            this.lMemberName = new System.Windows.Forms.Label();
            this.lExceptionInfo = new System.Windows.Forms.Label();
            this.nudThreadId1 = new System.Windows.Forms.NumericUpDown();
            this.nudThreadId2 = new System.Windows.Forms.NumericUpDown();
            this.cbLevel1 = new System.Windows.Forms.ComboBox();
            this.cbLevel2 = new System.Windows.Forms.ComboBox();
            this.dtpTimestamp1 = new System.Windows.Forms.DateTimePicker();
            this.dtpTimestamp2 = new System.Windows.Forms.DateTimePicker();
            this.tbTypeObject1 = new System.Windows.Forms.TextBox();
            this.tbTypeObject2 = new System.Windows.Forms.TextBox();
            this.tbException1 = new System.Windows.Forms.TextBox();
            this.tbException2 = new System.Windows.Forms.TextBox();
            this.tbExceptionInfo1 = new System.Windows.Forms.TextBox();
            this.tbExceptionInfo2 = new System.Windows.Forms.TextBox();
            this.tbText1 = new System.Windows.Forms.TextBox();
            this.tbText2 = new System.Windows.Forms.TextBox();
            this.tbMemberName1 = new System.Windows.Forms.TextBox();
            this.tbMemberName2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.ss.SuspendLayout();
            this.tlp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pOpen.SuspendLayout();
            this.pFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadId1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadId2)).BeginInit();
            this.SuspendLayout();
            // 
            // bOpen
            // 
            this.bOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bOpen.Location = new System.Drawing.Point(3, 3);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(376, 23);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "Открыть файл";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // dgv
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Location = new System.Drawing.Point(0, 191);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.Size = new System.Drawing.Size(784, 349);
            this.dgv.TabIndex = 0;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // bw
            // 
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // ss
            // 
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslGoodCount,
            this.tsslGoodCountValue,
            this.tsslBadCount,
            this.tsslBadCountValue,
            this.tsslCount,
            this.tsslCountValue});
            this.ss.Location = new System.Drawing.Point(0, 543);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(784, 22);
            this.ss.TabIndex = 1;
            this.ss.Text = "statusStrip1";
            // 
            // tsslGoodCount
            // 
            this.tsslGoodCount.MergeIndex = 1;
            this.tsslGoodCount.Name = "tsslGoodCount";
            this.tsslGoodCount.Size = new System.Drawing.Size(57, 17);
            this.tsslGoodCount.Text = "Удачных:";
            // 
            // tsslGoodCountValue
            // 
            this.tsslGoodCountValue.MergeIndex = 1;
            this.tsslGoodCountValue.Name = "tsslGoodCountValue";
            this.tsslGoodCountValue.Size = new System.Drawing.Size(22, 17);
            this.tsslGoodCountValue.Text = "???";
            // 
            // tsslBadCount
            // 
            this.tsslBadCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslBadCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslBadCount.MergeIndex = 2;
            this.tsslBadCount.Name = "tsslBadCount";
            this.tsslBadCount.Size = new System.Drawing.Size(73, 17);
            this.tsslBadCount.Text = "Неудачных:";
            // 
            // tsslBadCountValue
            // 
            this.tsslBadCountValue.MergeIndex = 2;
            this.tsslBadCountValue.Name = "tsslBadCountValue";
            this.tsslBadCountValue.Size = new System.Drawing.Size(22, 17);
            this.tsslBadCountValue.Text = "???";
            // 
            // tsslCount
            // 
            this.tsslCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsslCount.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslCount.MergeIndex = 3;
            this.tsslCount.Name = "tsslCount";
            this.tsslCount.Size = new System.Drawing.Size(60, 17);
            this.tsslCount.Text = "Текущее:";
            // 
            // tsslCountValue
            // 
            this.tsslCountValue.MergeIndex = 3;
            this.tsslCountValue.Name = "tsslCountValue";
            this.tsslCountValue.Size = new System.Drawing.Size(22, 17);
            this.tsslCountValue.Text = "???";
            // 
            // bFilter
            // 
            this.bFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bFilter.Enabled = false;
            this.bFilter.Location = new System.Drawing.Point(3, 3);
            this.bFilter.Name = "bFilter";
            this.bFilter.Size = new System.Drawing.Size(378, 23);
            this.bFilter.TabIndex = 0;
            this.bFilter.Text = "Применить фильтр";
            this.bFilter.UseVisualStyleBackColor = true;
            this.bFilter.Click += new System.EventHandler(this.bFilter_Click);
            // 
            // cbLevelValue1
            // 
            this.cbLevelValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLevelValue1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelValue1.FormattingEnabled = true;
            this.cbLevelValue1.Location = new System.Drawing.Point(115, 5);
            this.cbLevelValue1.Name = "cbLevelValue1";
            this.cbLevelValue1.Size = new System.Drawing.Size(105, 21);
            this.cbLevelValue1.TabIndex = 2;
            // 
            // cbLevelValue2
            // 
            this.cbLevelValue2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLevelValue2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelValue2.FormattingEnabled = true;
            this.cbLevelValue2.Location = new System.Drawing.Point(276, 5);
            this.cbLevelValue2.Name = "cbLevelValue2";
            this.cbLevelValue2.Size = new System.Drawing.Size(105, 21);
            this.cbLevelValue2.TabIndex = 4;
            // 
            // cbThreadId1
            // 
            this.cbThreadId1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbThreadId1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThreadId1.FormattingEnabled = true;
            this.cbThreadId1.Location = new System.Drawing.Point(463, 5);
            this.cbThreadId1.Name = "cbThreadId1";
            this.cbThreadId1.Size = new System.Drawing.Size(40, 21);
            this.cbThreadId1.TabIndex = 21;
            // 
            // cbThreadId2
            // 
            this.cbThreadId2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbThreadId2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThreadId2.FormattingEnabled = true;
            this.cbThreadId2.Location = new System.Drawing.Point(624, 5);
            this.cbThreadId2.Name = "cbThreadId2";
            this.cbThreadId2.Size = new System.Drawing.Size(40, 21);
            this.cbThreadId2.TabIndex = 23;
            // 
            // cbTimestamp1
            // 
            this.cbTimestamp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTimestamp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimestamp1.FormattingEnabled = true;
            this.cbTimestamp1.Location = new System.Drawing.Point(67, 36);
            this.cbTimestamp1.Name = "cbTimestamp1";
            this.cbTimestamp1.Size = new System.Drawing.Size(40, 21);
            this.cbTimestamp1.TabIndex = 6;
            // 
            // cbTimestamp2
            // 
            this.cbTimestamp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTimestamp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimestamp2.FormattingEnabled = true;
            this.cbTimestamp2.Location = new System.Drawing.Point(228, 36);
            this.cbTimestamp2.Name = "cbTimestamp2";
            this.cbTimestamp2.Size = new System.Drawing.Size(40, 21);
            this.cbTimestamp2.TabIndex = 8;
            // 
            // cbTypeObject1
            // 
            this.cbTypeObject1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTypeObject1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeObject1.FormattingEnabled = true;
            this.cbTypeObject1.Location = new System.Drawing.Point(67, 67);
            this.cbTypeObject1.Name = "cbTypeObject1";
            this.cbTypeObject1.Size = new System.Drawing.Size(40, 21);
            this.cbTypeObject1.TabIndex = 11;
            // 
            // cbTypeObject2
            // 
            this.cbTypeObject2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbTypeObject2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeObject2.FormattingEnabled = true;
            this.cbTypeObject2.Location = new System.Drawing.Point(228, 67);
            this.cbTypeObject2.Name = "cbTypeObject2";
            this.cbTypeObject2.Size = new System.Drawing.Size(40, 21);
            this.cbTypeObject2.TabIndex = 13;
            // 
            // cbMemberName1
            // 
            this.cbMemberName1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMemberName1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMemberName1.FormattingEnabled = true;
            this.cbMemberName1.Location = new System.Drawing.Point(463, 67);
            this.cbMemberName1.Name = "cbMemberName1";
            this.cbMemberName1.Size = new System.Drawing.Size(40, 21);
            this.cbMemberName1.TabIndex = 31;
            // 
            // cbMemberName2
            // 
            this.cbMemberName2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbMemberName2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMemberName2.FormattingEnabled = true;
            this.cbMemberName2.Location = new System.Drawing.Point(624, 67);
            this.cbMemberName2.Name = "cbMemberName2";
            this.cbMemberName2.Size = new System.Drawing.Size(40, 21);
            this.cbMemberName2.TabIndex = 33;
            // 
            // cbText1
            // 
            this.cbText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbText1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbText1.FormattingEnabled = true;
            this.cbText1.Location = new System.Drawing.Point(463, 36);
            this.cbText1.Name = "cbText1";
            this.cbText1.Size = new System.Drawing.Size(40, 21);
            this.cbText1.TabIndex = 26;
            // 
            // cbText2
            // 
            this.cbText2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbText2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbText2.FormattingEnabled = true;
            this.cbText2.Location = new System.Drawing.Point(624, 36);
            this.cbText2.Name = "cbText2";
            this.cbText2.Size = new System.Drawing.Size(40, 21);
            this.cbText2.TabIndex = 28;
            // 
            // cbException1
            // 
            this.cbException1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbException1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbException1.FormattingEnabled = true;
            this.cbException1.Location = new System.Drawing.Point(67, 98);
            this.cbException1.Name = "cbException1";
            this.cbException1.Size = new System.Drawing.Size(40, 21);
            this.cbException1.TabIndex = 16;
            // 
            // cbException2
            // 
            this.cbException2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbException2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbException2.FormattingEnabled = true;
            this.cbException2.Location = new System.Drawing.Point(228, 98);
            this.cbException2.Name = "cbException2";
            this.cbException2.Size = new System.Drawing.Size(40, 21);
            this.cbException2.TabIndex = 18;
            // 
            // cbExceptionInfo1
            // 
            this.cbExceptionInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExceptionInfo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExceptionInfo1.FormattingEnabled = true;
            this.cbExceptionInfo1.Location = new System.Drawing.Point(463, 98);
            this.cbExceptionInfo1.Name = "cbExceptionInfo1";
            this.cbExceptionInfo1.Size = new System.Drawing.Size(40, 21);
            this.cbExceptionInfo1.TabIndex = 36;
            // 
            // cbExceptionInfo2
            // 
            this.cbExceptionInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExceptionInfo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExceptionInfo2.FormattingEnabled = true;
            this.cbExceptionInfo2.Location = new System.Drawing.Point(624, 98);
            this.cbExceptionInfo2.Name = "cbExceptionInfo2";
            this.cbExceptionInfo2.Size = new System.Drawing.Size(40, 21);
            this.cbExceptionInfo2.TabIndex = 38;
            // 
            // tlp
            // 
            this.tlp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlp.ColumnCount = 11;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp.Controls.Add(this.panel1, 0, 5);
            this.tlp.Controls.Add(this.cbLevelValue1, 2, 0);
            this.tlp.Controls.Add(this.cbExceptionInfo2, 9, 3);
            this.tlp.Controls.Add(this.cbLevelValue2, 4, 0);
            this.tlp.Controls.Add(this.cbMemberName2, 9, 2);
            this.tlp.Controls.Add(this.cbText2, 9, 1);
            this.tlp.Controls.Add(this.cbTypeObject1, 1, 2);
            this.tlp.Controls.Add(this.cbExceptionInfo1, 7, 3);
            this.tlp.Controls.Add(this.cbThreadId2, 9, 0);
            this.tlp.Controls.Add(this.cbException1, 1, 3);
            this.tlp.Controls.Add(this.cbText1, 7, 1);
            this.tlp.Controls.Add(this.cbMemberName1, 7, 2);
            this.tlp.Controls.Add(this.cbTimestamp1, 1, 1);
            this.tlp.Controls.Add(this.cbTimestamp2, 3, 1);
            this.tlp.Controls.Add(this.cbTypeObject2, 3, 2);
            this.tlp.Controls.Add(this.cbException2, 3, 3);
            this.tlp.Controls.Add(this.cbThreadId1, 7, 0);
            this.tlp.Controls.Add(this.pOpen, 0, 4);
            this.tlp.Controls.Add(this.pFilter, 6, 4);
            this.tlp.Controls.Add(this.lLevel, 0, 0);
            this.tlp.Controls.Add(this.lTimestamp, 0, 1);
            this.tlp.Controls.Add(this.lTypeObject, 0, 2);
            this.tlp.Controls.Add(this.lException, 0, 3);
            this.tlp.Controls.Add(this.lThreadId, 6, 0);
            this.tlp.Controls.Add(this.lText, 6, 1);
            this.tlp.Controls.Add(this.lMemberName, 6, 2);
            this.tlp.Controls.Add(this.lExceptionInfo, 6, 3);
            this.tlp.Controls.Add(this.nudThreadId1, 8, 0);
            this.tlp.Controls.Add(this.nudThreadId2, 10, 0);
            this.tlp.Controls.Add(this.cbLevel1, 1, 0);
            this.tlp.Controls.Add(this.cbLevel2, 3, 0);
            this.tlp.Controls.Add(this.dtpTimestamp1, 2, 1);
            this.tlp.Controls.Add(this.dtpTimestamp2, 4, 1);
            this.tlp.Controls.Add(this.tbTypeObject1, 2, 2);
            this.tlp.Controls.Add(this.tbTypeObject2, 4, 2);
            this.tlp.Controls.Add(this.tbException1, 2, 3);
            this.tlp.Controls.Add(this.tbException2, 4, 3);
            this.tlp.Controls.Add(this.tbExceptionInfo1, 8, 3);
            this.tlp.Controls.Add(this.tbExceptionInfo2, 10, 3);
            this.tlp.Controls.Add(this.tbText1, 8, 1);
            this.tlp.Controls.Add(this.tbText2, 10, 1);
            this.tlp.Controls.Add(this.tbMemberName1, 8, 2);
            this.tlp.Controls.Add(this.tbMemberName2, 10, 2);
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 6;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp.Size = new System.Drawing.Size(784, 190);
            this.tlp.TabIndex = 0;
            // 
            // panel1
            // 
            this.tlp.SetColumnSpan(this.panel1, 11);
            this.panel1.Controls.Add(this.bExportToExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 157);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 31);
            this.panel1.TabIndex = 40;
            // 
            // bExportToExcel
            // 
            this.bExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bExportToExcel.Enabled = false;
            this.bExportToExcel.Location = new System.Drawing.Point(3, 3);
            this.bExportToExcel.Name = "bExportToExcel";
            this.bExportToExcel.Size = new System.Drawing.Size(774, 25);
            this.bExportToExcel.TabIndex = 2;
            this.bExportToExcel.Text = "Экспортировать в Excel";
            this.bExportToExcel.UseVisualStyleBackColor = true;
            this.bExportToExcel.Click += new System.EventHandler(this.bExportToExcel_Click);
            // 
            // pOpen
            // 
            this.tlp.SetColumnSpan(this.pOpen, 5);
            this.pOpen.Controls.Add(this.bOpen);
            this.pOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOpen.Location = new System.Drawing.Point(2, 126);
            this.pOpen.Margin = new System.Windows.Forms.Padding(0);
            this.pOpen.Name = "pOpen";
            this.pOpen.Size = new System.Drawing.Size(382, 29);
            this.pOpen.TabIndex = 0;
            // 
            // pFilter
            // 
            this.tlp.SetColumnSpan(this.pFilter, 5);
            this.pFilter.Controls.Add(this.bFilter);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pFilter.Location = new System.Drawing.Point(398, 126);
            this.pFilter.Margin = new System.Windows.Forms.Padding(0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(384, 29);
            this.pFilter.TabIndex = 1;
            // 
            // lLevel
            // 
            this.lLevel.AutoSize = true;
            this.lLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lLevel.Location = new System.Drawing.Point(5, 2);
            this.lLevel.Name = "lLevel";
            this.lLevel.Size = new System.Drawing.Size(54, 29);
            this.lLevel.TabIndex = 0;
            this.lLevel.Text = "Level";
            this.lLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lTimestamp
            // 
            this.lTimestamp.AutoSize = true;
            this.lTimestamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTimestamp.Location = new System.Drawing.Point(5, 33);
            this.lTimestamp.Name = "lTimestamp";
            this.lTimestamp.Size = new System.Drawing.Size(54, 29);
            this.lTimestamp.TabIndex = 5;
            this.lTimestamp.Text = "Time";
            this.lTimestamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lTypeObject
            // 
            this.lTypeObject.AutoSize = true;
            this.lTypeObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTypeObject.Location = new System.Drawing.Point(5, 64);
            this.lTypeObject.Name = "lTypeObject";
            this.lTypeObject.Size = new System.Drawing.Size(54, 29);
            this.lTypeObject.TabIndex = 10;
            this.lTypeObject.Text = "Type";
            this.lTypeObject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lException
            // 
            this.lException.AutoSize = true;
            this.lException.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lException.Location = new System.Drawing.Point(5, 95);
            this.lException.Name = "lException";
            this.lException.Size = new System.Drawing.Size(54, 29);
            this.lException.TabIndex = 15;
            this.lException.Text = "Exc";
            this.lException.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lThreadId
            // 
            this.lThreadId.AutoSize = true;
            this.lThreadId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lThreadId.Location = new System.Drawing.Point(401, 2);
            this.lThreadId.Name = "lThreadId";
            this.lThreadId.Size = new System.Drawing.Size(54, 29);
            this.lThreadId.TabIndex = 20;
            this.lThreadId.Text = "TID";
            this.lThreadId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lText
            // 
            this.lText.AutoSize = true;
            this.lText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lText.Location = new System.Drawing.Point(401, 33);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(54, 29);
            this.lText.TabIndex = 25;
            this.lText.Text = "Text";
            this.lText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lMemberName
            // 
            this.lMemberName.AutoSize = true;
            this.lMemberName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lMemberName.Location = new System.Drawing.Point(401, 64);
            this.lMemberName.Name = "lMemberName";
            this.lMemberName.Size = new System.Drawing.Size(54, 29);
            this.lMemberName.TabIndex = 30;
            this.lMemberName.Text = "Member";
            this.lMemberName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lExceptionInfo
            // 
            this.lExceptionInfo.AutoSize = true;
            this.lExceptionInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lExceptionInfo.Location = new System.Drawing.Point(401, 95);
            this.lExceptionInfo.Name = "lExceptionInfo";
            this.lExceptionInfo.Size = new System.Drawing.Size(54, 29);
            this.lExceptionInfo.TabIndex = 35;
            this.lExceptionInfo.Text = "ExcInfo";
            this.lExceptionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudThreadId1
            // 
            this.nudThreadId1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudThreadId1.Location = new System.Drawing.Point(511, 5);
            this.nudThreadId1.Name = "nudThreadId1";
            this.nudThreadId1.Size = new System.Drawing.Size(105, 21);
            this.nudThreadId1.TabIndex = 22;
            // 
            // nudThreadId2
            // 
            this.nudThreadId2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudThreadId2.Location = new System.Drawing.Point(672, 5);
            this.nudThreadId2.Name = "nudThreadId2";
            this.nudThreadId2.Size = new System.Drawing.Size(107, 21);
            this.nudThreadId2.TabIndex = 24;
            // 
            // cbLevel1
            // 
            this.cbLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel1.FormattingEnabled = true;
            this.cbLevel1.Location = new System.Drawing.Point(67, 5);
            this.cbLevel1.Name = "cbLevel1";
            this.cbLevel1.Size = new System.Drawing.Size(40, 21);
            this.cbLevel1.TabIndex = 1;
            // 
            // cbLevel2
            // 
            this.cbLevel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel2.FormattingEnabled = true;
            this.cbLevel2.Location = new System.Drawing.Point(228, 5);
            this.cbLevel2.Name = "cbLevel2";
            this.cbLevel2.Size = new System.Drawing.Size(40, 21);
            this.cbLevel2.TabIndex = 3;
            // 
            // dtpTimestamp1
            // 
            this.dtpTimestamp1.CustomFormat = "MM.dd HH:mm:ss";
            this.dtpTimestamp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTimestamp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimestamp1.Location = new System.Drawing.Point(115, 36);
            this.dtpTimestamp1.Name = "dtpTimestamp1";
            this.dtpTimestamp1.Size = new System.Drawing.Size(105, 21);
            this.dtpTimestamp1.TabIndex = 7;
            // 
            // dtpTimestamp2
            // 
            this.dtpTimestamp2.CustomFormat = "MM.dd HH:mm:ss";
            this.dtpTimestamp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTimestamp2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimestamp2.Location = new System.Drawing.Point(276, 36);
            this.dtpTimestamp2.Name = "dtpTimestamp2";
            this.dtpTimestamp2.Size = new System.Drawing.Size(105, 21);
            this.dtpTimestamp2.TabIndex = 9;
            // 
            // tbTypeObject1
            // 
            this.tbTypeObject1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTypeObject1.Location = new System.Drawing.Point(115, 67);
            this.tbTypeObject1.Name = "tbTypeObject1";
            this.tbTypeObject1.Size = new System.Drawing.Size(105, 21);
            this.tbTypeObject1.TabIndex = 12;
            // 
            // tbTypeObject2
            // 
            this.tbTypeObject2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTypeObject2.Location = new System.Drawing.Point(276, 67);
            this.tbTypeObject2.Name = "tbTypeObject2";
            this.tbTypeObject2.Size = new System.Drawing.Size(105, 21);
            this.tbTypeObject2.TabIndex = 14;
            // 
            // tbException1
            // 
            this.tbException1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbException1.Location = new System.Drawing.Point(115, 98);
            this.tbException1.Name = "tbException1";
            this.tbException1.Size = new System.Drawing.Size(105, 21);
            this.tbException1.TabIndex = 17;
            // 
            // tbException2
            // 
            this.tbException2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbException2.Location = new System.Drawing.Point(276, 98);
            this.tbException2.Name = "tbException2";
            this.tbException2.Size = new System.Drawing.Size(105, 21);
            this.tbException2.TabIndex = 19;
            // 
            // tbExceptionInfo1
            // 
            this.tbExceptionInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExceptionInfo1.Location = new System.Drawing.Point(511, 98);
            this.tbExceptionInfo1.Name = "tbExceptionInfo1";
            this.tbExceptionInfo1.Size = new System.Drawing.Size(105, 21);
            this.tbExceptionInfo1.TabIndex = 37;
            // 
            // tbExceptionInfo2
            // 
            this.tbExceptionInfo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbExceptionInfo2.Location = new System.Drawing.Point(672, 98);
            this.tbExceptionInfo2.Name = "tbExceptionInfo2";
            this.tbExceptionInfo2.Size = new System.Drawing.Size(107, 21);
            this.tbExceptionInfo2.TabIndex = 39;
            // 
            // tbText1
            // 
            this.tbText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbText1.Location = new System.Drawing.Point(511, 36);
            this.tbText1.Name = "tbText1";
            this.tbText1.Size = new System.Drawing.Size(105, 21);
            this.tbText1.TabIndex = 27;
            // 
            // tbText2
            // 
            this.tbText2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbText2.Location = new System.Drawing.Point(672, 36);
            this.tbText2.Name = "tbText2";
            this.tbText2.Size = new System.Drawing.Size(107, 21);
            this.tbText2.TabIndex = 29;
            // 
            // tbMemberName1
            // 
            this.tbMemberName1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMemberName1.Location = new System.Drawing.Point(511, 67);
            this.tbMemberName1.Name = "tbMemberName1";
            this.tbMemberName1.Size = new System.Drawing.Size(105, 21);
            this.tbMemberName1.TabIndex = 32;
            // 
            // tbMemberName2
            // 
            this.tbMemberName2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMemberName2.Location = new System.Drawing.Point(672, 67);
            this.tbMemberName2.Name = "tbMemberName2";
            this.tbMemberName2.Size = new System.Drawing.Size(107, 21);
            this.tbMemberName2.TabIndex = 34;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.tlp);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.dgv);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "LogViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.tlp.ResumeLayout(false);
            this.tlp.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pOpen.ResumeLayout(false);
            this.pFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadId1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadId2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpen;
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripStatusLabel tsslGoodCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslGoodCountValue;
        private System.Windows.Forms.ToolStripStatusLabel tsslBadCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslBadCountValue;
        private System.Windows.Forms.ToolStripStatusLabel tsslCount;
        private System.Windows.Forms.ToolStripStatusLabel tsslCountValue;
        private System.Windows.Forms.Button bFilter;
        private System.Windows.Forms.ComboBox cbLevelValue1;
        private System.Windows.Forms.ComboBox cbLevelValue2;
        private System.Windows.Forms.ComboBox cbThreadId1;
        private System.Windows.Forms.ComboBox cbThreadId2;
        private System.Windows.Forms.ComboBox cbTimestamp1;
        private System.Windows.Forms.ComboBox cbTimestamp2;
        private System.Windows.Forms.ComboBox cbTypeObject1;
        private System.Windows.Forms.ComboBox cbTypeObject2;
        private System.Windows.Forms.ComboBox cbMemberName1;
        private System.Windows.Forms.ComboBox cbMemberName2;
        private System.Windows.Forms.ComboBox cbText1;
        private System.Windows.Forms.ComboBox cbText2;
        private System.Windows.Forms.ComboBox cbException1;
        private System.Windows.Forms.ComboBox cbException2;
        private System.Windows.Forms.ComboBox cbExceptionInfo1;
        private System.Windows.Forms.ComboBox cbExceptionInfo2;
        private System.Windows.Forms.TableLayoutPanel tlp;
        private System.Windows.Forms.Panel pOpen;
        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Label lLevel;
        private System.Windows.Forms.Label lTimestamp;
        private System.Windows.Forms.Label lTypeObject;
        private System.Windows.Forms.Label lException;
        private System.Windows.Forms.Label lThreadId;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.Label lMemberName;
        private System.Windows.Forms.Label lExceptionInfo;
        private System.Windows.Forms.NumericUpDown nudThreadId1;
        private System.Windows.Forms.NumericUpDown nudThreadId2;
        private System.Windows.Forms.ComboBox cbLevel1;
        private System.Windows.Forms.ComboBox cbLevel2;
        private System.Windows.Forms.DateTimePicker dtpTimestamp1;
        private System.Windows.Forms.DateTimePicker dtpTimestamp2;
        private System.Windows.Forms.TextBox tbTypeObject1;
        private System.Windows.Forms.TextBox tbTypeObject2;
        private System.Windows.Forms.TextBox tbException1;
        private System.Windows.Forms.TextBox tbException2;
        private System.Windows.Forms.TextBox tbExceptionInfo1;
        private System.Windows.Forms.TextBox tbExceptionInfo2;
        private System.Windows.Forms.TextBox tbText1;
        private System.Windows.Forms.TextBox tbText2;
        private System.Windows.Forms.TextBox tbMemberName1;
        private System.Windows.Forms.TextBox tbMemberName2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button bExportToExcel;
        private System.Windows.Forms.Panel panel1;
    }
}

