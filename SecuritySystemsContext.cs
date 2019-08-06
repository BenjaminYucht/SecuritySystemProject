using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemsProgram
{
    public class SecuritySystemsContext : DbContext
    {
        public DbSet<AccessHistory> AccessHistories { get; set; }
        public DbSet<AccessHistoryAccessMethod> AccessHistoryAccessMethods { get; set; }
        public DbSet<AccessMethod> AccessMethods { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorSecurityLevelHistory> DoorSecurityLevelHistories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSecurityLevelHistory> EmployeeSecurityLevelHistories { get; set; }
        public DbSet<SecurityLevel> SecurityLevels { get; set; }
        public DbSet<SecurityLevelAccessMethod> SecurityLevelAccessMethods { get; set; }


        public int AccessedDoorID;
        public List<int> AccessMethodsIDs = new List<int>();
        public int NumOfReqs { get; set; }
        public void AccessRequest(Door Sender, List<AccessMethod> GivenReqs)
        {
            AccessedDoorID = Sender.DoorID;
            foreach (var Method in GivenReqs)
            {
                AccessMethodsIDs.Add(Method.AccessMethodID);
            }
            int i = DoorsCurrentSecurityLevel(AccessedDoorID);
            List<int> NeededReqs = SecurityLevelsAccessMethods(i);
            MeetsRequirments(NeededReqs, AccessMethodsIDs, NumOfReqs, AccessedDoorID);

        }
        public void RevokeAccess()
        {
            LockDoor();
        }

        public void LockDoor() { }
        public void IsAuthorized(bool GivenBool, int DoorID)
        {

            if (GivenBool == true)
            {
                GrantAccess(DoorID);
                Console.WriteLine("You passed Authorization and may enter");
            }
            else
            {
                Console.WriteLine("Authorization failed: Try again with the correct security requirments");
            }
        }
        public void GrantAccess(int DoorID)
        {


        }

        public int DoorsCurrentSecurityLevel(int DoorsID)
        {

            var a = from securitylevel in DoorSecurityLevelHistories
                    where securitylevel.DoorID == DoorsID && !securitylevel.EndDate.HasValue
                    select securitylevel.SecurityLevelID;
            var ToInt = 0;
            foreach (var item in a)
            {
                ToInt = item;
            }
            return ToInt;
        }

        public List<int> SecurityLevelsAccessMethods(int securitylevelID)
        {
            int NumOfReqs = 0;
            var a = from accessmethod in SecurityLevelAccessMethods
                    where accessmethod.SecurityLevelID == securitylevelID
                    select accessmethod.AccessMethodID;
            List<int> b = new List<int>();
            foreach (var item in a)
            {
                b.Add(item);
                NumOfReqs++;

            }
            this.NumOfReqs = NumOfReqs;
            return b;
        }
        public void MeetsRequirments(List<int> NeededReqs, List<int> ReqsGiven, int NumOfNeededReqs, int DoorID)
        {
            int NumOfReqsPassed = 0;
            for (int NeededReqsAmnt = 0; NeededReqsAmnt < NeededReqs.Count; NeededReqsAmnt++)
            {
                for (int ReqsGivenAmnt = 0; ReqsGivenAmnt < ReqsGiven.Count; ReqsGivenAmnt++)
                {
                    if (NeededReqs[NeededReqsAmnt] == ReqsGiven[ReqsGivenAmnt])
                    {
                        NumOfReqsPassed++;
                    }
                }
            }
            if (NumOfReqsPassed >= NumOfNeededReqs)
            {
                IsAuthorized(true, DoorID);
            }
            else
            {
                IsAuthorized(false, DoorID);
            }
        }



        public void AddAccessMethod(SecuritySystemsContext _SSC)
        {


            AccessMethod a = new AccessMethod();
            a.Name = Prompt("Name of Access Method");
            string UsrMsg = Prompt("Is this proof of identity");
            if (UsrMsg.Contains("yes"))
            {
                a.ProofOfIdentity = true;
            }
            else if (UsrMsg.Contains("no"))
            {
                a.ProofOfIdentity = false;
            }
            _SSC.AccessMethods.Add(a);
            _SSC.SaveChanges();
        }



        public void PrintResults(IQueryable<AccessHistory> accessHistories)
        {

            foreach (var Attempt in accessHistories)
            {
                string Message = "Exception";
                if (Attempt.Succeeded == true)
                {
                    Message = "Succeeded";
                }
                else if (Attempt.Succeeded == false)
                {
                    Message = "Failed";
                }

                Console.WriteLine("ID number:" + Attempt.AccessHistoryID + ". DoorID:" + Attempt.DoorID + ". EmplooyeeID:" + Attempt.EmployeeID + ". Date:" + Attempt.Date + ". Attempt " + Message);
            }

        }

        public void GetAccessHistoriesBtwTwoDates()
        {

            DateTime From = Convert.ToDateTime(Prompt("From Which Date"));
            DateTime To = Convert.ToDateTime(Prompt("Till Which Date"));
            var a = from Attempt in AccessHistories
                    where Attempt.Date >= From && Attempt.Date <= To
                    select Attempt;
            PrintResults(a);

        }
        public void GetAccessHistoriesByDoorID()
        {
            DateTime From = Convert.ToDateTime(Prompt("From Which Date"));
            DateTime To = Convert.ToDateTime(Prompt("Till Which Date"));
            int ID = Convert.ToInt32(Prompt("Which Door (by ID)"));
            var a = from Attempt in AccessHistories
                    where Attempt.DoorID == ID && Attempt.Date >= From && Attempt.Date <= To
                    select Attempt;
            PrintResults(a);

        }
        public string Prompt(string msg)
        {
            Console.WriteLine(msg);
            Console.Write(">");
            return Console.ReadLine();
        }
        public void GetSuspicAccessHis()
        {




            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=SecuritySystems2; Integrated Security=true"))
            {
                conn.Open();


                var cmnd = new SqlCommand(@"select 
             *
            from
             AccessHistories
            where
             Succeeded=0 and AccessHistoryID not in 
             (select
              NonSusFls.AccessHistoryID
             from
              (select 
           *
              from
               AccessHistories
              where
               Succeeded=0) as NonSusFls
              inner join
              (select 
               *
              from
               AccessHistories
              where
               Succeeded=1) as NonSusSuc
              on NonSusFls.DoorID=NonSusSuc.DoorID and DATEDIFF ( MINUTE , NonSusFls.Date , NonSusSuc.Date )<2) ");
                cmnd.Connection = conn;

                var rdr = cmnd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("AccessHistories, AccessHistoryID");
                }
                rdr.Close();
            }


        }

        public void LogAttempt(SecuritySystemsContext _SSC, int DoorID, int EmployeeID, DateTime Date, bool Succeeded, List<AccessMethod> Methods)
        {
            AccessHistory NewAccessHistRow = new AccessHistory();
            NewAccessHistRow.DoorID = DoorID;
            NewAccessHistRow.EmployeeID = EmployeeID;
            NewAccessHistRow.Date = Date;
            NewAccessHistRow.Succeeded = Succeeded;
            _SSC.AccessHistories.Add(NewAccessHistRow);
            _SSC.SaveChanges();
            var GetAccessHistoryID = (from History in AccessHistories
                                      where History.DoorID == DoorID && History.EmployeeID == EmployeeID && History.Date == Date
                                      select History).FirstOrDefault();
            foreach (var Method in Methods)
            {
                AccessHistoryAccessMethod c = new AccessHistoryAccessMethod();
                c.AccessHistoryID = GetAccessHistoryID.AccessHistoryID;
                c.AccessMethodID = Method.AccessMethodID;
                _SSC.AccessHistoryAccessMethods.Add(c);
                _SSC.SaveChanges();

            }





        }

    }
}
