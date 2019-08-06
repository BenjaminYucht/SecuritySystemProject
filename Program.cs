using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SecuritySystemsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SecuritySystemsContext SSC = new SecuritySystemsContext())
            {

                SSC.GetSuspicAccessHis();
                



                Console.ReadLine();
            }
        }

    }
}
