using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemsProgram
{
    public class AccessHistory
    {
        
        public int AccessHistoryID { get; set; }

        public int DoorID { get; set; }
        public virtual Door Door { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime Date { get; set; }
        public bool Succeeded { get; set; }
        public virtual ICollection<AccessHistoryAccessMethod> AccessHistoryAccessMethod { get; set; }

    }
}