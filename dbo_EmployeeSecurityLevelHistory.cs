using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemsProgram
{
    public class EmployeeSecurityLevelHistory
    {
        public int EmployeeSecurityLevelHistoryID { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int SecurityLevelID { get; set; }
        public virtual SecurityLevel SecurityLevel { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}