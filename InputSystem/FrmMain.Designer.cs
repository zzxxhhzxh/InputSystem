namespace InputSystem
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnSimulate = new System.Windows.Forms.Button();
            this.icnNote = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.rtxLog = new System.Windows.Forms.RichTextBox();
            this.chkKeyInput = new System.Windows.Forms.CheckBox();
            this.chkMouseInput = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSimulate
            // 
            this.btnSimulate.Location = new System.Drawing.Point(236, 197);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(75, 23);
            this.btnSimulate.TabIndex = 0;
            this.btnSimulate.Text = "Simulate";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.BtnSimulate_Click);
            // 
            // icnNote
            // 
            this.icnNote.Icon = ((System.Drawing.Icon)(resources.GetObject("icnNote.Icon")));
            this.icnNote.Text = "Input System";
            this.icnNote.Visible = true;
            this.icnNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.IcnNote_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(155, 168);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(236, 168);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // rtxLog
            // 
            this.rtxLog.Location = new System.Drawing.Point(12, 12);
            this.rtxLog.Name = "rtxLog";
            this.rtxLog.Size = new System.Drawing.Size(300, 150);
            this.rtxLog.TabIndex = 3;
            this.rtxLog.Text = "";
            // 
            // chkKeyInput
            // 
            this.chkKeyInput.AutoSize = true;
            this.chkKeyInput.Checked = true;
            this.chkKeyInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeyInput.Location = new System.Drawing.Point(12, 172);
            this.chkKeyInput.Name = "chkKeyInput";
            this.chkKeyInput.Size = new System.Drawing.Size(71, 17);
            this.chkKeyInput.TabIndex = 4;
            this.chkKeyInput.Text = "Key Input";
            this.chkKeyInput.UseVisualStyleBackColor = true;
            this.chkKeyInput.CheckedChanged += new System.EventHandler(this.ChkKeyInput_CheckedChanged);
            // 
            // chkMouseInput
            // 
            this.chkMouseInput.AutoSize = true;
            this.chkMouseInput.Checked = true;
            this.chkMouseInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMouseInput.Location = new System.Drawing.Point(12, 201);
            this.chkMouseInput.Name = "chkMouseInput";
            this.chkMouseInput.Size = new System.Drawing.Size(85, 17);
            this.chkMouseInput.TabIndex = 6;
            this.chkMouseInput.Text = "Mouse Input";
            this.chkMouseInput.UseVisualStyleBackColor = true;
            this.chkMouseInput.CheckedChanged += new System.EventHandler(this.ChkMouseInput_CheckedChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 232);
            this.Controls.Add(this.chkMouseInput);
            this.Controls.Add(this.chkKeyInput);
            this.Controls.Add(this.rtxLog);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSimulate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input System";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.NotifyIcon icnNote;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox rtxLog;
        private System.Windows.Forms.CheckBox chkKeyInput;
        private System.Windows.Forms.CheckBox chkMouseInput;
    }
}

