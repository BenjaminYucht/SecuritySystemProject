using System.Collections.Generic;

namespace SecuritySystemsProgram
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public virtual ICollection<AccessHistory> AccessHistory { get; set; }
        public virtual ICollection<EmployeeSecurityLevelHistory> EmployeeSecurityLevelHistory { get; set; }


    }
}