namespace Croaker
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
            this.grpGames = new System.Windows.Forms.GroupBox();
            this.nudResponseInterval = new System.Windows.Forms.NumericUpDown();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnAddNameToWatch = new System.Windows.Forms.Button();
            this.txtNameToWatch = new System.Windows.Forms.TextBox();
            this.lstNamesToWatch = new System.Windows.Forms.ListBox();
            this.rdoScramble = new System.Windows.Forms.RadioButton();
            this.rdoMath = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.grpSendText = new System.Windows.Forms.GroupBox();
            this.btnSendText = new System.Windows.Forms.Button();
            this.txtTextToSend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpModWatch = new System.Windows.Forms.GroupBox();
            this.btnModWatch = new System.Windows.Forms.Button();
            this.grpChatRepeater = new System.Windows.Forms.GroupBox();
            this.chkBackwards = new System.Windows.Forms.CheckBox();
            this.btnStartStopRepeat = new System.Windows.Forms.Button();
            this.txtNameToRepeat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpMicGuard = new System.Windows.Forms.GroupBox();
            this.btnMicGuard = new System.Windows.Forms.Button();
            this.lvMicUsage = new System.Windows.Forms.ListView();
            this.hdrNick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hdrTotalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResponseInterval)).BeginInit();
            this.grpSendText.SuspendLayout();
            this.grpModWatch.SuspendLayout();
            this.grpChatRepeater.SuspendLayout();
            this.grpMicGuard.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGames
            // 
            this.grpGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGames.Controls.Add(this.nudResponseInterval);
            this.grpGames.Controls.Add(this.btnPlay);
            this.grpGames.Controls.Add(this.btnAddNameToWatch);
            this.grpGames.Controls.Add(this.txtNameToWatch);
            this.grpGames.Controls.Add(this.lstNamesToWatch);
            this.grpGames.Controls.Add(this.rdoScramble);
            this.grpGames.Controls.Add(this.rdoMath);
            this.grpGames.Controls.Add(this.label1);
            this.grpGames.Location = new System.Drawing.Point(13, 13);
            this.grpGames.Name = "grpGames";
            this.grpGames.Size = new System.Drawing.Size(418, 128);
            this.grpGames.TabIndex = 0;
            this.grpGames.TabStop = false;
            this.grpGames.Text = "Games";
            // 
            // nudResponseInterval
            // 
            this.nudResponseInterval.Location = new System.Drawing.Point(130, 46);
            this.nudResponseInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudResponseInterval.Name = "nudResponseInterval";
            this.nudResponseInterval.Size = new System.Drawing.Size(68, 20);
            this.nudResponseInterval.TabIndex = 8;
            this.nudResponseInterval.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.Location = new System.Drawing.Point(9, 99);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Start";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnAddNameToWatch
            // 
            this.btnAddNameToWatch.Location = new System.Drawing.Point(376, 19);
            this.btnAddNameToWatch.Name = "btnAddNameToWatch";
            this.btnAddNameToWatch.Size = new System.Drawing.Size(36, 21);
            this.btnAddNameToWatch.TabIndex = 7;
            this.btnAddNameToWatch.Text = "add";
            this.btnAddNameToWatch.UseVisualStyleBackColor = true;
            this.btnAddNameToWatch.Click += new System.EventHandler(this.btnAddNameToWatch_Click);
            // 
            // txtNameToWatch
            // 
            this.txtNameToWatch.Location = new System.Drawing.Point(235, 20);
            this.txtNameToWatch.Name = "txtNameToWatch";
            this.txtNameToWatch.Size = new System.Drawing.Size(141, 20);
            this.txtNameToWatch.TabIndex = 6;
            // 
            // lstNamesToWatch
            // 
            this.lstNamesToWatch.FormattingEnabled = true;
            this.lstNamesToWatch.Location = new System.Drawing.Point(235, 40);
            this.lstNamesToWatch.Name = "lstNamesToWatch";
            this.lstNamesToWatch.Size = new System.Drawing.Size(177, 82);
            this.lstNamesToWatch.TabIndex = 5;
            this.lstNamesToWatch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstNamesToWatch_KeyDown);
            // 
            // rdoScramble
            // 
            this.rdoScramble.AutoSize = true;
            this.rdoScramble.Location = new System.Drawing.Point(64, 19);
            this.rdoScramble.Name = "rdoScramble";
            this.rdoScramble.Size = new System.Drawing.Size(98, 17);
            this.rdoScramble.TabIndex = 3;
            this.rdoScramble.Text = "Word Scramble";
            this.rdoScramble.UseVisualStyleBackColor = true;
            // 
            // rdoMath
            // 
            this.rdoMath.AutoSize = true;
            this.rdoMath.Checked = true;
            this.rdoMath.Location = new System.Drawing.Point(9, 19);
            this.rdoMath.Name = "rdoMath";
            this.rdoMath.Size = new System.Drawing.Size(49, 17);
            this.rdoMath.TabIndex = 2;
            this.rdoMath.TabStop = true;
            this.rdoMath.Text = "Math";
            this.rdoMath.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Response Interval (ms):";
            // 
            // grpSendText
            // 
            this.grpSendText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSendText.Controls.Add(this.btnSendText);
            this.grpSendText.Controls.Add(this.txtTextToSend);
            this.grpSendText.Controls.Add(this.label2);
            this.grpSendText.Location = new System.Drawing.Point(13, 449);
            this.grpSendText.Name = "grpSendText";
            this.grpSendText.Size = new System.Drawing.Size(418, 84);
            this.grpSendText.TabIndex = 1;
            this.grpSendText.TabStop = false;
            this.grpSendText.Text = "Send";
            // 
            // btnSendText
            // 
            this.btnSendText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendText.Location = new System.Drawing.Point(9, 55);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(75, 23);
            this.btnSendText.TabIndex = 1;
            this.btnSendText.Text = "Send";
            this.btnSendText.UseVisualStyleBackColor = true;
            // 
            // txtTextToSend
            // 
            this.txtTextToSend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextToSend.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextToSend.Location = new System.Drawing.Point(88, 24);
            this.txtTextToSend.Multiline = true;
            this.txtTextToSend.Name = "txtTextToSend";
            this.txtTextToSend.Size = new System.Drawing.Size(324, 54);
            this.txtTextToSend.TabIndex = 0;
            this.txtTextToSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTextToSend_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Text To Send:";
            // 
            // grpModWatch
            // 
            this.grpModWatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModWatch.Controls.Add(this.btnModWatch);
            this.grpModWatch.Location = new System.Drawing.Point(11, 147);
            this.grpModWatch.Name = "grpModWatch";
            this.grpModWatch.Size = new System.Drawing.Size(420, 63);
            this.grpModWatch.TabIndex = 2;
            this.grpModWatch.TabStop = false;
            this.grpModWatch.Text = "Mod Watch";
            // 
            // btnModWatch
            // 
            this.btnModWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnModWatch.Location = new System.Drawing.Point(11, 31);
            this.btnModWatch.Name = "btnModWatch";
            this.btnModWatch.Size = new System.Drawing.Size(115, 23);
            this.btnModWatch.TabIndex = 0;
            this.btnModWatch.Text = "Start Mod Watch";
            this.btnModWatch.UseVisualStyleBackColor = true;
            this.btnModWatch.Click += new System.EventHandler(this.btnModWatch_Click);
            // 
            // grpChatRepeater
            // 
            this.grpChatRepeater.Controls.Add(this.chkBackwards);
            this.grpChatRepeater.Controls.Add(this.btnStartStopRepeat);
            this.grpChatRepeater.Controls.Add(this.txtNameToRepeat);
            this.grpChatRepeater.Controls.Add(this.label3);
            this.grpChatRepeater.Location = new System.Drawing.Point(13, 216);
            this.grpChatRepeater.Name = "grpChatRepeater";
            this.grpChatRepeater.Size = new System.Drawing.Size(418, 106);
            this.grpChatRepeater.TabIndex = 3;
            this.grpChatRepeater.TabStop = false;
            this.grpChatRepeater.Text = "Chat Repeater";
            // 
            // chkBackwards
            // 
            this.chkBackwards.AutoSize = true;
            this.chkBackwards.Location = new System.Drawing.Point(104, 53);
            this.chkBackwards.Name = "chkBackwards";
            this.chkBackwards.Size = new System.Drawing.Size(79, 17);
            this.chkBackwards.TabIndex = 3;
            this.chkBackwards.Text = "Backwards";
            this.chkBackwards.UseVisualStyleBackColor = true;
            // 
            // btnStartStopRepeat
            // 
            this.btnStartStopRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStopRepeat.Location = new System.Drawing.Point(9, 77);
            this.btnStartStopRepeat.Name = "btnStartStopRepeat";
            this.btnStartStopRepeat.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopRepeat.TabIndex = 2;
            this.btnStartStopRepeat.Text = "Start Repeat";
            this.btnStartStopRepeat.UseVisualStyleBackColor = true;
            this.btnStartStopRepeat.Click += new System.EventHandler(this.btnStartStopRepeat_Click);
            // 
            // txtNameToRepeat
            // 
            this.txtNameToRepeat.Location = new System.Drawing.Point(104, 27);
            this.txtNameToRepeat.Name = "txtNameToRepeat";
            this.txtNameToRepeat.Size = new System.Drawing.Size(221, 20);
            this.txtNameToRepeat.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name To Repeat:";
            // 
            // grpMicGuard
            // 
            this.grpMicGuard.Controls.Add(this.lvMicUsage);
            this.grpMicGuard.Controls.Add(this.btnMicGuard);
            this.grpMicGuard.Location = new System.Drawing.Point(13, 328);
            this.grpMicGuard.Name = "grpMicGuard";
            this.grpMicGuard.Size = new System.Drawing.Size(418, 115);
            this.grpMicGuard.TabIndex = 4;
            this.grpMicGuard.TabStop = false;
            this.grpMicGuard.Text = "Mic Guard";
            // 
            // btnMicGuard
            // 
            this.btnMicGuard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMicGuard.Location = new System.Drawing.Point(6, 19);
            this.btnMicGuard.Name = "btnMicGuard";
            this.btnMicGuard.Size = new System.Drawing.Size(92, 23);
            this.btnMicGuard.TabIndex = 0;
            this.btnMicGuard.Text = "Start Guarding";
            this.btnMicGuard.UseVisualStyleBackColor = true;
            this.btnMicGuard.Click += new System.EventHandler(this.btnMicGuard_Click);
            // 
            // lvMicUsage
            // 
            this.lvMicUsage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrNick,
            this.hdrTotalTime});
            this.lvMicUsage.FullRowSelect = true;
            this.lvMicUsage.Location = new System.Drawing.Point(104, 19);
            this.lvMicUsage.Name = "lvMicUsage";
            this.lvMicUsage.Size = new System.Drawing.Size(308, 90);
            this.lvMicUsage.TabIndex = 1;
            this.lvMicUsage.UseCompatibleStateImageBehavior = false;
            this.lvMicUsage.View = System.Windows.Forms.View.Details;
            // 
            // hdrNick
            // 
            this.hdrNick.Text = "Nick Name";
            this.hdrNick.Width = 177;
            // 
            // hdrTotalTime
            // 
            this.hdrTotalTime.Text = "Total Time";
            this.hdrTotalTime.Width = 110;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 545);
            this.Controls.Add(this.grpMicGuard);
            this.Controls.Add(this.grpChatRepeater);
            this.Controls.Add(this.grpModWatch);
            this.Controls.Add(this.grpSendText);
            this.Controls.Add(this.grpGames);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Croaker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpGames.ResumeLayout(false);
            this.grpGames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResponseInterval)).EndInit();
            this.grpSendText.ResumeLayout(false);
            this.grpSendText.PerformLayout();
            this.grpModWatch.ResumeLayout(false);
            this.grpChatRepeater.ResumeLayout(false);
            this.grpChatRepeater.PerformLayout();
            this.grpMicGuard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGames;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnAddNameToWatch;
        private System.Windows.Forms.TextBox txtNameToWatch;
        private System.Windows.Forms.ListBox lstNamesToWatch;
        private System.Windows.Forms.RadioButton rdoScramble;
        private System.Windows.Forms.RadioButton rdoMath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpSendText;
        private System.Windows.Forms.Button btnSendText;
        private System.Windows.Forms.TextBox txtTextToSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpModWatch;
        private System.Windows.Forms.Button btnModWatch;
        private System.Windows.Forms.NumericUpDown nudResponseInterval;
        private System.Windows.Forms.GroupBox grpChatRepeater;
        private System.Windows.Forms.CheckBox chkBackwards;
        private System.Windows.Forms.Button btnStartStopRepeat;
        private System.Windows.Forms.TextBox txtNameToRepeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpMicGuard;
        private System.Windows.Forms.Button btnMicGuard;
        private System.Windows.Forms.ListView lvMicUsage;
        private System.Windows.Forms.ColumnHeader hdrNick;
        private System.Windows.Forms.ColumnHeader hdrTotalTime;
    }
}

