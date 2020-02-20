using OfficeOpenXml;
using OSD_Tool.Models;
using OSD_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OSD_Tool.Models.FullWHModel;

namespace OSD_Tool.Services
{
    public class WorkingHourService
    {
        private WeekSheet ws;
        private int dayCount;
        public WorkingHourService()
        {
            ws = new WeekSheet();
            dayCount = 0;
        }

        #region ReadDailyReportDataSheetAndWeekDays
        public async Task ReadDailyReportDataSheetAndWeekDays(List<ExcelWorksheet> workingSheets, List<DailyReport> dailyReports, List<WeekSheet> weekSheets)
        {
            int index = 0;
            foreach (var sheet in workingSheets)
            {
                index++;
                int startRow = 8;
                var totalRows = sheet.Dimension.End.Row;
                var dailyReport = new DailyReport { Name = sheet.Name };
                await CollectWeekSheetData(workingSheets.Count, sheet, weekSheets, index);
                var lastPartIndex = WorkingHoursUtils.Split2Loop(totalRows);
                await Task.WhenAll(ReadTimeSheetByPart(sheet, dailyReport, startRow, lastPartIndex), ReadTimeSheetByPart(sheet, dailyReport, lastPartIndex, totalRows));
                dailyReports.Add(dailyReport);
            }
        }

        #region private method
        private Task ReadTimeSheetByPart(ExcelWorksheet sheet, DailyReport dailyReport, int from, int to)
        {
            for (var i = from; i < to; i++)
            {
                var staffCode = sheet.Cells[i, 2].Value?.ToString();
                var checkInTime = sheet.Cells[i, 8].Value?.ToString();
                var checkOutTime = sheet.Cells[i, 10].Value?.ToString();

                if (!string.IsNullOrEmpty(staffCode))
                    dailyReport.StaffTimeSheets.Add(new StaffTimeSheet { StaffCode = staffCode, CheckInTime = checkInTime, CheckOutTime = checkOutTime });
                else
                    break;
            }
            return Task.CompletedTask;
        }

        private Task CollectWeekSheetData(int totalSheets, ExcelWorksheet sheet, List<WeekSheet> weekSheets, int indexCount)
        {
            dayCount += 1;
            var startRow = 8; //Row start in a day excel sheet
            var dateCol = 5; //Col to get date
            var dateOfSheet = sheet.Cells[startRow, dateCol].Value?.ToString();

            if (!string.IsNullOrEmpty(dateOfSheet))
            {
                try
                {
                    DateTime dateSheet = new DateTime();
                    dateSheet = DateTime.ParseExact(dateOfSheet, Constants.WH_INPUT_DATE_FORMAT, null);
                    // Auto add day sheet in a weeksheet
                    ws.WeekDayDatas.Add(new WeekDayData
                    {
                        Date = dateOfSheet,
                        SheetName = sheet.Name,
                        DateText = string.Format("Day {0}", dayCount)
                    });

                    if (dateSheet.DayOfWeek == DayOfWeek.Friday || (indexCount == totalSheets))
                    {
                        ws.Name +=
                            ws.WeekDayDatas.Count == 1 ?
                            dateSheet.ToString(Constants.WH_SHEETNAME_FORMAT) :
                            dateSheet.ToString(Constants.WH_SHEETNAME_SPLIT_FORMAT);

                        // Add weeksheet to the list and Reset WeekSheet
                        weekSheets.Add(ws);
                        ws = new WeekSheet();
                    }
                    else if (ws.WeekDayDatas.Count == 1)
                    {
                        ws.Name = dateSheet.ToString(Constants.WH_SHEETNAME_FORMAT);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }


            return Task.CompletedTask;
        }

        #endregion

        #endregion

        public void RenderFirstPart(ExcelWorksheet ws)
        {
            var rowFour = ws.Row(4);
            var firstCell = ws.Cells[1, 1];
            var secondCell = ws.Cells[1, 2];
            var thirdCell = ws.Cells[1, 3];

            rowFour.Height = 75;
            rowFour.Style.WrapText = true;

            firstCell.Value = "Holiday in this week (days):";
            firstCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            firstCell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            firstCell.Style.Font.SetFromFont(new Font("Times New Roman", 10));
            secondCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            secondCell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);
            thirdCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            thirdCell.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            for (var i = 0; i < Constants.WH_FIRST_4COLUMN_HEADERS.Count; i++)
            {
                var cell = ws.Cells[4, i + 1];
                cell.Value = Constants.WH_FIRST_4COLUMN_HEADERS[i];
                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dotted);
                cell.Style.Font.Color.SetColor(ColorTranslator.FromHtml(WH_COLORS.RED));
                cell.Style.Font.Bold = true;
            }
        }
    }
}
