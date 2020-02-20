using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using OSD_Tool.Models;
using System.IO;
using OSD_Tool.Utils;
using System.Threading.Tasks;
using OSD_Tool.Services;
using static OSD_Tool.Models.FullWHModel;

namespace OSD_Tool.Screens.Controls
{
    public partial class WorkingHourControl : UserControl
    {
        #region Properties
        private FileInfo file;
        private List<WeekSheet> _weekSheets;
        private List<GroupStaff> _groupStaffs;
        private List<DailyReport> _dailyReports;
        private ExcelPackage package;
        private WorkingHourService workingHourService;
        #endregion

        public WorkingHourControl()
        {
            InitializeComponent();
            panelProgress.Visible = false;
            panelResult.Visible = false;
            lbStatus.Text = string.Empty;

            _weekSheets = new List<WeekSheet>();
            _groupStaffs = new List<GroupStaff>();
            _dailyReports = new List<DailyReport>();
            workingHourService = new WorkingHourService();
        }

        #region Funcs
        private async void btnSelectFile_Click(object sender, EventArgs e)
        {
            ResetAll();
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
                        if (file != null)
                        {
                            using (ExcelPackage package = new ExcelPackage(file))
                            {
                                await TaskUtils.StartSTATask(() => ProcessFile(package));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task ProcessFile(ExcelPackage package)
        {
            var workSheets = package.Workbook.Worksheets;
            var workingSheets = workSheets.Take(workSheets.Count - 3).Reverse().ToList();
            panelProgress.BeginInvoke(new MethodInvoker(() => panelProgress.Visible = true));
            panelResult.BeginInvoke(new MethodInvoker(() => panelResult.Visible = false));
            lbStatus.BeginInvoke(new MethodInvoker(() => lbStatus.Text = "File is being reading..."));

            await PrepareGroupAndStaffData(workSheets[workSheets.Count - 1], workingSheets);
            lbStatus.BeginInvoke(new MethodInvoker(() => lbStatus.Text = "Analyzing and preparing for output file"));
            await CreateWeekSheetInCurrentFile();

            panelProgress.BeginInvoke(new MethodInvoker(() => panelProgress.Visible = false));
            panelResult.BeginInvoke(new MethodInvoker(() => panelResult.Visible = true));
        }


        #region PrepareGroupAndStaffData
        private async Task PrepareGroupAndStaffData(ExcelWorksheet sheet, List<ExcelWorksheet> workingSheets)
        {
            var rows = sheet.Dimension.Rows;
            var startRow = sheet.Dimension.Start.Row + 1;
            var lastPartOneIndex = WorkingHoursUtils.Split2Loop(rows);

            // if start from 1 => next part will + 1
            await Task.WhenAll(
                ReadEmployeeList(sheet, startRow, lastPartOneIndex),
                ReadEmployeeList(sheet, (lastPartOneIndex + 1), rows),
                workingHourService.ReadDailyReportDataSheetAndWeekDays(workingSheets, _dailyReports, _weekSheets)
            );

            var a = _groupStaffs;
        }

        private Task ReadEmployeeList(ExcelWorksheet sheet, int from, int to)
        {
            try
            {
                for (var i = from; i < to; i++)
                {
                    var code = sheet.Cells[i, 1].Value?.ToString();
                    if (!string.IsNullOrEmpty(code))
                    {
                        var name = sheet.Cells[i, 2].Value?.ToString();
                        var position = sheet.Cells[i, 3].Value?.ToString();
                        var group = sheet.Cells[i, 4].Value?.ToString();
                        var existedGroup = _groupStaffs.Where(grp => grp.Name.Equals(group)).FirstOrDefault();

                        var staff = new Staff
                        {
                            Code = code,
                            Name = name,
                            Group = group,
                            Position = position
                        };

                        if (existedGroup != null)
                        {
                            var indexGroup = _groupStaffs.IndexOf(existedGroup);
                            _groupStaffs[indexGroup].Staffs.Add(staff);
                        }
                        else
                        {
                            _groupStaffs.Add(new GroupStaff
                            {
                                Name = group,
                                Staffs = new List<Staff> { staff }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }

        #endregion

        private async Task CreateWeekSheetInCurrentFile()
        {
            try
            {
                package = new ExcelPackage(file);
                var workSheets = package.Workbook.Worksheets;
                foreach (var weekSheet in _weekSheets)
                {
                    ExcelWorksheet sheet = workSheets.Add(weekSheet.Name);
                    sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    sheet.Cells.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                    workSheets.MoveBefore(workSheets.Count, workSheets.Count - 3);

                    workingHourService.RenderFirstPart(sheet);
                    await RenderDayData(sheet, weekSheet);
                    sheet.Cells[4, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    sheet.Calculate();
                    sheet.View.FreezePanes(5, 5);
                    sheet.TabColor = Color.Yellow;
                    sheet.Protection.IsProtected = false;
                    sheet.Protection.AllowSelectLockedCells = false;
                }
                CreateDeductedSheet(package);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task RenderDayData(ExcelWorksheet sheet, WeekSheet weekSheet)
        {
            int col = Constants.WH_SKIP_COULUMNS;
            var totalDaysOfWeek = weekSheet.WeekDayDatas.Count;
            for (var i = 1; i <= totalDaysOfWeek; i++)
            {
                var endCol = col + 14;
                var dateRow = sheet.Cells[1, (col + 1), 1, endCol];
                var dayRowName = sheet.Cells[2, (col + 1), 2, endCol];
                var weekDayData = weekSheet.WeekDayDatas[i - 1];

                #region row1, row2
                // dateRow & dayRowName styles
                dateRow.Merge = dayRowName.Merge = true;
                dateRow.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                dayRowName.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                dateRow.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                dayRowName.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));

                dateRow.Value = weekDayData.Date;
                dayRowName.Value = weekDayData.DateText;
                #endregion

                #region row 3, left and right
                var rowCells_3left = sheet.Cells[3, col + 1, 3, col + 6];
                var rowCells_3right = sheet.Cells[3, col + 6 + 1, 3, col + 12];

                #region styles
                rowCells_3left.Style.Fill.PatternType =
                rowCells_3right.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rowCells_3left.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                rowCells_3left.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                rowCells_3right.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                rowCells_3right.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                rowCells_3left.Merge = rowCells_3right.Merge = true;
                rowCells_3left.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                rowCells_3right.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                #endregion

                rowCells_3left.Value = "ON REPORT";
                rowCells_3right.Value = "ADJUST";
                #endregion

                #region row 4
                var lastFisrtCell = sheet.Cells[4, col + 13];
                var lastCell = sheet.Cells[4, col + 14];
                sheet.Column(col + 13).Width = 10;
                sheet.Column(col + 14).Width = 10;
                sheet.Column(col + 14).Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Dotted;
                // Render colums in each merged left and right
                for (var j = 0; j < Constants.WH_DAY_HEADER_COLUMNS.Count; j++)
                {
                    int onReportCol = col + 1 + j;
                    int adjustCol = col + 1 + j + 6;
                    var onReportColumns = sheet.Cells[4, onReportCol];
                    var adjustColumns = sheet.Cells[4, adjustCol];
                    sheet.Column(onReportCol).Width = sheet.Column(adjustCol).Width = 7;
                    adjustColumns.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    adjustColumns.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    adjustColumns.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    onReportColumns.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    onReportColumns.Value = adjustColumns.Value = Constants.WH_DAY_HEADER_COLUMNS[j];
                }

                lastFisrtCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                lastCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                lastFisrtCell.Value = "In late (=1)";
                lastCell.Value = "Out early (=1)";
                #endregion

                await RenderStaffTimeSheetData(sheet, weekDayData.SheetName, totalDaysOfWeek, col);
                col = endCol;
            }
            RenderHeader(sheet, col);
        }

        private void CreateDeductedSheet(ExcelPackage package)
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Deducted");
            sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            sheet.Row(1).Style.Font.Bold = true;
            sheet.Row(2).Style.Font.Bold = true;

            #region Properties
            int totalFileWorkSheets = package.Workbook.Worksheets.Count;
            int weekCount = _weekSheets.Count;
            int skipCol = 3;
            var lackingHourCell = sheet.Cells[1, 3, 1, weekCount + 2 + skipCol];
            var deductLateCell = sheet.Cells[1, (weekCount + 3 + skipCol), 1, (weekCount * 2) + 6];
            var totalCell_LH = sheet.Cells[2, _weekSheets.Count + 3];
            var womenCell_LH = sheet.Cells[2, _weekSheets.Count + 4];
            var totalFinalCell_LH = sheet.Cells[2, _weekSheets.Count + 5];
            var totalCell_DL = sheet.Cells[2, (_weekSheets.Count * 2) + 6];
            int totalWeekSheets = _weekSheets.Count;
            
            #endregion

            #region Styles
            lackingHourCell.Merge = deductLateCell.Merge = true;
            lackingHourCell.Value = "LACKING HOUR";
            deductLateCell.Value = "DEDUCT LATE";
            lackingHourCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            lackingHourCell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#508ed7"));
            deductLateCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            deductLateCell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#93cf53"));
            sheet.Cells[2, 1].Value = "Code";
            sheet.Cells[2, 2].Value = "Name";
            sheet.Cells[2, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            sheet.Cells[2, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalCell_LH.Value = "TOTAL";
            womenCell_LH.Value = "WOMAN";
            totalFinalCell_LH.Value = "TOTAL FINAL";
            totalCell_DL.Value = "TOTAL";
            totalCell_LH.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            womenCell_LH.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalFinalCell_LH.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalCell_DL.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalCell_LH.Style.Fill.PatternType =
            womenCell_LH.Style.Fill.PatternType =
            totalCell_DL.Style.Fill.PatternType =
            totalFinalCell_LH.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            totalCell_LH.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            womenCell_LH.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            totalFinalCell_LH.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            totalCell_DL.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            #endregion

            for (var i = 1; i <= totalWeekSheets; i++)
            {
                sheet.Cells[2, i + 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheet.Cells[2, i + 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ddebf7"));
                sheet.Cells[2, (weekCount + skipCol + i + 2)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheet.Cells[2, (weekCount + skipCol + i + 2)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ccff99"));
                sheet.Cells[2, i + 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                sheet.Cells[2, (weekCount + skipCol + i + 2)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                sheet.Cells[2, i + 2].Value =
                sheet.Cells[2, (weekCount + skipCol + i + 2)].Value = $"Week {i}";
            }

            var staffs = _groupStaffs.SelectMany(grp => grp.Staffs).ToList();
            for(var staffIndex = 0; staffIndex < staffs.Count; staffIndex++)
            {
                int startRow = staffIndex + 3;
                var staff = staffs[staffIndex];
                var wHRanges = sheet.Cells[startRow, 3, startRow, totalWeekSheets + 2];
                var deductLateRanges = sheet.Cells[startRow, (totalWeekSheets + 6), startRow, (totalWeekSheets * 2) + 5].Address;
                var totalWH = sheet.Cells[startRow, totalWeekSheets + 3];
                var woman = sheet.Cells[startRow, totalWeekSheets + 4];
                var totalFinalWH = sheet.Cells[startRow, totalWeekSheets + 5];
                var totalDeductLate = sheet.Cells[startRow, (totalWeekSheets * 2) + 6];

                var staffLackingHours = staff.WeekLackingHours.Select(x => x).Distinct().ToArray();
                var staffDeductLates = staff.WeekDeductLates.Select(x => x).Distinct().ToArray();

                sheet.Cells[startRow, 1].Value = staff.Code;
                sheet.Cells[startRow, 2].Value = staff.Name;
                sheet.Cells[startRow, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                sheet.Cells[startRow, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                for (var i = 1; i <= totalWeekSheets; i++)
                {
                    var staffLackingHour = staffLackingHours[i - 1];
                    var staffDeductLate = staffDeductLates[i - 1];
                    var weekLackingHourCell = sheet.Cells[startRow, i + 2];
                    var weekDeductLateCell = sheet.Cells[startRow, (weekCount + skipCol + i + 2)];

                    weekLackingHourCell.Formula = staffLackingHour;
                    weekDeductLateCell.Formula = staffDeductLate;
                }

                totalWH.Formula = $"SUM({wHRanges.Address})";
                totalFinalWH.Formula = $"IF(AND({sheet.Cells[startRow, totalWeekSheets + 4].Address}=\"x\",OR(SUM({wHRanges.Address})>-1.5,SUM({wHRanges.Address})=1.5)),\"\",SUM({wHRanges.Address}))";
                totalDeductLate.Formula = $"SUM({deductLateRanges})";

                totalWH.Style.Fill.PatternType =
                    woman.Style.Fill.PatternType =
                    totalDeductLate.Style.Fill.PatternType =
                    totalFinalWH.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                totalWH.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                totalFinalWH.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                woman.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                totalDeductLate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                totalWH.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
                woman.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
                totalFinalWH.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
                totalDeductLate.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f8cbad"));
            }

            sheet.Cells.AutoFitColumns();
            sheet.TabColor = Color.Green;
            sheet.Protection.IsProtected = false;
            sheet.Protection.AllowSelectLockedCells = false;
            package.Workbook.Worksheets.MoveBefore(totalFileWorkSheets, totalFileWorkSheets - 3);
            package.Workbook.View.ActiveTab = totalFileWorkSheets - 5;
        }

        private int getTotalCols(int totalWeekDays)
        {
            int first4Cols = 4;
            int totalDayCols = totalWeekDays * 14;
            return first4Cols + totalDayCols;
        }

        private Task RenderStaffTimeSheetData(ExcelWorksheet sheet, string SheetName, int totalWeekDayDatas, int startCol)
        {
            int sheetStartRow = 5;
            int endCol = getTotalCols(totalWeekDayDatas);
            int columns = sheet.Dimension.Columns;
            var staffDailySheetData = _dailyReports.Where(x => x.Name.Equals(SheetName)).FirstOrDefault();
            sheet.Column(3).Width = sheet.Column(4).Width = 30;
            sheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

            foreach (var group in _groupStaffs)
            {
                var groupRanges = sheet.Cells[sheetStartRow, 1, sheetStartRow, (endCol + 20)];
                sheet.Cells[sheetStartRow, 1].Value = (sheetStartRow - 4);
                sheet.Cells[sheetStartRow, 3].Value = group.Name;
                sheet.Row(sheetStartRow).Height = 20;

                groupRanges.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                groupRanges.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.GREEN));
                groupRanges.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                sheetStartRow++; // Skip group row

                foreach (var staff in group.Staffs)
                {
                    sheet.Row(sheetStartRow).Height = 20;
                    #region Properties of Calculate Total
                    int workingDays = 0;
                    int col = Constants.WH_SKIP_COULUMNS;
                    string totalWH_RP_Fomula = string.Empty;
                    string totalWH_ADJ_Fomula = string.Empty;
                    string totalLateDays_Fomula = string.Empty;
                    string totalEarlyDays_Fomula = string.Empty;
                    string totalWorkingDays_Fomula = string.Empty;
                    string comma = ",";
                    double workingDateUnit = totalWeekDayDatas * 0.1;

                    var checkInCell_RP = sheet.Cells[sheetStartRow, startCol + 1];
                    var checkInCellInteger_RP = sheet.Cells[sheetStartRow, startCol + 2];
                    var checkOutCell_RP = sheet.Cells[sheetStartRow, startCol + 3];
                    var checkOutCellInteger_RP = sheet.Cells[sheetStartRow, startCol + 4];
                    var actualWorkingHoursCell_RP = sheet.Cells[sheetStartRow, startCol + 5];
                    var workingHour_RP = sheet.Cells[sheetStartRow, startCol + 6];
                    var checkInCell_ADJ = sheet.Cells[sheetStartRow, startCol + 7];
                    var checkInCellInteger_ADJ = sheet.Cells[sheetStartRow, startCol + 8];
                    var checkOutCell_ADJ = sheet.Cells[sheetStartRow, startCol + 9];
                    var checkOutCellInteger_ADJ = sheet.Cells[sheetStartRow, startCol + 10];
                    var actualWorkingHoursCell_ADJ = sheet.Cells[sheetStartRow, startCol + 11];
                    var workingHour_ADJ = sheet.Cells[sheetStartRow, startCol + 12];
                    var inLate = sheet.Cells[sheetStartRow, startCol + 13];
                    var outEarly = sheet.Cells[sheetStartRow, startCol + 14];

                    var leaveOfAskedDay = sheet.Cells[sheetStartRow, endCol + 1];
                    var offWithoutNotice = sheet.Cells[sheetStartRow, endCol + 2];
                    var workingDate = sheet.Cells[sheetStartRow, endCol + 3];
                    var totalWHCell_RP = sheet.Cells[sheetStartRow, endCol + 4];
                    var total1_RP = sheet.Cells[sheetStartRow, endCol + 5];
                    var total2_RP = sheet.Cells[sheetStartRow, endCol + 6];
                    var totalWHCell_ADJ = sheet.Cells[sheetStartRow, endCol + 7];
                    var total1_ADJ = sheet.Cells[sheetStartRow, endCol + 8];
                    var total2_ADJ = sheet.Cells[sheetStartRow, endCol + 9];
                    var averageWorkingHour = sheet.Cells[sheetStartRow, endCol + 10];
                    var totalLateDayCell = sheet.Cells[sheetStartRow, endCol + 11];
                    var totalEarlyDayCell = sheet.Cells[sheetStartRow, endCol + 12];
                    var lackingHour_RP = sheet.Cells[sheetStartRow, endCol + 13];
                    var lackingHour_ADJ = sheet.Cells[sheetStartRow, endCol + 14];
                    var deductLate = sheet.Cells[sheetStartRow, endCol + 15];
                    var totalWorkingDate = sheet.Cells[sheetStartRow, endCol + 16];
                    var workingDateDifference = sheet.Cells[sheetStartRow, endCol + 17];
                    var forgetCheckInOut = sheet.Cells[sheetStartRow, endCol + 18];
                    var timesheetCode = sheet.Cells[sheetStartRow, endCol + 19];
                    var note = sheet.Cells[sheetStartRow, endCol + 20];
                    #endregion

                    #region Styles
                    workingDate.Style.Font.Bold = total1_RP.Style.Font.Bold = total2_RP.Style.Font.Bold = total1_ADJ.Style.Font.Bold = total2_ADJ.Style.Font.Bold = true;
                    averageWorkingHour.Style.Font.Bold = totalLateDayCell.Style.Font.Bold = totalEarlyDayCell.Style.Font.Bold = lackingHour_RP.Style.Font.Bold = lackingHour_ADJ.Style.Font.Bold = true;
                    deductLate.Style.Font.Bold = totalWorkingDate.Style.Font.Bold = workingDateDifference.Style.Font.Bold = forgetCheckInOut.Style.Font.Bold = timesheetCode.Style.Font.Bold = note.Style.Font.Bold = true;

                    total1_RP.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.TEXT_BLUE));
                    total2_RP.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.TEXT_BLUE));
                    total1_ADJ.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.TEXT_BLUE));
                    total2_ADJ.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.TEXT_BLUE));
                    lackingHour_RP.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                    lackingHour_ADJ.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                    deductLate.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));

                    workingDate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    totalWHCell_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    total1_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    total2_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    totalWHCell_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    total1_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    total2_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    averageWorkingHour.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    totalLateDayCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    totalEarlyDayCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    lackingHour_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    lackingHour_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    deductLate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    totalWorkingDate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    workingDateDifference.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    forgetCheckInOut.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    timesheetCode.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    note.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                    checkInCell_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkInCellInteger_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkOutCell_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkOutCellInteger_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    actualWorkingHoursCell_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    workingHour_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkInCell_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkInCellInteger_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkOutCell_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    checkOutCellInteger_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    actualWorkingHoursCell_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    workingHour_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    inLate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    outEarly.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                    leaveOfAskedDay.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    offWithoutNotice.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    leaveOfAskedDay.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.GREEN));
                    offWithoutNotice.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.GREEN));
                    leaveOfAskedDay.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    offWithoutNotice.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                    checkInCell_ADJ.Style.Fill.PatternType =
                            checkInCellInteger_ADJ.Style.Fill.PatternType =
                            checkOutCell_ADJ.Style.Fill.PatternType =
                            checkOutCellInteger_ADJ.Style.Fill.PatternType =
                            actualWorkingHoursCell_ADJ.Style.Fill.PatternType =
                            workingHour_ADJ.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                    checkInCell_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    checkInCellInteger_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    checkOutCell_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    checkOutCellInteger_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    workingHour_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    actualWorkingHoursCell_ADJ.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.CORE_BLUE));
                    #endregion

                    sheet.Cells[sheetStartRow, 1].Value = (sheetStartRow - 4);
                    sheet.Cells[sheetStartRow, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    sheet.Cells[sheetStartRow, 2].Value = staff.Code;
                    sheet.Cells[sheetStartRow, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    sheet.Cells[sheetStartRow, 3].Value = staff.Name;
                    sheet.Cells[sheetStartRow, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                    sheet.Cells[sheetStartRow, 4].Value = staff.Position;
                    sheet.Cells[sheetStartRow, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);

                    if (staffDailySheetData != null)
                    {
                        var staffTimeSheet = staffDailySheetData.StaffTimeSheets.Where(x => x.StaffCode.Equals(staff.Code)).FirstOrDefault();
                        staff.WeekLackingHours.Add($"='{sheet.Name}'!{lackingHour_ADJ.Address}");
                        staff.WeekDeductLates.Add($"='{sheet.Name}'!{deductLate.Address}");

                        if (staffTimeSheet != null)
                        {
                            var code = staffTimeSheet.StaffCode;
                            var checkInTime = staffTimeSheet.CheckInTime;
                            var checkOutTime = staffTimeSheet.CheckOutTime;

                            #region REPORT
                            var checkIn = WorkingHoursUtils.GetWorkingTime(checkInTime);
                            var checkOut = WorkingHoursUtils.GetWorkingTime(checkOutTime);
                            checkInCell_RP.Style.Numberformat.Format =
                            checkInCell_ADJ.Style.Numberformat.Format =
                            checkOutCell_ADJ.Style.Numberformat.Format =
                            checkOutCell_RP.Style.Numberformat.Format = "hh:mm";

                            if (checkIn.Item1)
                            {
                                checkInCell_RP.Value = checkInCell_ADJ.Value = checkIn.Item2;
                                var condition = sheet.ConditionalFormatting.AddExpression(checkInCell_RP);
                                condition.Formula = $"IF(AND({checkInCellInteger_RP.Address}>10,{checkInCellInteger_RP.Address}<12),1, 0)";
                                condition.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                condition.Style.Fill.BackgroundColor.Color = ColorTranslator.FromHtml("#e26c0a");
                            }
                            else
                            {
                                checkInCell_RP.Value = checkInCell_ADJ.Value = checkIn.Item3;
                                checkInCell_RP.Style.Fill.PatternType =
                                    checkInCell_ADJ.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                checkInCell_RP.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                checkInCell_ADJ.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }

                            if (checkOut.Item1)
                            {
                                checkOutCell_RP.Value = checkOutCell_ADJ.Value = checkOut.Item2;
                            }
                            else
                            {
                                checkOutCell_RP.Value = checkOutCell_ADJ.Value = checkOut.Item3;
                                checkOutCell_RP.Style.Fill.PatternType =
                                checkOutCell_ADJ.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                checkOutCell_RP.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                checkOutCell_ADJ.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            }

                            checkInCellInteger_RP.Formula = $"IF({checkInCell_RP.Address}=\"-\",0,({checkInCell_RP.Address}-INT({checkInCell_RP.Address}))*24)";
                            checkOutCellInteger_RP.Formula = $"IF({checkOutCell_RP.Address}=\"-\",0,({checkOutCell_RP.Address}-INT({checkOutCell_RP.Address}))*24)";
                            actualWorkingHoursCell_RP.Formula = $"IF(({checkOutCellInteger_RP.Address}-{checkInCellInteger_RP.Address})=0,0,IF(OR({checkOutCellInteger_RP.Address}=0,{checkInCellInteger_RP.Address}=0),0," +
                                $"ROUND({checkOutCellInteger_RP.Address}-{checkInCellInteger_RP.Address}-IF({checkInCellInteger_RP.Address}>=13,0,IF({checkOutCellInteger_RP.Address}<=12,0," +
                                $"IF(AND({checkOutCellInteger_RP.Address}>12,{checkOutCellInteger_RP.Address}<13),{checkOutCellInteger_RP.Address}-12,IF(AND(12<{checkOutCellInteger_RP.Address},{checkOutCellInteger_RP.Address}<13),13-{checkOutCellInteger_RP.Address},1)))),2)))";
                            checkInCellInteger_RP.Calculate();
                            checkOutCellInteger_RP.Calculate();
                            actualWorkingHoursCell_RP.Calculate();
                            actualWorkingHoursCell_RP.Style.Font.Color.SetColor(WorkingHoursUtils.CheckEnoughTime(actualWorkingHoursCell_RP.Value?.ToString(), 8) ? Color.Blue : Color.Red);
                            actualWorkingHoursCell_RP.Style.Font.Bold = true;

                            workingHour_RP.Formula = $"IF({actualWorkingHoursCell_RP.Address}<10,{actualWorkingHoursCell_RP.Address},10)";
                            workingHour_RP.Calculate();
                            workingHour_RP.Style.Font.Color.SetColor(WorkingHoursUtils.CheckEnoughTime(workingHour_RP.Value?.ToString(), 8) ? Color.Blue : Color.Red);
                            workingHour_RP.Style.Font.Bold = true;
                            actualWorkingHoursCell_ADJ.Style.Font.Bold = true;
                            #endregion

                            #region ADJUST
                            checkInCellInteger_ADJ.Formula = $"IF({checkInCell_ADJ.Address}=\"-\",0,({checkInCell_ADJ.Address}-INT({checkInCell_ADJ.Address}))*24)";
                            checkOutCellInteger_ADJ.Formula = $"IF({checkOutCell_ADJ.Address}=\"-\",0,({checkOutCell_ADJ.Address}-INT({checkOutCell_ADJ.Address}))*24)";

                            actualWorkingHoursCell_ADJ.Formula = $"IF(({checkOutCellInteger_ADJ.Address}-{checkInCellInteger_ADJ.Address})=0,0,IF(OR({checkOutCellInteger_ADJ.Address}=0,{checkInCellInteger_ADJ.Address}=0),0," +
                                $"ROUND({checkOutCellInteger_ADJ.Address}-{checkInCellInteger_ADJ.Address}-IF({checkInCellInteger_ADJ.Address}>=13,0,IF({checkOutCellInteger_ADJ.Address}<=12,0," +
                                $"IF(AND({checkOutCellInteger_ADJ.Address}>12,{checkOutCellInteger_ADJ.Address}<13),{checkOutCellInteger_ADJ.Address}-12,IF(AND(12<{checkOutCellInteger_ADJ.Address},{checkOutCellInteger_ADJ.Address}<13),13-{checkOutCellInteger_ADJ.Address},1)))),2)))";
                            checkInCellInteger_ADJ.Calculate();
                            checkOutCellInteger_ADJ.Calculate();
                            actualWorkingHoursCell_ADJ.Calculate();
                            actualWorkingHoursCell_ADJ.Style.Font.Color.SetColor(WorkingHoursUtils.CheckEnoughTime(actualWorkingHoursCell_ADJ.Value?.ToString(), 8) ? Color.Blue : Color.Red);

                            workingHour_ADJ.Formula = $"IF({actualWorkingHoursCell_ADJ.Address}<10,{actualWorkingHoursCell_ADJ.Address},10)";
                            workingHour_ADJ.Calculate();
                            workingHour_ADJ.Style.Font.Color.SetColor(WorkingHoursUtils.CheckEnoughTime(workingHour_ADJ.Value?.ToString(), 8) ? Color.Blue : Color.Red);
                            workingHour_ADJ.Style.Font.Bold = true;
                            #endregion

                            #region In Late & Out Late
                            inLate.Formula = $"IF(AND({checkInCellInteger_RP.Address}>10.01,{checkInCellInteger_RP.Address}<12),1,0)";
                            outEarly.Formula = $"IF(AND({checkOutCellInteger_RP.Address}<17,{checkOutCellInteger_RP.Address}>14.5),1,0)";
                            inLate.Calculate();
                            outEarly.Calculate();
                            var inLateVal = inLate.Value.ToString();
                            var outEarlyVal = outEarly.Value.ToString();
                            inLate.Style.Font.Color.SetColor(int.Parse(inLateVal) == 1 ? ColorTranslator.FromHtml(WH_COLORS.RED) : Color.Black);
                            outEarly.Style.Font.Color.SetColor(int.Parse(outEarlyVal) == 1 ? ColorTranslator.FromHtml(WH_COLORS.RED) : Color.Black);

                            #endregion
                        }
                    }

                    #region Cal Total
                    for (var i = 1; i <= totalWeekDayDatas; i++)
                    {
                        workingDays += 1;
                        totalWH_RP_Fomula += string.Format("{0}{1}", sheet.Cells[sheetStartRow, col + 6].Address, (i < totalWeekDayDatas) ? comma : string.Empty);
                        totalWH_ADJ_Fomula += string.Format("{0}{1}", sheet.Cells[sheetStartRow, col + 12].Address, (i < totalWeekDayDatas) ? comma : string.Empty);
                        totalLateDays_Fomula += string.Format("{0}{1}", sheet.Cells[sheetStartRow, col + 13].Address, (i < totalWeekDayDatas) ? comma : string.Empty);
                        totalEarlyDays_Fomula += string.Format("{0}{1}", sheet.Cells[sheetStartRow, col + 14].Address, (i < totalWeekDayDatas) ? comma : string.Empty);
                        totalWorkingDays_Fomula += string.Format("{0}{1}", $"IF({sheet.Cells[sheetStartRow, col + 5].Address}=0,0,IF({sheet.Cells[sheetStartRow, col + 5].Address}>6,1,{workingDateUnit}))", (i < totalWeekDayDatas) ? comma : string.Empty);
                        col = col + 14;
                    }

                    #region Styles
                    for (var i = columns; i >= 20; i--) sheet.Column(i).Width = 7;

                    sheet.Column(endCol + 13).Width =
                    sheet.Column(endCol + 14).Width = 10;
                    #endregion

                    totalWHCell_RP.Formula = $"SUM({totalWH_RP_Fomula})";
                    totalWHCell_RP.Calculate();
                    totalWHCell_RP.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                    totalWHCell_RP.Style.Font.Bold = true;
                    workingDate.Formula = $"IF(${totalWHCell_RP.Address} = 0,0,IF({sheet.Cells[sheetStartRow, 2].Address}=\"\",0,{workingDays}-{leaveOfAskedDay.Address}-{offWithoutNotice.Address}-$C$1))";
                    totalWHCell_ADJ.Formula = $"SUM({totalWH_ADJ_Fomula})";
                    totalWHCell_ADJ.Calculate();
                    totalWHCell_ADJ.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                    totalWHCell_ADJ.Style.Font.Bold = true;
                    totalLateDayCell.Formula = $"SUM({totalLateDays_Fomula})";
                    totalEarlyDayCell.Formula = $"SUM({totalEarlyDays_Fomula})";

                    total1_RP.Formula = $"IF({totalWHCell_RP.Address}>0,LEFT({totalWHCell_RP.Address},2),0)";
                    total2_RP.Formula = $"=(({totalWHCell_RP.Address}-{total1_RP.Address})*100)*60/100";

                    total1_ADJ.Formula = $"IF({totalWHCell_ADJ.Address}>0,LEFT({totalWHCell_ADJ.Address},2),0)";
                    total2_ADJ.Formula = $"=(({totalWHCell_ADJ.Address}-{total1_ADJ.Address})*100)*60/100";

                    averageWorkingHour.Formula = $"IF({workingDate.Address}>0,{totalWHCell_RP.Address}/{workingDate.Address},0)";
                    lackingHour_RP.Formula = $"IF({totalWHCell_RP.Address}=0,0,IF({totalWHCell_RP.Address}-{workingDate.Address}*8>0,0,{totalWHCell_RP.Address}-{workingDate.Address}*8))";
                    lackingHour_ADJ.Formula = $"IF({totalWHCell_ADJ.Address}=0,0,IF({totalWHCell_ADJ.Address}-{workingDate.Address}*8>0,0,{totalWHCell_ADJ.Address}-{workingDate.Address}*8))";
                    deductLate.Formula = $"IF({totalLateDayCell.Address}>=3,100000,0)";
                    totalWorkingDate.Formula = $"SUM({totalWorkingDays_Fomula})";
                    totalWorkingDate.Calculate();
                    workingDateDifference.Formula = $"IF({workingDate.Address}={totalWorkingDate.Address},0,{workingDate.Address}-{totalWorkingDate.Address})";

                    averageWorkingHour.Calculate();
                    var totalWhCondition = sheet.ConditionalFormatting.AddExpression(totalWHCell_RP);
                    totalWhCondition.Formula = $"IF(AND({averageWorkingHour.Address}>0.1,{averageWorkingHour.Address}<7.99),1, 0)";
                    totalWhCondition.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    totalWhCondition.Style.Fill.BackgroundColor.Color = Color.Yellow;

                    var averageWhCondition = sheet.ConditionalFormatting.AddExpression(averageWorkingHour);
                    averageWhCondition.Formula = $"IF(AND({averageWorkingHour.Address}>0.1,{averageWorkingHour.Address}<7.99),1, 0)";
                    averageWhCondition.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    averageWhCondition.Style.Fill.BackgroundColor.Color = Color.Yellow;
                    #endregion

                    sheetStartRow++;
                }
            }

            return Task.CompletedTask;
        }

        private void RenderHeader(ExcelWorksheet sheet, int lastCol)
        {
            var report_1Cell = sheet.Cells[3, (lastCol + 4), 3, (lastCol + 4 + 2)];
            var report_2Cell = sheet.Cells[3, (lastCol + 13)];
            var report_1CellRow4 = sheet.Cells[4, (lastCol + 4), 4, (lastCol + 4 + 2)];
            var report_2CellRow4 = sheet.Cells[4, (lastCol + 7), 4, (lastCol + 7 + 2)];
            var adjust_1Cell = sheet.Cells[3, (lastCol + 7), 3, (lastCol + 7 + 2)];
            var adjust_2Cell = sheet.Cells[3, lastCol + 14];
            var leaveOfAskedDay = sheet.Cells[4, lastCol + 1];
            var offWithoutNotice = sheet.Cells[4, lastCol + 2];
            var workingDate = sheet.Cells[4, lastCol + 3];
            var avarageWorkingHours = sheet.Cells[4, lastCol + 10];
            var totalWorkLate = sheet.Cells[4, lastCol + 11];
            var totalLeaveEarly = sheet.Cells[4, lastCol + 12];
            var lackingHour_RP = sheet.Cells[4, lastCol + 13];
            var lackingHour_ADJ = sheet.Cells[4, lastCol + 14];

            var deductLate = sheet.Cells[4, lastCol + 15];
            var workingDate_TT = sheet.Cells[4, lastCol + 16];
            var workingDateDiff = sheet.Cells[4, lastCol + 17];
            var forgetCheckInOut = sheet.Cells[4, lastCol + 18];
            var timeSheetCode = sheet.Cells[4, lastCol + 19];
            var note = sheet.Cells[4, lastCol + 20];

            #region styles row 3
            report_1Cell.Merge =
            adjust_1Cell.Merge =
            report_1CellRow4.Merge =
            report_2CellRow4.Merge = true;

            report_1Cell.Value = report_2Cell.Value = "REPORT";
            adjust_1Cell.Value = adjust_2Cell.Value = "ADJUST";
            report_1Cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            report_2Cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            adjust_1Cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            adjust_2Cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));

            report_1Cell.Style.Fill.PatternType =
            report_2Cell.Style.Fill.PatternType =
            adjust_1Cell.Style.Fill.PatternType =
            adjust_2Cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

            report_1Cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            report_2Cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            adjust_1Cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            adjust_2Cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            report_1Cell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            report_2Cell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            adjust_2Cell.Style.Fill.BackgroundColor.SetColor(Color.LightCyan);
            adjust_1Cell.Style.Fill.BackgroundColor.SetColor(Color.LightCyan);
            #endregion
            #region styles row 4
            leaveOfAskedDay.Value = "Leave Off asked (days)";
            leaveOfAskedDay.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            leaveOfAskedDay.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.GREEN));
            leaveOfAskedDay.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            leaveOfAskedDay.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            leaveOfAskedDay.Style.Font.Bold = true;

            offWithoutNotice.Value = "Off without notice /planned (days)";
            offWithoutNotice.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            offWithoutNotice.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(WH_COLORS.GREEN));
            offWithoutNotice.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            offWithoutNotice.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            offWithoutNotice.Style.Font.Bold = true;

