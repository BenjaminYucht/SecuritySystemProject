using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecuritySystemsProgram
{
    public class AccessMethod
    {
       
        public int  AccessMethodID { get; set; }
        public string Name { get; set; }
        public bool ProofOfIdentity  { get; set; }
        public virtual ICollection<AccessHistoryAccessMethod> AccessHistoryAccessMethod { get; set; }
        public virtual ICollection<SecurityLevelAccessMethod> SecurityLevelAccessMethod { get; set; }
    }
}