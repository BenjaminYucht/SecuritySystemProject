using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemsProgram
{
    public class DoorSecurityLevelHistory
    {
        public int DoorSecurityLevelHistoryID {get; set;}

        public int DoorID { get; set; }
        public virtual Door Door { get; set; }

        public int SecurityLevelID { get; set; }
        public virtual SecurityLevel SecurityLevel { get; set; }

        public DateTime StartDate { get; set; }

        
        public DateTime? EndDate { get; set; }


    }
}