using OfficeOpenXml;
using OSD_Tool.Models;
using OSD_Tool.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OSD_Tool.Screens
{
    public partial class AnnualLeaveCtr : UserControl
    {
        #region Properties
        BindingList<Staff_AnnualLeaveModel> employees = new BindingList<Staff_AnnualLeaveModel>();
        FileInfo file;
        private List<DateRange> _dateRanges;
        private int _totalColumns;
        private int currentIndex = -1;
        #endregion

        public AnnualLeaveCtr()
        {
            _dateRanges = AnnualLeaveUtils.GetDateRanges();
            _totalColumns = _dateRanges.Count + Constants.AnnualLeaveHeader10Columns.Count + Constants.AnnualLeaveHeaderLast4Columns.Count;
            InitializeComponent();
            turnOffSetTimeOff();
            btnExport.Enabled = false;
            empCode.Enabled = false;
            empName.Enabled = false;
            cbEmGroup.Enabled = false;
            cbPosition.Enabled = false;
            btnAddEmployee.Enabled = false;
            empStartingDate.Enabled = false;
            btnUpdateStaff.Enabled = false;
        }

        #region Private Funcs
        private void GetContent(ExcelWorksheet ws, int totalCol)
        {
            var startRow = ws.Dimension.Start.Row + 4;
            int rowCount = ws.Dimension.Rows - 3;
            string group = string.Empty;
            bool hasFoundColumn = false;
            int balcanceUtil20Col = -1;
            for (var s = ws.Dimension.Start.Row; s <= ws.Dimension.End.Row; s++)
            {
                if (hasFoundColumn) break;
                for (var t = ws.Dimension.Start.Column; t <= ws.Dimension.End.Column; t++)
                {
                    var balance = ws.Cells[s, t].Value?.ToString();
                    if (!string.IsNullOrEmpty(balance) && balance.Contains("Balance until"))
                    {
                        hasFoundColumn = true;
                        balcanceUtil20Col = t;
                        break;
                    }
                }
            }

            for (var i = startRow; i < rowCount; i++)
            {
                string no = ws.Cells[i, 1].Value?.ToString();
                string code = ws.Cells[i, 2].Value?.ToString();
                string name = ws.Cells[i, 4].Value?.ToString();
                string position = ws.Cells[i, 5].Value?.ToString();
                string OTOf20 = ws.Cells[i, 8].Value?.ToString();
                string balcanceUtil20 = ws.Cells[i, balcanceUtil20Col].Value?.ToString();

                string startingDay = ws.Cells[i, 6].Value?.ToString();
                DateTime startDay = new DateTime();
                if (startingDay != null)
                {
                    try
                    {
                        long date = long.Parse(startingDay);
                        startDay = DateTime.FromOADate(date);
                    }
                    catch { }
                }

                if (no == null && code == null && name != "Da Nang Branch")
                    group = name;

                if (no != null)
                {
                    employees.Add(new Staff_AnnualLeaveModel
                    {
                        Code = code,
                        Name = name,
                        Position = position,
                        Group = group,
                        StartDay = startDay,
                        OTOf20 = OTOf20 != null ? double.Parse(OTOf20) : 0,
                        BalanceUtil20 = balcanceUtil20 != null ? double.Parse(balcanceUtil20) : 0
                    });
                }
            }
            gridAnnualLeave.DataSource = employees;
            gridAnnualLeave.Columns["Group"].Frozen = true;
            gridAnnualLeave.Columns["OTOf20"].Visible = false;
            gridAnnualLeave.Columns["BalanceUtil20"].Visible = false;
            AddDeleteAnnualLeaveButton();
        }

        private void InitComboboxsItem()
        {
            var groups = employees.Select(x => x.Group).Distinct().ToArray();
            var positions = employees.Select(z => z.Position).Distinct().ToArray();

            cbEmGroup.DataSource = groups;
            cbPosition.DataSource = positions;
            cbEmGroup.Text = cbPosition.Text = string.Empty;
        }

        private void btnSelectFile_Click(object sender, System.EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog()
                {
                    Filter = "Excel Workbook|*.xls;*.xlsx",
                    ValidateNames = true
                })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        file = new FileInfo(ofd.FileName);
                        ResetAllData();
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            var totalWorksheets = package.Workbook.Worksheets.Count;
                            var latestWorksheet = package.Workbook.Worksheets[totalWorksheets - 2];
                            int colCount = latestWorksheet.Dimension.Columns;
                            lbCurrentSheet.Text = latestWorksheet.Name;

                            GetContent(latestWorksheet, colCount);
                            InitComboboxsItem();
                            btnExport.Enabled = true;
                            empCode.Enabled = true;
                            empName.Enabled = true;
                            cbEmGroup.Enabled = true;
                            cbPosition.Enabled = true;
                            btnAddEmployee.Enabled = true;
                            empStartingDate.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    string sheetName = DateTime.Now.ToString("MMM, yyyy");
                    ExcelWorksheet newSheet = package.Workbook.Worksheets.Add(sheetName);
                    package.Workbook.Worksheets.MoveBefore(package.Workbook.Worksheets.Count, package.Workbook.Worksheets.Count - 2);
                    package.Workbook.View.ActiveTab = package.Workbook.Worksheets.Count - 3;
                    ExcelRange Rng = newSheet.Cells[1, 4];
                    Rng.Value = "RECORD OF ANNUAL LEAVE  OSD";
                    Rng.Style.Font.Size = 18;
                    Rng.Style.Font.Bold = true;

                    RenderRow2(newSheet);
                    RenderRow3(newSheet);
                    RenderRow4(newSheet);
                    RenderRow5(newSheet);
                    RenderData(newSheet);
                    RenderFinalRow(newSheet);

                    newSheet.Protection.IsProtected = false;
                    newSheet.Protection.AllowSelectLockedCells = false;
                    newSheet.View.FreezePanes(5, 11);

                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    saveDialog.FilterIndex = 2;

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        package.SaveAs(new FileInfo(saveDialog.FileName));
                        MessageBox.Show("Export Successful.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDeleteTimeLeaveButton()
        {
            DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
            DeleteButton.Name = "Action";
            DeleteButton.Text = "Delete";
            DeleteButton.UseColumnTextForButtonValue = true;
            if (gridTimeLeave.Columns["Action"] == null)
            {
                gridTimeLeave.Columns.Insert(gridTimeLeave.ColumnCount, DeleteButton);
            }
        }

        private void AddDeleteAnnualLeaveButton()
        {
            DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
            DeleteButton.Name = "Action";
            DeleteButton.Text = "Delete";
            DeleteButton.UseColumnTextForButtonValue = true;
            if (gridAnnualLeave.Columns["Action"] == null)
            {
                gridAnnualLeave.Columns.Insert(gridAnnualLeave.ColumnCount, DeleteButton);
            }
        }

        private void gridTimeLeave_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridTimeLeave.Columns["Action"].Index && e.RowIndex >= 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this record?"
                            , "Confirm Delete!!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    employees[currentIndex].DateAnnualLeaves.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var name = empName.Text;
            var code = empCode.Text;
            var startingDate = empStartingDate.Value;
            var group = cbEmGroup.Text;
            var position = cbPosition.Text;
            var existedEmployeeCode = employees.FirstOrDefault(x => x.Code.ToLower() == code.ToLower()) != null;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                lbEmployeeErr.Text = "Please enter all required fields.";
            }
            else if (existedEmployeeCode)
            {
                lbEmployeeErr.Text = $"Employee with {code} already exist.";
            }
            else
            {
                lbEmployeeErr.Text = string.Empty;
                Staff_AnnualLeaveModel employee = new Staff_AnnualLeaveModel
                {
                    Code = code,
                    Name = name,
                    Group = group,
                    Position = position,
                    StartDay = startingDate
                };
                employees.Add(employee);

                InitComboboxsItem();
                gridAnnualLeave.DataSource = employees;
                ResetForm();
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            var name = empName.Text;
            var code = empCode.Text;
            var startingDate = empStartingDate.Value;
            var group = cbEmGroup.Text;
            var position = cbPosition.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                lbEmployeeErr.Text = "Please enter all required fields.";
            }
            else
            {
                lbEmployeeErr.Text = string.Empty;
                var employee = employees[currentIndex];
                employee.Code = code;
                employee.Name = name;
                employee.StartDay = startingDate;
                employee.Group = group;
                employee.Position = position;
                employees[currentIndex] = employee;
                btnUpdateStaff.Enabled = false;
                InitComboboxsItem();
                gridAnnualLeave.DataSource = employees;
                ResetForm();
            }
        }
        #endregion

        #region Event Methods

        private void ResetAllData()
        {
            gridAnnualLeave.DataSource = new BindingList<Staff_AnnualLeaveModel>();
            gridTimeLeave.DataSource = new BindingList<DateAnnualLeave>();
            employees.Clear();
            lblTimeOffError.Text = string.Empty;
            ResetForm();
        }

        private void gridAnnualLeave_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ClearTimeOffData();
                var staffCode = gridAnnualLeave.Rows[e.RowIndex].Cells["Code"]?.Value.ToString();
                var staff = employees.Where(x => x.Code == staffCode).FirstOrDefault();
                int staffIndex = employees.IndexOf(staff);

                if (staffIndex > -1)
                {
                    currentIndex = staffIndex;
                }

                if (e.ColumnIndex == gridAnnualLeave.Columns["Action"].Index && e.RowIndex >= 0)
                {
                    var confirmResult = MessageBox.Show("Are you sure to delete this record?"
                                , "Confirm Delete!!", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        gridAnnualLeave.Rows.RemoveAt(e.RowIndex);
                        employees.RemoveAt(staffIndex);
                        currentIndex = -1;
                        lbStaffName.Text = string.Empty;
                        ResetForm();
                        gridTimeLeave.DataSource = new BindingList<DateAnnualLeave>();
                    }
                }
                else if (currentIndex >= 0)
                {
                    turnOnSetTimeOff();
                    lbStaffName.Text = staff.Name;
                    gridTimeLeave.DataSource = staff.DateAnnualLeaves;
                    AddDeleteTimeLeaveButton();
                    btnUpdateStaff.Enabled = true;
                    empName.Text = staff.Name;
                    empCode.Text = staff.Code;
                    cbEmGroup.Text = staff.Group;
                    cbPosition.Text = staff.Position;
                    empStartingDate.Value = staff.StartDay;
                }
            }
        }

        private void turnOffSetTimeOff()
        {
            btnApplyTimeOff.Enabled = false;
            dateTimePickerSelectDay.Enabled = false;
            txtTimeOff.Enabled = false;
            txtTimeOff.Text = null;
            lbStaffName.Text = null;
        }

        private void turnOnSetTimeOff()
        {
            btnApplyTimeOff.Enabled = true;
            dateTimePickerSelectDay.Enabled = true;
            txtTimeOff.Enabled = true;
            var today = DateTime.Today;
            dateTimePickerSelectDay.MinDate = new DateTime(today.Year, today.Month, 21).AddMonths(-1);
            dateTimePickerSelectDay.MaxDate = new DateTime(today.Year, today.Month, 20);
            dateTimePickerSelectDay.Value = new DateTime(today.Year, today.Month, 21).AddMonths(-1);
            lblTimeOffError.Text = null;
        }

        private void ClearTimeOffData()
        {
            var today = DateTime.Today;
            dateTimePickerSelectDay.Value = new DateTime(today.Year, today.Month, 21).AddMonths(-1);
            txtTimeOff.Text = null;
        }

        private void ResetForm()
        {
            empCode.Text = string.Empty;
            empName.Text = string.Empty;
            cbEmGroup.Text = string.Empty;
            cbPosition.Text = string.Empty;
            empStartingDate.Value = DateTime.Now;
        }

        private void gridAnnualLeave_DataBindingComplete(object sender, EventArgs e)
        {
            gridAnnualLeave.ClearSelection();
        }

        private void gridTimeLeave_DataBindingComplete(object sender, EventArgs e)
        {
            gridTimeLeave.ClearSelection();
        }

        private void btnApplyTimeOff_Click(object sender, EventArgs e)
        {
            try
            {
                var timeOff = Convert.ToDouble(txtTimeOff.Text);
                var employee = employees[currentIndex];
                var existedTimeLeave = employee.DateAnnualLeaves.Where(x => x.Date.Date == dateTimePickerSelectDay.Value.Date).FirstOrDefault();

                if (timeOff > 0 && timeOff <= 1)
                {
                    if (existedTimeLeave != null)
                    {
                        var confirmResult = MessageBox.Show("The time leave for this employee in this date already exists. Are you sure to update current as latest?"
                            , "Confirm Update Info!!", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            var timeLeaveExistedIndex = employee.DateAnnualLeaves.IndexOf(existedTimeLeave);
                            if (timeLeaveExistedIndex > -1)
                                employees[currentIndex].DateAnnualLeaves[timeLeaveExistedIndex].Leave = timeOff;
                        }
                    }
                    else
                    {
                        employees[currentIndex].DateAnnualLeaves.Add(new DateAnnualLeave
                        {
                            Date = dateTimePickerSelectDay.Value,
                            Leave = timeOff
                        });
                    }
                    txtTimeOff.Text = string.Empty;
                    gridTimeLeave.DataSource = employees[currentIndex].DateAnnualLeaves;
                    gridTimeLeave.Refresh();
                }
                else
                {
                    lblTimeOffError.Text = "Time off must be (0 < Time off <=1)";
                }
            }
            catch
            {
                lblTimeOffError.Text = "Time off must be (0 < Time off <=1)";
            }
        }

        private void txtTimeOff_TextChanged(object sender, EventArgs e)
        {
            lblTimeOffError.Text = null;
        }
        #endregion

        #region Render Rows In Export Excel
        private void RenderRow2(ExcelWorksheet sheet)
        {
            sheet.Cells[2, 4].Value = "Ngày chốt:";
            sheet.Cells[2, 4].Style.Font.Size = 9;
            sheet.Cells[2, 4].Style.Font.Color.SetColor(Color.OrangeRed);
            sheet.Cells[2, 5].Value = string.Format("20-{0}", DateTime.Now.ToString("MMM-yyyy"));
            sheet.Cells[2, 5].Style.Font.Size = 10;
            sheet.Cells[2, 5].Style.Font.Bold = true;
            sheet.Cells[2, 5].Style.Font.Color.SetColor(Color.OrangeRed);
        }

        private void RenderRow3(ExcelWorksheet sheet)
        {
            sheet.Cells[3, 2].Value = 1;
            var monthDurationCells = sheet.Cells[3, 11, 3, _dateRanges.Count + 11];
            monthDurationCells.Style.Font.Bold = true;
            monthDurationCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            monthDurationCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            monthDurationCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            monthDurationCells.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            monthDurationCells.Style.Border.Right.Style =
            monthDurationCells.Style.Border.Bottom.Style =
            monthDurationCells.Style.Border.Left.Style =
                OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            int colOfLastDateOfPrevMonth = _dateRanges.FindIndex(x => x.IsLastDateOfPrevMonth) + 1;
            var prevMonthCells = sheet.Cells[3, 11, 3, colOfLastDateOfPrevMonth + 11];
            prevMonthCells.Value = DateTime.Today.AddMonths(-1).ToString("MMM-yyyy");
            prevMonthCells.Merge = true;

            var currentMonthCells = sheet.Cells[3, 11 + (colOfLastDateOfPrevMonth + 1), 3, _dateRanges.Count + 11];
            currentMonthCells.Merge = true;
            currentMonthCells.Value = DateTime.Today.ToString("MMMM-yyyy");
        }

        private void RenderRow4(ExcelWorksheet sheet)
        {
            int startHeaderRow = 4;
            Color colFromHex = ColorTranslator.FromHtml("#f8cbad");
            Color redText = Color.OrangeRed;

            var allCells = sheet.Cells[startHeaderRow, 1, startHeaderRow, _totalColumns];
            allCells.Style.Font.Bold = true;
            allCells.Style.Font.SetFromFont(new Font("Arial", 8));
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            allCells.Style.WrapText = true;
            allCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            allCells.Style.Fill.BackgroundColor.SetColor(colFromHex);
            allCells.Style.Border.Top.Style =
            allCells.Style.Border.Right.Style =
            allCells.Style.Border.Bottom.Style =
            allCells.Style.Border.Left.Style =
                OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Row(startHeaderRow).Height = 70;

            for (var i = 0; i < Constants.AnnualLeaveHeader10Columns.Count; i++)
            {
                int column = i + 1;
                sheet.Cells[4, column].Value = Constants.AnnualLeaveHeader10Columns[i];
                sheet.Cells[4, column].Style.Font.Bold = true;

                if (i == 1)
                    sheet.Column(column).Width = 15;

                if (i == 2)
                    sheet.Column(column).Width = 25;

                if (i == 3)
                    sheet.Column(column).Width = 30;
                if (i == 9)
                    sheet.Cells[4, column].Style.Font.Color.SetColor(redText);
            }


            var prevCol = 11;
            for (var i = 0; i < _dateRanges.Count; i++)
            {
                int column = i + prevCol;
                var dateRange = _dateRanges[i];
                sheet.Cells[4, column].Value = dateRange.Text;
                sheet.Column(column).Width = 5;
            }

            for (var i = 0; i < Constants.AnnualLeaveHeaderLast4Columns.Count; i++)
            {
                int column = (i + _dateRanges.Count + prevCol);
                sheet.Cells[4, column].Value = Constants.AnnualLeaveHeaderLast4Columns[i];
                sheet.Cells[4, column].Style.Font.Bold = true;
                if (i == 3)
                    sheet.Cells[4, column].Style.Font.Color.SetColor(redText);
            }
        }

        private void RenderRow5(ExcelWorksheet sheet)
        {
            var row5 = sheet.Cells[5, 1, 5, _totalColumns];
            row5.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            row5.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#00b052"));
            row5.Style.Font.SetFromFont(new Font("Arial", 10));
            row5.Style.Border.Top.Style =
            row5.Style.Border.Right.Style =
            row5.Style.Border.Bottom.Style =
            row5.Style.Border.Left.Style =
                OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            sheet.Cells[5, 4].Value = "Da Nang Branch";
            sheet.Cells[5, 4].Style.Font.Bold = true;
        }

        private void RenderData(ExcelWorksheet sheet)
        {
            var startRow = 6;
            var groups = employees.Select(x => x.Group).Distinct().ToArray();
            foreach (var group in groups)
            {
                var allCellOfRow = sheet.Cells[startRow, 1, startRow, _totalColumns];
                allCellOfRow.Style.Font.SetFromFont(new Font("Arial", 8));
                allCellOfRow.Style.Font.Bold = true;
                allCellOfRow.Style.Font.Size = 10;
                allCellOfRow.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                allCellOfRow.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#91a8de"));
                allCellOfRow.Style.Border.Right.Style =
                allCellOfRow.Style.Border.Bottom.Style =
                allCellOfRow.Style.Border.Left.Style =
                    OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                sheet.Cells[startRow, 4].Value = group;

                startRow++;
                var staffByGroup = employees.Where(x => x.Group.Equals(group)).ToList();
                for (var i = 0; i < staffByGroup.Count; i++)
                {
                    var staff = staffByGroup[i];
                    var allCellOfRowStaff = sheet.Cells[startRow, 1, startRow, _totalColumns];
                    allCellOfRowStaff.Style.Font.SetFromFont(new Font("Arial", 8));
                    allCellOfRowStaff.Style.Border.Right.Style =
                    allCellOfRowStaff.Style.Border.Bottom.Style =
                    allCellOfRowStaff.Style.Border.Left.Style =
                        OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    sheet.Cells[startRow, 1].Value = i + 1;

                    sheet.Cells[startRow, 2].Value = staff.Code;
                    sheet.Cells[startRow, 2].Style.Font.Size = 7;
                    sheet.Cells[startRow, 3].Value = staff.Code + "@orientsoftware.com";
                    sheet.Cells[startRow, 3].Style.Font.Size = 7;

                    var employeeNameCell = sheet.Cells[startRow, 4];
                    employeeNameCell.Value = staff.Name;
                    employeeNameCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    // Setcolor yellow if Trainee
                    if (!AnnualLeaveUtils.CheckDayTraineeExpire(staff.StartDay, staff.Group) && staff.Group != Constants.INTERN)
                    {
                        employeeNameCell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }
                    else
                    {
                        employeeNameCell.Style.Fill.BackgroundColor.SetColor(Color.White);
                    }

                    sheet.Cells[startRow, 5].Value = staff.Position;

                    sheet.Cells[startRow, 6].Value = staff.StartDay;
                    sheet.Cells[startRow, 6].Style.Numberformat.Format = "dd-MMM-yy";

                    sheet.Cells[startRow, 7].Value = staff.BalanceUtil20;
                    sheet.Cells[startRow, 7].Style.Font.Bold = true;
                    sheet.Cells[startRow, 7].Style.Font.Size = 9;

                    sheet.Cells[startRow, 8].Value = staff.OTOf20;

                    // Annual Leave of Month
                    sheet.Cells[startRow, 9].Value = group.ToUpper().Equals(Constants.INTERN) ? 0 : 1;

                    // Calc total annual leave
                    sheet.Cells[startRow, 10].Formula = "=SUM(" + sheet.Cells[startRow, 7].Address + ":" + sheet.Cells[startRow, 9].Address + ")";
                    sheet.Cells[startRow, 10].Style.Font.Bold = true;
                    sheet.Cells[startRow, 10].Style.Font.Color.SetColor(Color.OrangeRed);

                    // Mapping data input date leave
                    int startCol = 10;
                    for (var j = 0; j < _dateRanges.Count; j++)
                    {
                        startCol += 1;
                        var dateRange = _dateRanges[j];
                        var dayLeave = staff.DateAnnualLeaves.Where(st => st.Date.Date == dateRange.Date.Date).FirstOrDefault();
                        if (dayLeave != null)
                            sheet.Cells[startRow, startCol].Value = dayLeave.Leave;

                        // Set yellow color if day is weekend
                        if (dateRange.IsWeekend)
                        {
                            sheet.Cells[startRow, startCol].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            sheet.Cells[startRow, startCol].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        }
                    }

                    // Calc total days off
                    sheet.Cells[startRow, _totalColumns - 3].Formula = "=SUM(" + sheet.Cells[startRow, 11].Address + ":" + sheet.Cells[startRow, _totalColumns - 4].Address + ")";

                    // Calc normal balance
                    sheet.Cells[startRow, _totalColumns - 2].Formula = "=" + sheet.Cells[startRow, 10].Address + "-" + sheet.Cells[startRow, _totalColumns - 3].Address;

                    // Calc deduct
                    sheet.Cells[startRow, _totalColumns - 1].Formula = "=IF(" + sheet.Cells[startRow, _totalColumns - 2].Address + "<0," + sheet.Cells[startRow, _totalColumns - 2].Address + ",0)";

                    // Calc balance until of month
                    sheet.Cells[startRow, _totalColumns].Formula = "=" + sheet.Cells[startRow, _totalColumns - 2].Address + "-" + sheet.Cells[startRow, _totalColumns - 1].Address;
                    sheet.Cells[startRow, _totalColumns].Style.Font.Bold = true;
                    sheet.Cells[startRow, _totalColumns].Style.Font.Size = 9;
                    sheet.Cells[startRow, _totalColumns].Style.Font.Color.SetColor(Color.OrangeRed);

                    startRow++;
                }
            }
        }

        private void RenderFinalRow(ExcelWorksheet sheet)
        {
            var lastRowIndex = sheet.Dimension.Rows + 1;
            var lastRow = sheet.Cells[lastRowIndex, 1, lastRowIndex, _totalColumns];
            lastRow.Style.Font.Bold = true;
            lastRow.Style.Border.Right.Style =
            lastRow.Style.Border.Bottom.Style =
            lastRow.Style.Border.Left.Style =
                OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            var sumCellLb = sheet.Cells[lastRowIndex, 1, lastRowIndex, 6];
            sumCellLb.Value = "SUM";
            sumCellLb.Merge = true;
            sumCellLb.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sumCellLb.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Cells[lastRowIndex, 7].Formula = "=SUM(" + sheet.Cells[5, 7].Address + ":" + sheet.Cells[lastRowIndex - 1, 7].Address + ")";
            sheet.Cells[lastRowIndex, 8].Formula = "=SUM(" + sheet.Cells[5, 8].Address + ":" + sheet.Cells[lastRowIndex - 1, 8].Address + ")";
            sheet.Cells[lastRowIndex, 9].Formula = "=SUM(" + sheet.Cells[5, 9].Address + ":" + sheet.Cells[lastRowIndex - 1, 9].Address + ")";
            sheet.Cells[lastRowIndex, 10].Formula = "=SUM(" + sheet.Cells[5, 10].Address + ":" + sheet.Cells[lastRowIndex - 1, 10].Address + ")";
            sheet.Cells[lastRowIndex, 42].Formula = "=SUM(" + sheet.Cells[5, 42].Address + ":" + sheet.Cells[lastRowIndex - 1, 42].Address + ")";
            sheet.Cells[lastRowIndex, 43].Formula = "=SUM(" + sheet.Cells[5, 43].Address + ":" + sheet.Cells[lastRowIndex - 1, 43].Address + ")";
            sheet.Cells[lastRowIndex, 44].Formula = "=SUM(" + sheet.Cells[5, 44].Address + ":" + sheet.Cells[lastRowIndex - 1, 44].Address + ")";
            sheet.Cells[lastRowIndex, 45].Formula = "=SUM(" + sheet.Cells[5, 45].Address + ":" + sheet.Cells[lastRowIndex - 1, 45].Address + ")";
        }
        #endregion

        private void txtStaffSearch_TextChanged(object sender, EventArgs e)
        {
            var searchTerm = txtStaffSearch.Text;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = employees.Where(x => x.Code.Contains(txtStaffSearch.Text)).ToList();
                var sources = new BindingList<Staff_AnnualLeaveModel>(results);
                gridAnnualLeave.DataSource = sources;
            }
            else
            {
                gridAnnualLeave.DataSource = employees;
            }
        }
    }
}
