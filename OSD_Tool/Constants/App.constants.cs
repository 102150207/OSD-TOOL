using System;
using System.Collections.Generic;

namespace OSD_Tool
{
    public class Constants
    {
        public const string INTERN = "INTERNS";
        public const string DATE_FM_MMM_YYYY = "MMM, yyyy";
        public const string DATE_FM_MMMM_YYYY = "MMMM, yyyy";

        public static List<string> AnnualLeaveHeader10Columns = new List<string>
        {
            "No",
            "Code",
            "Email",
            "Group Name",
            "Position",
            "Starting day",
            string.Format("End of 20-{0}", DateTime.Now.AddMonths(-1).ToString(DATE_FM_MMM_YYYY)),
            string.Format("Compensati for OT/ Marriage/ Meternity in {0}", DateTime.Now.ToString(DATE_FM_MMMM_YYYY)),
            string.Format("Leave of {0}", DateTime.Now.ToString(DATE_FM_MMM_YYYY)),
            string.Format("Total Annual leaves until {0}", DateTime.Now.ToString(DATE_FM_MMM_YYYY)),
        };

        public static List<string> AnnualLeaveHeaderLast4Columns = new List<string>
        {
            "Total day off",
            "Balance Normal",
            string.Format("Deduct in {0} 's salary", DateTime.Now.ToString(DATE_FM_MMMM_YYYY)),
            string.Format("Balance until {0}'20", DateTime.Now.ToString("MMMM"))
        };


        // Working Hours
        public const int WH_SKIP_COULUMNS = 4;
        public const string WH_INPUT_DATE_FORMAT = "dd/MM/yyyy";
        public const string WH_SHEETNAME_FORMAT = "MMM dd";
        public const string WH_SHEETNAME_SPLIT_FORMAT = "_MMM dd";
        public static List<string> WH_DAY_HEADER_COLUMNS = new List<string>
        {
            "In (hours)",
            "In (number format)",
            "Out (hours)",
            "Out (number format)",
            "Actual Working hours",
            "Working hours"
        };

        public static List<string> WH_FIRST_4COLUMN_HEADERS = new List<string>
        {
            "NO",
            "CODE",
            "GROUP NAME",
            "POSITION"
        };
    }

    public class WH_COLORS
    {
        public const string RED = "#ff0000";
        public const string GREEN = "#d8e5ba";
        public const string CORE_BLUE = "#ddebf7";
        public const string TEXT_BLUE = "#0070c0";
    }
}
