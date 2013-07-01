namespace MySpaceLoader
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.SearchSongs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new MySpaceLoader.HelperObjects.TransparentLabel();
            this.label3 = new MySpaceLoader.HelperObjects.TransparentLabel();
            this.panelstatus = new System.Windows.Forms.Panel();
            this.labelStatusSong = new System.Windows.Forms.Label();
            this.progressBarSong = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.textBoxBase = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControlBar = new System.Windows.Forms.Panel();
            this.pictureBoxConfig = new System.Windows.Forms.PictureBox();
            this.pictureBoxDe = new System.Windows.Forms.PictureBox();
            this.pictureBoxEn = new System.Windows.Forms.PictureBox();
            this.pictureBoxMinimize = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.transparentLabel1 = new MySpaceLoader.HelperObjects.TransparentLabel();
            this.dataGridViewSongs = new System.Windows.Forms.DataGridView();
            this.ColumnDownload = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnSong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.groupBoxMenü = new System.Windows.Forms.GroupBox();
            this.labelFormat = new System.Windows.Forms.Label();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.buttonConfigSave = new System.Windows.Forms.Button();
            this.labelConfigSave = new System.Windows.Forms.Label();
            this.textBoxConfigSave = new System.Windows.Forms.TextBox();
            this.labelConfigTitle = new System.Windows.Forms.Label();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.radioButtonSearchClassic = new System.Windows.Forms.RadioButton();
            this.radioButtonSearchNew = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            this.panelstatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelControlBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSongs)).BeginInit();
            this.groupBoxMenü.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUrl.Location = new System.Drawing.Point(120, 12);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(237, 20);
            this.textBoxUrl.TabIndex = 0;
            this.textBoxUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUrl_KeyPress);
            // 
            // SearchSongs
            // 
            this.SearchSongs.Location = new System.Drawing.Point(363, 10);
            this.SearchSongs.Name = "SearchSongs";
            this.SearchSongs.Size = new System.Drawing.Size(75, 23);
            this.SearchSongs.TabIndex = 1;
            this.SearchSongs.Text = "Suche";
            this.SearchSongs.UseVisualStyleBackColor = true;
            this.SearchSongs.Click += new System.EventHandler(this.SearchSongs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBoxHelp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(469, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hilfe";
            // 
            // pictureBoxHelp
            // 
            this.pictureBoxHelp.BackgroundImage = global::MySpaceLoader.Properties.Resources.help;
            this.pictureBoxHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxHelp.Location = new System.Drawing.Point(41, 80);
            this.pictureBoxHelp.Name = "pictureBoxHelp";
            this.pictureBoxHelp.Size = new System.Drawing.Size(96, 93);
            this.pictureBoxHelp.TabIndex = 10;
            this.pictureBoxHelp.TabStop = false;
            this.pictureBoxHelp.Click += new System.EventHandler(this.pictureBoxHelp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Du brauchst Hilfe oder willst \r\nnoch ein paar Tipps & Tricks \r\nkennenlernen? Dann" +
    " drücke \r\nauf das Fragezeichen!";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(530, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 46);
            this.label2.TabIndex = 5;
            this.label2.TabStop = false;
            this.label2.Text = "MySpace\r\nDownloader";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.label3.Location = new System.Drawing.Point(467, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 14);
            this.label3.TabIndex = 6;
            this.label3.TabStop = false;
            this.label3.Text = "      http://www.code-bude.net";
            // 
            // panelstatus
            // 
            this.panelstatus.BackColor = System.Drawing.Color.White;
            this.panelstatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelstatus.Controls.Add(this.labelStatusSong);
            this.panelstatus.Controls.Add(this.progressBarSong);
            this.panelstatus.Controls.Add(this.labelStatus);
            this.panelstatus.Controls.Add(this.progressBar1);
            this.panelstatus.Location = new System.Drawing.Point(132, 104);
            this.panelstatus.Name = "panelstatus";
            this.panelstatus.Size = new System.Drawing.Size(388, 69);
            this.panelstatus.TabIndex = 7;
            this.panelstatus.Visible = false;
            // 
            // labelStatusSong
            // 
            this.labelStatusSong.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusSong.Location = new System.Drawing.Point(-1, 68);
            this.labelStatusSong.Name = "labelStatusSong";
            this.labelStatusSong.Size = new System.Drawing.Size(388, 23);
            this.labelStatusSong.TabIndex = 10;
            this.labelStatusSong.Text = "Song 0/0";
            this.labelStatusSong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarSong
            // 
            this.progressBarSong.Location = new System.Drawing.Point(9, 94);
            this.progressBarSong.Name = "progressBarSong";
            this.progressBarSong.Size = new System.Drawing.Size(369, 23);
            this.progressBarSong.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarSong.TabIndex = 9;
            // 
            // labelStatus
            // 
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(-1, 6);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(388, 23);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Suche...";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 32);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(369, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::MySpaceLoader.Properties.Resources.myspace;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(67, 63);
            this.pictureBoxLogo.TabIndex = 4;
            this.pictureBoxLogo.TabStop = false;
            // 
            // textBoxBase
            // 
            this.textBoxBase.Enabled = false;
            this.textBoxBase.Location = new System.Drawing.Point(6, 12);
            this.textBoxBase.Name = "textBoxBase";
            this.textBoxBase.Size = new System.Drawing.Size(110, 22);
            this.textBoxBase.TabIndex = 8;
            this.textBoxBase.Text = "http://myspace.com/";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBoxLogo);
            this.panel1.Location = new System.Drawing.Point(469, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(63, 63);
            this.panel1.TabIndex = 9;
            // 
            // panelControlBar
            // 
            this.panelControlBar.BackColor = System.Drawing.Color.Transparent;
            this.panelControlBar.Controls.Add(this.pictureBoxConfig);
            this.panelControlBar.Controls.Add(this.pictureBoxDe);
            this.panelControlBar.Controls.Add(this.pictureBoxEn);
            this.panelControlBar.Controls.Add(this.pictureBoxMinimize);
            this.panelControlBar.Controls.Add(this.pictureBoxClose);
            this.panelControlBar.Location = new System.Drawing.Point(480, 0);
            this.panelControlBar.Name = "panelControlBar";
            this.panelControlBar.Size = new System.Drawing.Size(176, 34);
            this.panelControlBar.TabIndex = 10;
            // 
            // pictureBoxConfig
            // 
            this.pictureBoxConfig.BackgroundImage = global::MySpaceLoader.Properties.Resources.settings;
            this.pictureBoxConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxConfig.Location = new System.Drawing.Point(3, 0);
            this.pictureBoxConfig.Name = "pictureBoxConfig";
            this.pictureBoxConfig.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxConfig.TabIndex = 9;
            this.pictureBoxConfig.TabStop = false;
            this.pictureBoxConfig.Click += new System.EventHandler(this.pictureBoxConfig_Click);
            // 
            // pictureBoxDe
            // 
            this.pictureBoxDe.BackgroundImage = global::MySpaceLoader.Properties.Resources.de1;
            this.pictureBoxDe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxDe.Location = new System.Drawing.Point(39, 0);
            this.pictureBoxDe.Name = "pictureBoxDe";
            this.pictureBoxDe.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxDe.TabIndex = 8;
            this.pictureBoxDe.TabStop = false;
            this.pictureBoxDe.Click += new System.EventHandler(this.pictureBoxDe_Click);
            // 
            // pictureBoxEn
            // 
            this.pictureBoxEn.BackgroundImage = global::MySpaceLoader.Properties.Resources.en;
            this.pictureBoxEn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxEn.Location = new System.Drawing.Point(73, 0);
            this.pictureBoxEn.Name = "pictureBoxEn";
            this.pictureBoxEn.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxEn.TabIndex = 7;
            this.pictureBoxEn.TabStop = false;
            this.pictureBoxEn.Click += new System.EventHandler(this.pictureBoxEn_Click);
            // 
            // pictureBoxMinimize
            // 
            this.pictureBoxMinimize.BackgroundImage = global::MySpaceLoader.Properties.Resources.minimize;
            this.pictureBoxMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxMinimize.Location = new System.Drawing.Point(109, 0);
            this.pictureBoxMinimize.Name = "pictureBoxMinimize";
            this.pictureBoxMinimize.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxMinimize.TabIndex = 6;
            this.pictureBoxMinimize.TabStop = false;
            this.pictureBoxMinimize.Click += new System.EventHandler(this.pictureBoxMinimize_Click);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackgroundImage = global::MySpaceLoader.Properties.Resources.close;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Location = new System.Drawing.Point(143, 0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(30, 31);
            this.pictureBoxClose.TabIndex = 5;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // transparentLabel1
            // 
            this.transparentLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transparentLabel1.ForeColor = System.Drawing.Color.White;
            this.transparentLabel1.Location = new System.Drawing.Point(8, 6);
            this.transparentLabel1.Name = "transparentLabel1";
            this.transparentLabel1.Size = new System.Drawing.Size(456, 25);
            this.transparentLabel1.TabIndex = 11;
            this.transparentLabel1.TabStop = false;
            this.transparentLabel1.Text = "MySpace Loader [ProVersion 4.0] ";
            this.transparentLabel1.Click += new System.EventHandler(this.transparentLabel1_Click);
            // 
            // dataGridViewSongs
            // 
            this.dataGridViewSongs.AllowUserToAddRows = false;
            this.dataGridViewSongs.AllowUserToDeleteRows = false;
            this.dataGridViewSongs.AllowUserToResizeColumns = false;
            this.dataGridViewSongs.AllowUserToResizeRows = false;
            this.dataGridViewSongs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewSongs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSongs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDownload,
            this.ColumnSong,
            this.Artist});
            this.dataGridViewSongs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewSongs.Location = new System.Drawing.Point(18, 94);
            this.dataGridViewSongs.MultiSelect = false;
            this.dataGridViewSongs.Name = "dataGridViewSongs";
            this.dataGridViewSongs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSongs.ShowCellErrors = false;
            this.dataGridViewSongs.ShowCellToolTips = false;
            this.dataGridViewSongs.ShowEditingIcon = false;
            this.dataGridViewSongs.ShowRowErrors = false;
            this.dataGridViewSongs.Size = new System.Drawing.Size(431, 169);
            this.dataGridViewSongs.TabIndex = 12;
            this.dataGridViewSongs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSongs_CellValueChanged);
            this.dataGridViewSongs.Click += new System.EventHandler(this.dataGridViewSongs_Click);
            // 
            // ColumnDownload
            // 
            this.ColumnDownload.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColumnDownload.HeaderText = "Download";
            this.ColumnDownload.Name = "ColumnDownload";
            this.ColumnDownload.Width = 67;
            // 
            // ColumnSong
            // 
            this.ColumnSong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnSong.HeaderText = "Song";
            this.ColumnSong.Name = "ColumnSong";
            this.ColumnSong.ReadOnly = true;
            // 
            // Artist
            // 
            this.Artist.HeaderText = "Artist";
            this.Artist.Name = "Artist";
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(338, 11);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(88, 24);
            this.buttonDownload.TabIndex = 13;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.ItemHeight = 13;
            this.comboBoxFormat.Items.AddRange(new object[] {
            ".mp3",
            ".wma",
            ".ogg",
            ".aac",
            ".flv"});
            this.comboBoxFormat.Location = new System.Drawing.Point(69, 13);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(74, 21);
            this.comboBoxFormat.TabIndex = 14;
            // 
            // groupBoxMenü
            // 
            this.groupBoxMenü.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxMenü.Controls.Add(this.labelFormat);
            this.groupBoxMenü.Controls.Add(this.comboBoxFormat);
            this.groupBoxMenü.Controls.Add(this.buttonDownload);
            this.groupBoxMenü.Location = new System.Drawing.Point(18, 287);
            this.groupBoxMenü.Name = "groupBoxMenü";
            this.groupBoxMenü.Size = new System.Drawing.Size(432, 40);
            this.groupBoxMenü.TabIndex = 15;
            this.groupBoxMenü.TabStop = false;
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFormat.ForeColor = System.Drawing.Color.White;
            this.labelFormat.Location = new System.Drawing.Point(7, 15);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(50, 15);
            this.labelFormat.TabIndex = 15;
            this.labelFormat.Text = "Format:";
            // 
            // panelConfig
            // 
            this.panelConfig.BackColor = System.Drawing.Color.White;
            this.panelConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConfig.Controls.Add(this.buttonConfigSave);
            this.panelConfig.Controls.Add(this.labelConfigSave);
            this.panelConfig.Controls.Add(this.textBoxConfigSave);
            this.panelConfig.Controls.Add(this.labelConfigTitle);
            this.panelConfig.Location = new System.Drawing.Point(9, 43);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(638, 278);
            this.panelConfig.TabIndex = 11;
            this.panelConfig.Visible = false;
            // 
            // buttonConfigSave
            // 
            this.buttonConfigSave.Location = new System.Drawing.Point(498, 50);
            this.buttonConfigSave.Name = "buttonConfigSave";
            this.buttonConfigSave.Size = new System.Drawing.Size(128, 23);
            this.buttonConfigSave.TabIndex = 3;
            this.buttonConfigSave.Text = "button1";
            this.buttonConfigSave.UseVisualStyleBackColor = true;
            this.buttonConfigSave.Click += new System.EventHandler(this.buttonConfigSave_Click);
            // 
            // labelConfigSave
            // 
            this.labelConfigSave.AutoSize = true;
            this.labelConfigSave.Location = new System.Drawing.Point(6, 36);
            this.labelConfigSave.Name = "labelConfigSave";
            this.labelConfigSave.Size = new System.Drawing.Size(38, 13);
            this.labelConfigSave.TabIndex = 2;
            this.labelConfigSave.Text = "label4";
            // 
            // textBoxConfigSave
            // 
            this.textBoxConfigSave.Location = new System.Drawing.Point(9, 52);
            this.textBoxConfigSave.Name = "textBoxConfigSave";
            this.textBoxConfigSave.ReadOnly = true;
            this.textBoxConfigSave.Size = new System.Drawing.Size(483, 22);
            this.textBoxConfigSave.TabIndex = 1;
            // 
            // labelConfigTitle
            // 
            this.labelConfigTitle.AutoSize = true;
            this.labelConfigTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConfigTitle.Location = new System.Drawing.Point(5, 8);
            this.labelConfigTitle.Name = "labelConfigTitle";
            this.labelConfigTitle.Size = new System.Drawing.Size(56, 24);
            this.labelConfigTitle.TabIndex = 0;
            this.labelConfigTitle.Text = "Einst";
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.ForeColor = System.Drawing.Color.White;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(18, 269);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(123, 17);
            this.checkBoxSelectAll.TabIndex = 16;
            this.checkBoxSelectAll.Text = "Select / deselect all";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.Click += new System.EventHandler(this.checkBoxSelectAll_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSearch.Controls.Add(this.radioButtonSearchNew);
            this.groupBoxSearch.Controls.Add(this.radioButtonSearchClassic);
            this.groupBoxSearch.Controls.Add(this.textBoxBase);
            this.groupBoxSearch.Controls.Add(this.textBoxUrl);
            this.groupBoxSearch.Controls.Add(this.SearchSongs);
            this.groupBoxSearch.Location = new System.Drawing.Point(9, 25);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(442, 65);
            this.groupBoxSearch.TabIndex = 16;
            this.groupBoxSearch.TabStop = false;
            // 
            // radioButtonSearchClassic
            // 
            this.radioButtonSearchClassic.AutoSize = true;
            this.radioButtonSearchClassic.Checked = true;
            this.radioButtonSearchClassic.Location = new System.Drawing.Point(6, 40);
            this.radioButtonSearchClassic.Name = "radioButtonSearchClassic";
            this.radioButtonSearchClassic.Size = new System.Drawing.Size(105, 17);
            this.radioButtonSearchClassic.TabIndex = 9;
            this.radioButtonSearchClassic.TabStop = true;
            this.radioButtonSearchClassic.Text = "Classic myspace";
            this.radioButtonSearchClassic.UseVisualStyleBackColor = true;
            this.radioButtonSearchClassic.CheckedChanged += new System.EventHandler(this.radioButtonSearchClassic_CheckedChanged);
            // 
            // radioButtonSearchNew
            // 
            this.radioButtonSearchNew.AutoSize = true;
            this.radioButtonSearchNew.Location = new System.Drawing.Point(117, 40);
            this.radioButtonSearchNew.Name = "radioButtonSearchNew";
            this.radioButtonSearchNew.Size = new System.Drawing.Size(114, 17);
            this.radioButtonSearchNew.TabIndex = 10;
            this.radioButtonSearchNew.Text = "The new myspace";
            this.radioButtonSearchNew.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::MySpaceLoader.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(656, 340);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.checkBoxSelectAll);
            this.Controls.Add(this.groupBoxMenü);
            this.Controls.Add(this.panelstatus);
            this.Controls.Add(this.transparentLabel1);
            this.Controls.Add(this.panelControlBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewSongs);
            this.Controls.Add(this.panelConfig);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(656, 340);
            this.MinimumSize = new System.Drawing.Size(656, 340);
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.Text = "MySpace Downloader by www.code-bude.net";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
            this.panelstatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelControlBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSongs)).EndInit();
            this.groupBoxMenü.ResumeLayout(false);
            this.groupBoxMenü.PerformLayout();
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button SearchSongs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private MySpaceLoader.HelperObjects.TransparentLabel label2;
        private MySpaceLoader.HelperObjects.TransparentLabel label3;
        private System.Windows.Forms.Panel panelstatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelControlBar;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxMinimize;
        private HelperObjects.TransparentLabel transparentLabel1;
        private System.Windows.Forms.PictureBox pictureBoxDe;
        private System.Windows.Forms.PictureBox pictureBoxEn;
        private System.Windows.Forms.DataGridView dataGridViewSongs;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Label labelStatusSong;
        private System.Windows.Forms.ProgressBar progressBarSong;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.GroupBox groupBoxMenü;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.PictureBox pictureBoxConfig;
        private System.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.Label labelConfigTitle;
        private System.Windows.Forms.Button buttonConfigSave;
        private System.Windows.Forms.Label labelConfigSave;
        private System.Windows.Forms.TextBox textBoxConfigSave;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.PictureBox pictureBoxHelp;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.RadioButton radioButtonSearchNew;
        private System.Windows.Forms.RadioButton radioButtonSearchClassic;
    }
}