            workingDate.Value = "Working date";
            workingDate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            workingDate.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            workingDate.Style.Font.Bold = true;

            avarageWorkingHours.Value = "Avarage working hours";
            avarageWorkingHours.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            avarageWorkingHours.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            avarageWorkingHours.Style.Font.Bold = true;

            totalWorkLate.Value = "Total come to work late days";
            totalWorkLate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalWorkLate.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            totalWorkLate.Style.Font.Bold = true;

            totalLeaveEarly.Value = "Total leave early days";
            totalLeaveEarly.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            totalLeaveEarly.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            totalLeaveEarly.Style.Font.Bold = true;

            lackingHour_RP.Value =
            lackingHour_ADJ.Value = "Lacking hour";
            lackingHour_RP.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            lackingHour_RP.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            lackingHour_ADJ.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            lackingHour_ADJ.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            lackingHour_RP.Style.Font.Bold = true;
            lackingHour_ADJ.Style.Font.Bold = true;

            deductLate.Value = "Deduct late";
            deductLate.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            deductLate.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            deductLate.Style.Font.Bold = true;

            workingDate_TT.Value = "Working date";
            workingDate_TT.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            workingDate_TT.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            workingDate_TT.Style.Font.Bold = true;

            workingDateDiff.Value = "Checking working date difference";
            workingDateDiff.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            workingDateDiff.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            workingDateDiff.Style.Font.Bold = true;

            forgetCheckInOut.Value = "Forget check in-out";
            forgetCheckInOut.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            forgetCheckInOut.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            forgetCheckInOut.Style.Font.Bold = true;

            timeSheetCode.Value = "Timesheet code";
            timeSheetCode.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            timeSheetCode.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            timeSheetCode.Style.Font.Bold = true;

            note.Value = "Note";
            note.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            note.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            note.Style.Font.Bold = true;

            report_1CellRow4.Value = "Total Working hours/Week";
            report_1CellRow4.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            report_1CellRow4.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            report_1CellRow4.Style.Font.Bold = true;

            report_2CellRow4.Value = "Total Working hours/Week";
            report_2CellRow4.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
            report_2CellRow4.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
            report_2CellRow4.Style.Font.Bold = true;
            #endregion
        }

        public void ResetAll()
        {
            file = null;
            _weekSheets.Clear();
            _groupStaffs.Clear();
            _dailyReports.Clear();
            panelResult.Visible = false;
            panelProgress.Visible = false;
            lbStatus.Text = string.Empty;
            Application.DoEvents();
            this.Update();
        }

        private void buttonExportFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    package.SaveAs(new FileInfo(saveDialog.FileName));
                    MessageBox.Show("Export Successful.");
                    package.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
