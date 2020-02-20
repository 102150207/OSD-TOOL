namespace OSD_Tool.Screens
{
    partial class AnnualLeaveCtr
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.gridTimeLeave = new System.Windows.Forms.DataGridView();
            this.lblTimeOffError = new System.Windows.Forms.Label();
            this.btnApplyTimeOff = new System.Windows.Forms.Button();
            this.lbStaffName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeOff = new System.Windows.Forms.TextBox();
            this.dateTimePickerSelectDay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateStaff = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbEmployeeErr = new System.Windows.Forms.Label();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbEmGroup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.empStartingDate = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtStaffSearch = new System.Windows.Forms.TextBox();
            this.lbStaffSearch = new System.Windows.Forms.Label();
            this.empName = new System.Windows.Forms.TextBox();
            this.empCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.lbCurrentSheet = new System.Windows.Forms.Label();
            this.currentSheetLb = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridAnnualLeave = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeLeave)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAnnualLeave)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.gridTimeLeave);
            this.panel2.Controls.Add(this.lblTimeOffError);
            this.panel2.Controls.Add(this.btnApplyTimeOff);
            this.panel2.Controls.Add(this.lbStaffName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtTimeOff);
            this.panel2.Controls.Add(this.dateTimePickerSelectDay);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(1041, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 967);
            this.panel2.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(152, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Leave Time";
            // 
            // gridTimeLeave
            // 
            this.gridTimeLeave.AllowUserToAddRows = false;
            this.gridTimeLeave.AllowUserToOrderColumns = true;
            this.gridTimeLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTimeLeave.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTimeLeave.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridTimeLeave.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTimeLeave.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridTimeLeave.Location = new System.Drawing.Point(0, 272);
            this.gridTimeLeave.Margin = new System.Windows.Forms.Padding(2);
            this.gridTimeLeave.Name = "gridTimeLeave";
            this.gridTimeLeave.ReadOnly = true;
            this.gridTimeLeave.RowTemplate.Height = 24;
            this.gridTimeLeave.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTimeLeave.Size = new System.Drawing.Size(395, 690);
            this.gridTimeLeave.TabIndex = 15;
            this.gridTimeLeave.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTimeLeave_CellClick);
            // 
            // lblTimeOffError
            // 
            this.lblTimeOffError.AutoSize = true;
            this.lblTimeOffError.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeOffError.ForeColor = System.Drawing.Color.Red;
            this.lblTimeOffError.Location = new System.Drawing.Point(29, 206);
            this.lblTimeOffError.Name = "lblTimeOffError";
            this.lblTimeOffError.Size = new System.Drawing.Size(0, 19);
            this.lblTimeOffError.TabIndex = 14;
            // 
            // btnApplyTimeOff
            // 
            this.btnApplyTimeOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplyTimeOff.ForeColor = System.Drawing.Color.Black;
            this.btnApplyTimeOff.Location = new System.Drawing.Point(334, 113);
            this.btnApplyTimeOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnApplyTimeOff.Name = "btnApplyTimeOff";
            this.btnApplyTimeOff.Size = new System.Drawing.Size(62, 69);
            this.btnApplyTimeOff.TabIndex = 13;
            this.btnApplyTimeOff.Text = "Apply";
            this.btnApplyTimeOff.UseVisualStyleBackColor = true;
            this.btnApplyTimeOff.Click += new System.EventHandler(this.btnApplyTimeOff_Click);
            // 
            // lbStaffName
            // 
            this.lbStaffName.AutoSize = true;
            this.lbStaffName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStaffName.ForeColor = System.Drawing.Color.Red;
            this.lbStaffName.Location = new System.Drawing.Point(128, 76);
            this.lbStaffName.Name = "lbStaffName";
            this.lbStaffName.Size = new System.Drawing.Size(0, 17);
            this.lbStaffName.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Employee name:";
            // 
            // txtTimeOff
            // 
            this.txtTimeOff.Location = new System.Drawing.Point(131, 160);
            this.txtTimeOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimeOff.Name = "txtTimeOff";
            this.txtTimeOff.Size = new System.Drawing.Size(187, 21);
            this.txtTimeOff.TabIndex = 10;
            // 
            // dateTimePickerSelectDay
            // 
            this.dateTimePickerSelectDay.Location = new System.Drawing.Point(132, 116);
            this.dateTimePickerSelectDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePickerSelectDay.Name = "dateTimePickerSelectDay";
            this.dateTimePickerSelectDay.Size = new System.Drawing.Size(186, 21);
            this.dateTimePickerSelectDay.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Fill time off:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select day:";
            // 
            // btnUpdateStaff
            // 
            this.btnUpdateStaff.Location = new System.Drawing.Point(581, 131);
            this.btnUpdateStaff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateStaff.Name = "btnUpdateStaff";
            this.btnUpdateStaff.Size = new System.Drawing.Size(89, 24);
            this.btnUpdateStaff.TabIndex = 14;
            this.btnUpdateStaff.Text = "Update";
            this.btnUpdateStaff.UseVisualStyleBackColor = true;
            this.btnUpdateStaff.Click += new System.EventHandler(this.btnUpdateStaff_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(58, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "(*)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(48, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "(*)";
            // 
            // lbEmployeeErr
            // 
            this.lbEmployeeErr.AutoSize = true;
            this.lbEmployeeErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmployeeErr.ForeColor = System.Drawing.Color.Red;
            this.lbEmployeeErr.Location = new System.Drawing.Point(114, 164);
            this.lbEmployeeErr.Name = "lbEmployeeErr";
            this.lbEmployeeErr.Size = new System.Drawing.Size(0, 13);
            this.lbEmployeeErr.TabIndex = 11;
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddEmployee.Location = new System.Drawing.Point(476, 131);
            this.btnAddEmployee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(84, 24);
            this.btnAddEmployee.TabIndex = 10;
            this.btnAddEmployee.Text = "Add";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // cbPosition
            // 
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(477, 92);
            this.cbPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(192, 24);
            this.cbPosition.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(393, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Position:";
            // 
            // cbEmGroup
            // 
            this.cbEmGroup.FormattingEnabled = true;
            this.cbEmGroup.Location = new System.Drawing.Point(477, 51);
            this.cbEmGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbEmGroup.Name = "cbEmGroup";
            this.cbEmGroup.Size = new System.Drawing.Size(192, 24);
            this.cbEmGroup.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(394, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Group:";
            // 
            // empStartingDate
            // 
            this.empStartingDate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.empStartingDate.Location = new System.Drawing.Point(117, 131);
            this.empStartingDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.empStartingDate.Name = "empStartingDate";
            this.empStartingDate.Size = new System.Drawing.Size(214, 24);
            this.empStartingDate.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(49)))));
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1452, 967);
            this.panel4.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnSelectFile);
            this.panel1.Controls.Add(this.lbCurrentSheet);
            this.panel1.Controls.Add(this.currentSheetLb);
            this.panel1.Location = new System.Drawing.Point(13, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 261);
            this.panel1.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtStaffSearch);
            this.panel5.Controls.Add(this.lbStaffSearch);
            this.panel5.Controls.Add(this.btnUpdateStaff);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.lbEmployeeErr);
            this.panel5.Controls.Add(this.btnAddEmployee);
            this.panel5.Controls.Add(this.cbPosition);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.cbEmGroup);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.empStartingDate);
            this.panel5.Controls.Add(this.empName);
            this.panel5.Controls.Add(this.empCode);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(3, 74);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(738, 183);
            this.panel5.TabIndex = 8;
            // 
            // txtStaffSearch
            // 
            this.txtStaffSearch.Location = new System.Drawing.Point(116, 12);
            this.txtStaffSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStaffSearch.Multiline = true;
            this.txtStaffSearch.Name = "txtStaffSearch";
            this.txtStaffSearch.Size = new System.Drawing.Size(553, 24);
            this.txtStaffSearch.TabIndex = 16;
            this.txtStaffSearch.TextChanged += new System.EventHandler(this.txtStaffSearch_TextChanged);
            // 
            // lbStaffSearch
            // 
            this.lbStaffSearch.AutoSize = true;
            this.lbStaffSearch.ForeColor = System.Drawing.Color.White;
            this.lbStaffSearch.Location = new System.Drawing.Point(8, 16);
            this.lbStaffSearch.Name = "lbStaffSearch";
            this.lbStaffSearch.Size = new System.Drawing.Size(100, 16);
            this.lbStaffSearch.TabIndex = 15;
            this.lbStaffSearch.Text = "Search by Code:";
            // 
            // empName
            // 
            this.empName.Location = new System.Drawing.Point(117, 91);
            this.empName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.empName.Multiline = true;
            this.empName.Name = "empName";
            this.empName.Size = new System.Drawing.Size(214, 24);
            this.empName.TabIndex = 4;
            // 
            // empCode
            // 
            this.empCode.Location = new System.Drawing.Point(117, 51);
            this.empCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.empCode.Multiline = true;
            this.empCode.Name = "empCode";
            this.empCode.Size = new System.Drawing.Size(214, 24);
            this.empCode.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 16);
            this.label7.TabIndex = 2;
            this.label7.Text = "Starting Date:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Code:";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.AutoSize = true;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Image = global::OSD_Tool.Properties.Resources.export;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(120, 11);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 50);
            this.btnExport.TabIndex = 6;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "     Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectFile.Image = global::OSD_Tool.Properties.Resources.import;
            this.btnSelectFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectFile.Location = new System.Drawing.Point(3, 11);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(103, 50);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "         Choose File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lbCurrentSheet
            // 
            this.lbCurrentSheet.AutoSize = true;
            this.lbCurrentSheet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbCurrentSheet.ForeColor = System.Drawing.Color.White;
            this.lbCurrentSheet.Location = new System.Drawing.Point(343, 31);
            this.lbCurrentSheet.Name = "lbCurrentSheet";
            this.lbCurrentSheet.Size = new System.Drawing.Size(0, 13);
            this.lbCurrentSheet.TabIndex = 7;
            // 
            // currentSheetLb
            // 
            this.currentSheetLb.AutoSize = true;
            this.currentSheetLb.ForeColor = System.Drawing.Color.White;
            this.currentSheetLb.Location = new System.Drawing.Point(247, 29);
            this.currentSheetLb.Name = "currentSheetLb";
            this.currentSheetLb.Size = new System.Drawing.Size(85, 16);
            this.currentSheetLb.TabIndex = 3;
            this.currentSheetLb.Text = "Current Sheet:";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.gridAnnualLeave);
            this.panel3.Location = new System.Drawing.Point(0, 272);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1035, 694);
            this.panel3.TabIndex = 10;
            // 
            // gridAnnualLeave
            // 
            this.gridAnnualLeave.AllowUserToAddRows = false;
            this.gridAnnualLeave.AllowUserToDeleteRows = false;
            this.gridAnnualLeave.AllowUserToOrderColumns = true;
            this.gridAnnualLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAnnualLeave.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAnnualLeave.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridAnnualLeave.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAnnualLeave.Location = new System.Drawing.Point(13, 0);
            this.gridAnnualLeave.Margin = new System.Windows.Forms.Padding(58, 25, 3, 4);
            this.gridAnnualLeave.Name = "gridAnnualLeave";
            this.gridAnnualLeave.ReadOnly = true;
            this.gridAnnualLeave.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridAnnualLeave.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridAnnualLeave.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAnnualLeave.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAnnualLeave.Size = new System.Drawing.Size(1021, 690);
            this.gridAnnualLeave.TabIndex = 5;
            this.gridAnnualLeave.VirtualMode = true;
            this.gridAnnualLeave.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAnnualLeave_CellClick);
            this.gridAnnualLeave.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridAnnualLeave_DataBindingComplete);
            // 
            // AnnualLeaveCtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AnnualLeaveCtr";
            this.Size = new System.Drawing.Size(1452, 967);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTimeLeave)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAnnualLeave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridTimeLeave;
        private System.Windows.Forms.Label lblTimeOffError;
        private System.Windows.Forms.Button btnApplyTimeOff;
        private System.Windows.Forms.Label lbStaffName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeOff;
        private System.Windows.Forms.DateTimePicker dateTimePickerSelectDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateStaff;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbEmployeeErr;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbEmGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker empStartingDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox empName;
        private System.Windows.Forms.TextBox empCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label lbCurrentSheet;
        private System.Windows.Forms.Label currentSheetLb;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gridAnnualLeave;
        private System.Windows.Forms.Label lbStaffSearch;
        private System.Windows.Forms.TextBox txtStaffSearch;
    }
}
