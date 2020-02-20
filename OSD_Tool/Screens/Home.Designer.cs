namespace OSD_Tool.Screens
{
    partial class Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelActiveWH = new System.Windows.Forms.Panel();
            this.panelActiveAnnualLeave = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMenuWorkingHour = new System.Windows.Forms.Button();
            this.btnMenuAnnualLeave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.workingHourControl = new OSD_Tool.Screens.Controls.WorkingHourControl();
            this.annualLeaveCtr = new OSD_Tool.Screens.AnnualLeaveCtr();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panelActiveWH);
            this.panel1.Controls.Add(this.panelActiveAnnualLeave);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnMenuWorkingHour);
            this.panel1.Controls.Add(this.btnMenuAnnualLeave);
            this.panel1.Location = new System.Drawing.Point(0, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 777);
            this.panel1.TabIndex = 0;
            // 
            // panelActiveWH
            // 
            this.panelActiveWH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(82)))), ((int)(((byte)(64)))));
            this.panelActiveWH.Location = new System.Drawing.Point(0, 135);
            this.panelActiveWH.Name = "panelActiveWH";
            this.panelActiveWH.Size = new System.Drawing.Size(10, 49);
            this.panelActiveWH.TabIndex = 4;
            // 
            // panelActiveAnnualLeave
            // 
            this.panelActiveAnnualLeave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(82)))), ((int)(((byte)(64)))));
            this.panelActiveAnnualLeave.Location = new System.Drawing.Point(0, 86);
            this.panelActiveAnnualLeave.Name = "panelActiveAnnualLeave";
            this.panelActiveAnnualLeave.Size = new System.Drawing.Size(10, 49);
            this.panelActiveAnnualLeave.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::OSD_Tool.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(189, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnMenuWorkingHour
            // 
            this.btnMenuWorkingHour.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuWorkingHour.FlatAppearance.BorderSize = 0;
            this.btnMenuWorkingHour.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(28)))), ((int)(((byte)(31)))));
            this.btnMenuWorkingHour.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(28)))), ((int)(((byte)(31)))));
            this.btnMenuWorkingHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuWorkingHour.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuWorkingHour.ForeColor = System.Drawing.Color.White;
            this.btnMenuWorkingHour.Image = global::OSD_Tool.Properties.Resources.working_hours;
            this.btnMenuWorkingHour.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuWorkingHour.Location = new System.Drawing.Point(10, 135);
            this.btnMenuWorkingHour.Name = "btnMenuWorkingHour";
            this.btnMenuWorkingHour.Size = new System.Drawing.Size(183, 49);
            this.btnMenuWorkingHour.TabIndex = 2;
            this.btnMenuWorkingHour.Text = "         Working Hours";
            this.btnMenuWorkingHour.UseVisualStyleBackColor = true;
            this.btnMenuWorkingHour.Click += new System.EventHandler(this.btnMenuWorkingHour_Click);
            // 
            // btnMenuAnnualLeave
            // 
            this.btnMenuAnnualLeave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuAnnualLeave.FlatAppearance.BorderSize = 0;
            this.btnMenuAnnualLeave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(28)))), ((int)(((byte)(31)))));
            this.btnMenuAnnualLeave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(28)))), ((int)(((byte)(31)))));
            this.btnMenuAnnualLeave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuAnnualLeave.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAnnualLeave.ForeColor = System.Drawing.Color.White;
            this.btnMenuAnnualLeave.Image = global::OSD_Tool.Properties.Resources.leave;
            this.btnMenuAnnualLeave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuAnnualLeave.Location = new System.Drawing.Point(10, 86);
            this.btnMenuAnnualLeave.Name = "btnMenuAnnualLeave";
            this.btnMenuAnnualLeave.Size = new System.Drawing.Size(183, 49);
            this.btnMenuAnnualLeave.TabIndex = 0;
            this.btnMenuAnnualLeave.Text = "         Annual Leave";
            this.btnMenuAnnualLeave.UseVisualStyleBackColor = true;
            this.btnMenuAnnualLeave.Click += new System.EventHandler(this.btnMenuAnnualLeave_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(82)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.btnMinimize);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 31);
            this.panel2.TabIndex = 1;
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Image = global::OSD_Tool.Properties.Resources.minimize;
            this.btnMinimize.Location = new System.Drawing.Point(1290, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(34, 30);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = global::OSD_Tool.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(1323, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(37, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1360, 809);
            this.panel3.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(82)))), ((int)(((byte)(64)))));
            this.panel6.Location = new System.Drawing.Point(191, 31);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(2, 778);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.workingHourControl);
            this.panel5.Controls.Add(this.annualLeaveCtr);
            this.panel5.Location = new System.Drawing.Point(195, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1165, 778);
            this.panel5.TabIndex = 2;
            // 
            // workingHourControl
            // 
            this.workingHourControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(49)))));
            this.workingHourControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workingHourControl.Location = new System.Drawing.Point(0, 0);
            this.workingHourControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.workingHourControl.Name = "workingHourControl";
            this.workingHourControl.Size = new System.Drawing.Size(1165, 778);
            this.workingHourControl.TabIndex = 1;
            // 
            // annualLeaveCtr
            // 
            this.annualLeaveCtr.AutoSize = true;
            this.annualLeaveCtr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.annualLeaveCtr.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.annualLeaveCtr.Location = new System.Drawing.Point(0, 0);
            this.annualLeaveCtr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.annualLeaveCtr.Name = "annualLeaveCtr";
            this.annualLeaveCtr.Size = new System.Drawing.Size(1165, 778);
            this.annualLeaveCtr.TabIndex = 0;
            // 
            // Home2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1360, 809);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Home2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMenuAnnualLeave;
        private System.Windows.Forms.Button btnMenuWorkingHour;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelActiveAnnualLeave;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel6;
        private AnnualLeaveCtr annualLeaveCtr;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel panelActiveWH;
        private Controls.WorkingHourControl workingHourControl;
    }
}