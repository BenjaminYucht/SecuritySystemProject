using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemsProgram
{
    public class AccessHistoryAccessMethod
    {
       public int AccessHistoryAccessMethodID { get; set; }

       public int AccessHistoryID { get; set; }
       public virtual AccessHistory AccessHistory { get; set; }

       public int AccessMethodID { get; set; }
       public virtual AccessMethod AccessMethod { get; set; }
            
    }
}