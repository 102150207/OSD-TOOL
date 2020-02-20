namespace OSD_Tool.Screens.Controls
{
    partial class WorkingHourControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelResult = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExportFile = new System.Windows.Forms.Button();
            this.iconSuccess = new System.Windows.Forms.PictureBox();
            this.panelSelectFile = new System.Windows.Forms.Panel();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lbCurrentSheet = new System.Windows.Forms.Label();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panelResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconSuccess)).BeginInit();
            this.panelSelectFile.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panelResult);
            this.panel2.Controls.Add(this.panelSelectFile);
            this.panel2.Controls.Add(this.panelProgress);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 474);
            this.panel2.TabIndex = 11;
            // 
            // panelResult
            // 
            this.panelResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelResult.BackColor = System.Drawing.Color.Transparent;
            this.panelResult.Controls.Add(this.label2);
            this.panelResult.Controls.Add(this.buttonExportFile);
            this.panelResult.Controls.Add(this.iconSuccess);
            this.panelResult.Location = new System.Drawing.Point(118, 88);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(549, 322);
            this.panelResult.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 185);
            this.label2.MinimumSize = new System.Drawing.Size(500, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(500, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Well done :). Your file has been created. Click Save File to save.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonExportFile
            // 
            this.buttonExportFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonExportFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExportFile.Image = global::OSD_Tool.Properties.Resources.export;
            this.buttonExportFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExportFile.Location = new System.Drawing.Point(232, 238);
            this.buttonExportFile.Name = "buttonExportFile";
            this.buttonExportFile.Size = new System.Drawing.Size(106, 37);
            this.buttonExportFile.TabIndex = 0;
            this.buttonExportFile.Text = "     Save File";
            this.buttonExportFile.UseVisualStyleBackColor = true;
            this.buttonExportFile.Click += new System.EventHandler(this.buttonExportFile_Click);
            // 
            // iconSuccess
            // 
            this.iconSuccess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconSuccess.Image = global::OSD_Tool.Properties.Resources.success;
            this.iconSuccess.Location = new System.Drawing.Point(217, 29);
            this.iconSuccess.Name = "iconSuccess";
            this.iconSuccess.Size = new System.Drawing.Size(121, 117);
            this.iconSuccess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconSuccess.TabIndex = 2;
            this.iconSuccess.TabStop = false;
            // 
            // panelSelectFile
            // 
            this.panelSelectFile.BackColor = System.Drawing.Color.Transparent;
            this.panelSelectFile.Controls.Add(this.btnSelectFile);
            this.panelSelectFile.Controls.Add(this.lbCurrentSheet);
            this.panelSelectFile.Location = new System.Drawing.Point(30, 13);
            this.panelSelectFile.Name = "panelSelectFile";
            this.panelSelectFile.Size = new System.Drawing.Size(120, 60);
            this.panelSelectFile.TabIndex = 9;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.Image = global::OSD_Tool.Properties.Resources.import;
            this.btnSelectFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectFile.Location = new System.Drawing.Point(3, 12);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(114, 38);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "     Choose File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lbCurrentSheet
            // 
            this.lbCurrentSheet.AutoSize = true;
            this.lbCurrentSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbCurrentSheet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbCurrentSheet.Location = new System.Drawing.Point(346, 18);
            this.lbCurrentSheet.Name = "lbCurrentSheet";
            this.lbCurrentSheet.Size = new System.Drawing.Size(0, 13);
            this.lbCurrentSheet.TabIndex = 7;
            // 
            // panelProgress
            // 
            this.panelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgress.BackColor = System.Drawing.Color.Transparent;
            this.panelProgress.Controls.Add(this.label1);
            this.panelProgress.Controls.Add(this.progressBarLoading);
            this.panelProgress.Controls.Add(this.lbStatus);
            this.panelProgress.Location = new System.Drawing.Point(127, 88);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(529, 333);
            this.panelProgress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(89, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "This proccess can be finish after a few minutes. Please wait...";
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBarLoading.Location = new System.Drawing.Point(107, 192);
            this.progressBarLoading.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(329, 10);
            this.progressBarLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoading.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbStatus.AutoSize = true;
            this.lbStatus.ForeColor = System.Drawing.Color.White;
            this.lbStatus.Location = new System.Drawing.Point(22, 149);
            this.lbStatus.MinimumSize = new System.Drawing.Size(500, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(500, 17);
            this.lbStatus.TabIndex = 2;
            this.lbStatus.Text = "File is being read...";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkingHourControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(49)))));
            this.Controls.Add(this.panel2);
            this.Name = "WorkingHourControl";
            this.Size = new System.Drawing.Size(798, 474);
            this.panel2.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.panelResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconSuccess)).EndInit();
            this.panelSelectFile.ResumeLayout(false);
            this.panelSelectFile.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox iconSuccess;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Panel panelSelectFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lbCurrentSheet;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Button buttonExportFile;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Label label2;
    }
}
