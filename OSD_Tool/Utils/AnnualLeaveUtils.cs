using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OSD_Tool.Utils
{
	public class DateRange
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsWeekend => AnnualLeaveUtils.CheckIsWeekendDate(Date);
        public bool IsLastDateOfPrevMonth { get; set; }
    }

    public class AnnualLeaveUtils
    {
        public static bool CheckDayTraineeExpire(DateTime startingDay, string group=null)
        {
            if (group == Constants.INTERN) return false;

            var currentDate = DateTime.Now.Date;
            var dayTraineeExpire = startingDay.AddMonths(2).AddDays(-1).Date;
			if(currentDate >= dayTraineeExpire)
            {
                return true;
            }
            return false;
        }

        public static float GetAnnualLeaveDayForStaff(DateTime startingDay, string group=null)
        {
            bool IsEmployee = CheckDayTraineeExpire(startingDay, group);

            if (!IsEmployee) return 0;

            var day = startingDay.Day;
            var noDayLeaveSetting = ConfigurationManager.AppSettings["NoLeaveRange"].ToString().Split('|');
            var oneDayLeaveSetting = ConfigurationManager.AppSettings["OneDayLeaveRange"].ToString().Split('|');
            var halfDayLeaveSetting = ConfigurationManager.AppSettings["HalfDayLeaveRange"].ToString().Split('|');
            List<string[]> settings = new List<string[]>() { noDayLeaveSetting, oneDayLeaveSetting, halfDayLeaveSetting };

            for (var i = 0; i < settings.Count; i++)
            {
                var setting = settings[i];
                var settingRanges = setting[0].Split('-').Select(r => int.Parse(r)).ToArray();
                float settingValue = float.Parse(setting[1]);
                if (settingRanges.Count() > 1)
                {
                    int rangeFrom = settingRanges[0];
                    int rangeTo = settingRanges[1];
                    if (rangeFrom <= day && day <= rangeTo)
                    {
                        return settingValue;
                    }
                }
                else if (settingRanges.Count() == 1)
                {
                    if (day >= settingRanges[0])
                        return settingValue;
                }
            }

            return 0;
        }

		public static List<DateRange> GetDateRanges()
        {
            List<DateRange> dateRanges = new List<DateRange>();
            int startDuringDate = 21;
            int endDuringDate = 20;
            int year = DateTime.Now.Year;
            int previousMonth = DateTime.Now.AddMonths(-1).Month;
            int currentMonth = DateTime.Now.Month;
            int daysOfPrevMonth = DateTime.DaysInMonth(year, previousMonth);

			for(var i = startDuringDate; i <= daysOfPrevMonth; i++)
            {
                bool isLastDateOfPrevMonth = (i == daysOfPrevMonth - 1);
                dateRanges.Add(new DateRange
                {
                    Text = i.ToString(),
					Date = new DateTime(year, previousMonth, i),
					IsLastDateOfPrevMonth = isLastDateOfPrevMonth
                });
            }

			for(var i = 1; i <= endDuringDate; i++)
            {
                dateRanges.Add(new DateRange
                {
                    Text = i.ToString(),
                    Date = new DateTime(year, currentMonth, i)
                });
            }

            return dateRanges;
        }

		public static bool CheckIsWeekendDate(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
