using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemsProgram
{
    public class SecurityLevelAccessMethod
    {
        public int SecurityLevelAccessMethodID { get; set; }

        
        public int SecurityLevelID { get; set; }
        public virtual SecurityLevel SecurityLevel { get; set; }

        public int AccessMethodID { get; set; }
        public virtual AccessMethod AccessMethod { get; set; }


    }
}