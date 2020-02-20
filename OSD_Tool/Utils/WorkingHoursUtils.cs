using OfficeOpenXml;
using System;

namespace OSD_Tool.Utils
{
    public class MergedRangeInfo
    {
        public string Address { get; set; }
        public int TotalColumns { get; set; }
        public bool IsMerged { get; set; }
    }

    public static class WorkingHoursUtils
    {
        public static MergedRangeInfo GetMergedRangeAddress(this ExcelRange excRange, ExcelWorksheet ws)
        {
            var cellInfo = new MergedRangeInfo
            {
                Address = excRange.Address,
                IsMerged = excRange.Merge
            };

            if (excRange.Merge)
            {
                var idx = excRange.Worksheet.GetMergeCellId(excRange.Start.Row, excRange.Start.Column);
                var address = excRange.Worksheet.MergedCells[idx - 1]; //the array is 0-indexed but the mergeId is 1-indexed...
                var totalColumns = ws.Cells[address].Columns;
                cellInfo.Address = address;
                cellInfo.TotalColumns = totalColumns;
            }

            return cellInfo;
        }

        public static bool CheckEnoughTime(string timeValue, decimal targetValue)
        {
            return timeValue != null && timeValue != "-" && decimal.Parse(timeValue) >= targetValue;
        }

        public static Tuple<bool, DateTime, string> GetWorkingTime(string timeValue)
        {
            double timeDouble;
            var isTime = double.TryParse(timeValue, out timeDouble);
            return Tuple.Create(isTime, DateTime.FromOADate(timeDouble), "-");
        }

        public static int Split2Loop(int totalRecords)
        {
            int lastPartOneIndex = 0;
            if (totalRecords % 2 == 0)
            {
                lastPartOneIndex = (totalRecords / 2);
            }
            else
            {
                lastPartOneIndex = (int)Math.Round((double)(totalRecords / 2));
            }

            return lastPartOneIndex;
        }
    }
}
