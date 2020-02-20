using System.Collections.Generic;

namespace OSD_Tool.Models
{
    public class FullWHModel
    {
        public class DailyReport
        {
            public string Name { get; set; }
            public List<StaffTimeSheet> StaffTimeSheets { get; set; }

            public DailyReport()
            {
                StaffTimeSheets = new List<StaffTimeSheet>();
            }
        }

        public class StaffTimeSheet
        {
            public string StaffCode { get; set; }
            public string CheckInTime { get; set; }
            public string CheckOutTime { get; set; }
        }

        public class Staff
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Group { get; set; }
            public List<string> WeekLackingHours { get; set; }
            public List<string> WeekDeductLates { get; set; }


            public Staff()
            {
                WeekLackingHours = new List<string>();
                WeekDeductLates = new List<string>();
            }
        }

        public class WeekDayData
        {
            public string Date { get; set; }
            public string DateText { get; set; }
            public string SheetName { get; set; }
        }

        public class WeekSheet
        {
            public string Name { get; set; }
            public List<WeekDayData> WeekDayDatas { set; get; }

            public WeekSheet()
            {
                WeekDayDatas = new List<WeekDayData>();
            }
        }

        public class GroupStaff
        {
            public string Name { get; set; }
            public List<Staff> Staffs { get; set; }

            public GroupStaff()
            {
                Staffs = new List<Staff>();
            }
        }
    }
}
