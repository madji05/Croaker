namespace Croaker_Mic_Watcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgvMicUsage = new System.Windows.Forms.DataGridView();
            this.nickNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.micWatcherDataSet1 = new Croaker_Mic_Watcher.MicWatcherDataSet();
            this.btnStartStopWatching = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstSafeList = new System.Windows.Forms.ListBox();
            this.txtSafeListNew = new System.Windows.Forms.TextBox();
            this.btnSafeListAdd = new System.Windows.Forms.Button();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.btnClearSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.chkSendToRoom = new System.Windows.Forms.CheckBox();
            this.dtpResetInterval = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTimeOnMic = new System.Windows.Forms.Label();
            this.nudBlockPeriod = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAutoUnBlock = new System.Windows.Forms.CheckBox();
            this.dtpBlockThreshold = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpWarnThreshold = new System.Windows.Forms.DateTimePicker();
            this.Label3 = new System.Windows.Forms.Label();
            this.lblCurrentlyOnMic = new System.Windows.Forms.Label();
            this.toolTipper = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMicUsage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.micWatcherDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMicUsage
            // 
            this.dgvMicUsage.AllowUserToAddRows = false;
            this.dgvMicUsage.AllowUserToDeleteRows = false;
            this.dgvMicUsage.AllowUserToResizeRows = false;
            this.dgvMicUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMicUsage.AutoGenerateColumns = false;
            this.dgvMicUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMicUsage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nickNameDataGridViewTextBoxColumn,
            this.totalTimeDataGridViewTextBoxColumn});
            this.dgvMicUsage.DataSource = this.bindingSource1;
            this.dgvMicUsage.Location = new System.Drawing.Point(12, 36);
            this.dgvMicUsage.MultiSelect = false;
            this.dgvMicUsage.Name = "dgvMicUsage";
            this.dgvMicUsage.ReadOnly = true;
            this.dgvMicUsage.RowHeadersVisible = false;
            this.dgvMicUsage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMicUsage.ShowCellErrors = false;
            this.dgvMicUsage.ShowCellToolTips = false;
            this.dgvMicUsage.ShowEditingIcon = false;
            this.dgvMicUsage.ShowRowErrors = false;
            this.dgvMicUsage.Size = new System.Drawing.Size(526, 346);
            this.dgvMicUsage.TabIndex = 0;
            this.toolTipper.SetToolTip(this.dgvMicUsage, "Top results of saved mic sessions");
            this.dgvMicUsage.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMicUsage_CellFormatting);
            // 
            // nickNameDataGridViewTextBoxColumn
            // 
            this.nickNameDataGridViewTextBoxColumn.DataPropertyName = "NickName";
            this.nickNameDataGridViewTextBoxColumn.HeaderText = "NickName";
            this.nickNameDataGridViewTextBoxColumn.Name = "nickNameDataGridViewTextBoxColumn";
            this.nickNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nickNameDataGridViewTextBoxColumn.Width = 300;
            // 
            // totalTimeDataGridViewTextBoxColumn
            // 
            this.totalTimeDataGridViewTextBoxColumn.DataPropertyName = "TotalTime";
            dataGridViewCellStyle1.Format = "HH:mm:ss";
            this.totalTimeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.totalTimeDataGridViewTextBoxColumn.HeaderText = "TotalTime";
            this.totalTimeDataGridViewTextBoxColumn.Name = "totalTimeDataGridViewTextBoxColumn";
            this.totalTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.totalTimeDataGridViewTextBoxColumn.Width = 200;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "Totals";
            this.bindingSource1.DataSource = this.micWatcherDataSet1;
            // 
            // micWatcherDataSet1
            // 
            this.micWatcherDataSet1.DataSetName = "MicWatcherDataSet";
            this.micWatcherDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnStartStopWatching
            // 
            this.btnStartStopWatching.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStopWatching.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStopWatching.Location = new System.Drawing.Point(411, 633);
            this.btnStartStopWatching.Name = "btnStartStopWatching";
            this.btnStartStopWatching.Size = new System.Drawing.Size(127, 28);
            this.btnStartStopWatching.TabIndex = 2;
            this.btnStartStopWatching.Text = "Start Watching";
            this.toolTipper.SetToolTip(this.btnStartStopWatching, "Start or Stop the mic watcher");
            this.btnStartStopWatching.UseVisualStyleBackColor = true;
            this.btnStartStopWatching.Click += new System.EventHandler(this.btnStartStopWatching_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Top ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Safe List";
            // 
            // lstSafeList
            // 
            this.lstSafeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSafeList.FormattingEnabled = true;
            this.lstSafeList.Location = new System.Drawing.Point(12, 407);
            this.lstSafeList.Name = "lstSafeList";
            this.lstSafeList.Size = new System.Drawing.Size(184, 199);
            this.lstSafeList.TabIndex = 4;
            this.toolTipper.SetToolTip(this.lstSafeList, "Select an item and press delete to remove an item");
            this.lstSafeList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstSafeList_KeyDown);
            // 
            // txtSafeListNew
            // 
            this.txtSafeListNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSafeListNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSafeListNew.Location = new System.Drawing.Point(12, 612);
            this.txtSafeListNew.Name = "txtSafeListNew";
            this.txtSafeListNew.Size = new System.Drawing.Size(143, 20);
            this.txtSafeListNew.TabIndex = 5;
            // 
            // btnSafeListAdd
            // 
            this.btnSafeListAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSafeListAdd.Location = new System.Drawing.Point(161, 609);
            this.btnSafeListAdd.Name = "btnSafeListAdd";
            this.btnSafeListAdd.Size = new System.Drawing.Size(35, 23);
            this.btnSafeListAdd.TabIndex = 6;
            this.btnSafeListAdd.Text = "add";
            this.toolTipper.SetToolTip(this.btnSafeListAdd, "Add a new item to the safe list");
            this.btnSafeListAdd.UseVisualStyleBackColor = true;
            this.btnSafeListAdd.Click += new System.EventHandler(this.btnSafeListAdd_Click);
            // 
            // nudTop
            // 
            this.nudTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTop.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTop.Location = new System.Drawing.Point(45, 13);
            this.nudTop.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudTop.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(56, 21);
            this.nudTop.TabIndex = 0;
            this.toolTipper.SetToolTip(this.nudTop, "Set top count to filter results");
            this.nudTop.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTop.ValueChanged += new System.EventHandler(this.nudTopCount_ValueChanged);
            // 
            // btnClearSave
            // 
            this.btnClearSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSave.Location = new System.Drawing.Point(442, 11);
            this.btnClearSave.Name = "btnClearSave";
            this.btnClearSave.Size = new System.Drawing.Size(96, 23);
            this.btnClearSave.TabIndex = 18;
            this.btnClearSave.Text = "Clear And Save";
            this.toolTipper.SetToolTip(this.btnClearSave, "Clear the session data and refresh the top list");
            this.btnClearSave.UseVisualStyleBackColor = true;
            this.btnClearSave.Click += new System.EventHandler(this.btnClearSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUserCount);
            this.panel1.Controls.Add(this.chkSendToRoom);
            this.panel1.Controls.Add(this.dtpResetInterval);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTimeOnMic);
            this.panel1.Controls.Add(this.nudBlockPeriod);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chkAutoUnBlock);
            this.panel1.Controls.Add(this.dtpBlockThreshold);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpWarnThreshold);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Controls.Add(this.lblCurrentlyOnMic);
            this.panel1.Location = new System.Drawing.Point(203, 407);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 220);
            this.panel1.TabIndex = 1;
            // 
            // lblUserCount
            // 
            this.lblUserCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserCount.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lblUserCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblUserCount.Location = new System.Drawing.Point(269, 26);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(61, 23);
            this.lblUserCount.TabIndex = 30;
            this.lblUserCount.Text = "Users (0)";
            this.lblUserCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkSendToRoom
            // 
            this.chkSendToRoom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSendToRoom.AutoSize = true;
            this.chkSendToRoom.Location = new System.Drawing.Point(109, 192);
            this.chkSendToRoom.Name = "chkSendToRoom";
            this.chkSendToRoom.Size = new System.Drawing.Size(98, 17);
            this.chkSendToRoom.TabIndex = 5;
            this.chkSendToRoom.Text = "Send To Room";
            this.toolTipper.SetToolTip(this.chkSendToRoom, "If checked then warnings and block text will be sent to room");
            this.chkSendToRoom.UseVisualStyleBackColor = true;
            this.chkSendToRoom.CheckedChanged += new System.EventHandler(this.chkSendToRoom_CheckedChanged);
            // 
            // dtpResetInterval
            // 
            this.dtpResetInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpResetInterval.CustomFormat = "HH:mm:ss";
            this.dtpResetInterval.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpResetInterval.Location = new System.Drawing.Point(108, 134);
            this.dtpResetInterval.Name = "dtpResetInterval";
            this.dtpResetInterval.ShowCheckBox = true;
            this.dtpResetInterval.ShowUpDown = true;
            this.dtpResetInterval.Size = new System.Drawing.Size(93, 20);
            this.dtpResetInterval.TabIndex = 3;
            this.dtpResetInterval.Value = new System.DateTime(2015, 2, 6, 0, 3, 0, 0);
            this.dtpResetInterval.ValueChanged += new System.EventHandler(this.dtpResetInterval_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Reset Interval (min)";
            // 
            // lblTimeOnMic
            // 
            this.lblTimeOnMic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTimeOnMic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTimeOnMic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeOnMic.Location = new System.Drawing.Point(269, 3);
            this.lblTimeOnMic.Name = "lblTimeOnMic";
            this.lblTimeOnMic.Size = new System.Drawing.Size(61, 21);
            this.lblTimeOnMic.TabIndex = 26;
            this.lblTimeOnMic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTipper.SetToolTip(this.lblTimeOnMic, "Current time on mic in seconds");
            // 
            // nudBlockPeriod
            // 
            this.nudBlockPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudBlockPeriod.Location = new System.Drawing.Point(109, 101);
            this.nudBlockPeriod.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudBlockPeriod.Name = "nudBlockPeriod";
            this.nudBlockPeriod.Size = new System.Drawing.Size(48, 20);
            this.nudBlockPeriod.TabIndex = 2;
            this.nudBlockPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBlockPeriod.ValueChanged += new System.EventHandler(this.nudBlockPeriod_ValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Block Period (min)";
            // 
            // chkAutoUnBlock
            // 
            this.chkAutoUnBlock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoUnBlock.AutoSize = true;
            this.chkAutoUnBlock.Checked = true;
            this.chkAutoUnBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUnBlock.Location = new System.Drawing.Point(109, 171);
            this.chkAutoUnBlock.Name = "chkAutoUnBlock";
            this.chkAutoUnBlock.Size = new System.Drawing.Size(92, 17);
            this.chkAutoUnBlock.TabIndex = 4;
            this.chkAutoUnBlock.Text = "Auto UnBlock";
            this.toolTipper.SetToolTip(this.chkAutoUnBlock, "Automatically unblocks users after the specified block period");
            this.chkAutoUnBlock.UseVisualStyleBackColor = true;
            this.chkAutoUnBlock.CheckedChanged += new System.EventHandler(this.chkAutoUnBlock_CheckedChanged);
            // 
            // dtpBlockThreshold
            // 
            this.dtpBlockThreshold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpBlockThreshold.CustomFormat = "HH:mm:ss";
            this.dtpBlockThreshold.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBlockThreshold.Location = new System.Drawing.Point(109, 68);
            this.dtpBlockThreshold.Name = "dtpBlockThreshold";
            this.dtpBlockThreshold.ShowCheckBox = true;
            this.dtpBlockThreshold.ShowUpDown = true;
            this.dtpBlockThreshold.Size = new System.Drawing.Size(97, 20);
            this.dtpBlockThreshold.TabIndex = 1;
            this.dtpBlockThreshold.Value = new System.DateTime(2015, 2, 6, 0, 1, 45, 0);
            this.dtpBlockThreshold.ValueChanged += new System.EventHandler(this.dtpBlockThreshold_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Block Threshold";
            // 
            // dtpWarnThreshold
            // 
            this.dtpWarnThreshold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpWarnThreshold.CustomFormat = "HH:mm:ss";
            this.dtpWarnThreshold.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWarnThreshold.Location = new System.Drawing.Point(109, 35);
            this.dtpWarnThreshold.Name = "dtpWarnThreshold";
            this.dtpWarnThreshold.ShowCheckBox = true;
            this.dtpWarnThreshold.ShowUpDown = true;
            this.dtpWarnThreshold.Size = new System.Drawing.Size(97, 20);
            this.dtpWarnThreshold.TabIndex = 0;
            this.dtpWarnThreshold.Value = new System.DateTime(2015, 2, 6, 0, 1, 0, 0);
            this.dtpWarnThreshold.ValueChanged += new System.EventHandler(this.dtpWarnThreshold_ValueChanged);
            // 
            // Label3
            // 
            this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 39);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(83, 13);
            this.Label3.TabIndex = 19;
            this.Label3.Text = "Warn Threshold";
            // 
            // lblCurrentlyOnMic
            // 
            this.lblCurrentlyOnMic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCurrentlyOnMic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentlyOnMic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyOnMic.Location = new System.Drawing.Point(3, 3);
            this.lblCurrentlyOnMic.Name = "lblCurrentlyOnMic";
            this.lblCurrentlyOnMic.Size = new System.Drawing.Size(260, 21);
            this.lblCurrentlyOnMic.TabIndex = 18;
            this.lblCurrentlyOnMic.Text = "Currently on mic:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 673);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClearSave);
            this.Controls.Add(this.nudTop);
            this.Controls.Add(this.btnSafeListAdd);
            this.Controls.Add(this.txtSafeListNew);
            this.Controls.Add(this.lstSafeList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartStopWatching);
            this.Controls.Add(this.dgvMicUsage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Croaker Mic Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMicUsage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.micWatcherDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMicUsage;
        private System.Windows.Forms.Button btnStartStopWatching;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstSafeList;
        private System.Windows.Forms.TextBox txtSafeListNew;
        private System.Windows.Forms.Button btnSafeListAdd;
        private System.Windows.Forms.BindingSource bindingSource1;
        private MicWatcherDataSet micWatcherDataSet1;
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn nickNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnClearSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudBlockPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAutoUnBlock;
        private System.Windows.Forms.DateTimePicker dtpBlockThreshold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpWarnThreshold;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label lblCurrentlyOnMic;
        private System.Windows.Forms.Label lblTimeOnMic;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkSendToRoom;
        private System.Windows.Forms.DateTimePicker dtpResetInterval;
        private System.Windows.Forms.ToolTip toolTipper;
        private System.Windows.Forms.Label lblUserCount;
    }
}

