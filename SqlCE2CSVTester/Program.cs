using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlCE2CSV;

namespace SqlCE2CSVTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = string.Format("DataSource=\"{0}\"", "TwitterData_21.01.2014.10.41.55.6465.sdf");

            string[] rcUsers = File.ReadAllLines("compList.txt");
            string[] companyIDs = { "64784531", "297676668", "183033795", "37687077" };
            int i = 0;
            foreach (string name in rcUsers)
            {
                string companyID = companyIDs[i];
                string query = string.Empty;
                i++;

                string filename = string.Format("{0}_{1}.csv", name, DateTime.Now.ToString("dd.MM.yyyy.HH.mm.ss.ffff"));
                //first write the company details
                query = String.Format("Select * from Users where UserID= \'{0}\'", companyID);
                SqlCE2CSVConverter.ConvertDBTableToCSV(query, new string[]{"UserID",
                                                                        "Name",
                                                                        "Follows", 
                                                                        "Description",
                                                                        "Screen_Name",
                                                                        "Followers_Count",
                                                                        "Friends_Count",
                                                                        "Favourites_Count",
                                                                        "Total_Tweet_Count",
                                                                        "Weekly_Tweet_Count",
                                                                        "Weekly_Retweet_Count",
                                                                        "Location",
                                                                        "Twitter_Name",
                                                                        "Time_Zone",
                                                                        "Created_At",
                                                                        "Time_Stamp"}, connectionString, filename, true,
                                                                        "TwitterData_21.01.2014.10.41.55.6465");
                //then write the company followers details
                query = String.Format("Select * from Followers where follows = \'{0}\'", companyID);
                SqlCE2CSVConverter.ConvertDBTableToCSV(query, new string[]{"UserID",
                                                                        "Name",
                                                                        "Follows", 
                                                                        "Description",
                                                                        "Screen_Name",
                                                                        "Followers_Count",
                                                                        "Friends_Count",
                                                                        "Favourites_Count",
                                                                        "Total_Tweet_Count",
                                                                        "Weekly_Tweet_Count",
                                                                        "Weekly_Retweet_Count",
                                                                        "Location",
                                                                        "Twitter_Name",
                                                                        "Time_Zone",
                                                                        "Created_At",
                                                                        "Time_Stamp"}, connectionString, filename, true,
                                                                        "TwitterData_21.01.2014.10.41.55.6465");
            }
        }
    }
}
