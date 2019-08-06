using System.Collections.Generic;

namespace SecuritySystemsProgram
{
    public class SecurityLevel
    {
        public int SecurityLevelID { get; set; }
        public string Type { get; set; }

        public virtual ICollection<DoorSecurityLevelHistory> DoorSecurityLevelHistory { get; set; }
        public virtual ICollection<EmployeeSecurityLevelHistory> EmployeeSecurityLevelHistory { get; set; }
        public virtual ICollection<SecurityLevelAccessMethod> SecurityLevelAccessMethod { get; set; }

    }
}