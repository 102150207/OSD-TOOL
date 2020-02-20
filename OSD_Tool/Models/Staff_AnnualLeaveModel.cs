using System;
using System.ComponentModel;

namespace OSD_Tool.Models
{
    public class DateAnnualLeave
    {
        public DateTime Date { get; set; }
        public double Leave { get; set; }
    }

    public class Staff_AnnualLeaveModel
    {
        public Staff_AnnualLeaveModel()
        {
            this.DateAnnualLeaves = new BindingList<DateAnnualLeave>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Position { get; set; }
        public DateTime StartDay { get; set; }
        public double OTOf20 { get; set; }
        public double BalanceUtil20 { get; set; }
        public virtual BindingList<DateAnnualLeave> DateAnnualLeaves { get; set; }
    }
}
