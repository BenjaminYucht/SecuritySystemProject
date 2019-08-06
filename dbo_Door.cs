using System.Collections.Generic;
using System.Collections;

namespace SecuritySystemsProgram
{
    public class Door
    {
        public int DoorID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DoorSecurityLevelHistory> DoorSecurityLevelHistory { get; set; }
        public virtual ICollection<EmployeeSecurityLevelHistory> EmployeeSecurityLevelHistory { get; set; }

       
    }
}