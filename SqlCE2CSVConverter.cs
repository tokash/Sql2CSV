using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.IO;

namespace SqlCE2CSV
{
    public class SqlCE2CSVConverter
    {
        public static void ConvertDBTableToCSV(string iQuery, string[] iColumnNames, string iConnectionString, string iFilename, bool iCreateDirectory, string iDirectoryName)
        {
            string fullPath = string.Empty;
            try
            {
                if (iCreateDirectory)
                {
                    Directory.CreateDirectory(iDirectoryName);
                }

                SqlCeDataReader Response = sqlHandler.ExecuteSQLQuery(iQuery, iConnectionString);

                fullPath = Path.Combine(iDirectoryName, SanitizeString(iFilename, new string[] { ",", "\n", "\r" }));

                //Writing Column names
                if (iColumnNames != null && iColumnNames.Length > 0)
                {
                    WriteLineToCSV(fullPath, iColumnNames); 
                }
                while (Response.Read())
                {
                    Object[] Data = new Object[Response.FieldCount];
                    Response.GetValues(Data);

                    if (Data != null)
                    {
                        WriteLineToCSV(fullPath, Data);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static string SanitizeString(string iStringToSanitize, string[] iCharsToRemove)
        {
            string newString = iStringToSanitize;


            foreach (string charToRemove in iCharsToRemove)
            {
                newString = newString.Replace(charToRemove, string.Empty);
            }

            return newString;
        }

        public static void WriteLineToCSV(string iFilename, params object[] iData)
        {
            string csvLine = string.Empty;
            string currData = string.Empty;
                        
            try
            {
                //Create the csv ilne from the data
                int i = 0;
                while (i < iData.Length - 1)
                {
                    currData = string.Empty;
                    
                    currData = SanitizeString(iData[i].ToString(), new string[]{",", "\n", "\r"}) + ",";
                    csvLine += currData;
                    i++;
                }

                currData = string.Empty;
                currData = SanitizeString(iData[i].ToString(), new string[] { ",", "\n", "\r" }) + ",";
                
                csvLine += currData;

                //Username	Followers	Following	Favorites	Tweets	Retweets
                using (StreamWriter file = new StreamWriter(iFilename, true))
                {
                    file.WriteLine(csvLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //public static void WriteTweetsToCSV(string iFilepath, List<TweetRow> iData)
        //{ 
            
        //    try
        //    {
        //        foreach (TweetRow tweetRow in iData)
        //        {
        //            using (StreamWriter file = new StreamWriter(iFilepath, true))
        //            {
        //                string line = string.Format("{0},{1},{2},{3}", tweetRow.TweetID, tweetRow.UserID, tweetRow.Tweet, tweetRow.TimeOfTweet);
        //                file.WriteLine(line);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
            
        //}

        //public static void WriteCompanyToCSV(string iFilepath, List<UserRow> iData)
        //{

        //    try
        //    {
        //        //Username	Followers	Following	Favorites	Tweets	Retweets
        //        using (StreamWriter file = new StreamWriter(iFilepath, true))
        //        {
        //            file.WriteLine("Username,Followers,Following,Favorites,Tweets,Retweets");
        //            foreach (UserRow userRow in iData)
        //            {

        //                string line = string.Format("{0},{1},{2},{3},{4},{5}",
        //                                            userRow.Name.Replace(",", ""),
        //                                            userRow.Followers_Count,
        //                                            userRow.Friends_Count,
        //                                            userRow.Favourites_Count,
        //                                            userRow.Tweet_Count,
        //                                            userRow.Tweet_Count);
        //                file.WriteLine(line);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }

        //}

        //public static List<User> GetUsersFromTable(string query, string connectionString)
        //{ 
        //    SqlCeDataReader Response = sqlHandler.ExecuteSQLQuery(query, connectionString); //Get Users from sql table

        //        List<User> Users = new List<User>();

        //        while (Response.Read())
        //        {
        //            Object[] NUser = new Object[Response.FieldCount];
        //            Response.GetValues(NUser);

        //            if (NUser.Length == 3)
        //            {
        //                User user = new User() { Id = NUser[0].ToString(), Name = NUser[1].ToString(), Follows = NUser[2].ToString() };
        //                Users.Add(user);
        //            }
        //            else
        //            {
        //                User user = new User() { Id = NUser[0].ToString(), Name = NUser[1].ToString() };
        //                Users.Add(user);
        //            }

        //        }
        //        return Users;
        //}

        //public static List<TweetRow> GetTweets(string query, string connectionString, int iNumRows)
        //{
        //    SqlCeDataReader Response = sqlHandler.ExecuteSQLQuery(query, connectionString); //Get Users from sql table

        //    List<TweetRow> records = new List<TweetRow>();

        //    while (Response.Read())
        //    {
        //        Object[] record = new Object[Response.FieldCount];
        //        Response.GetValues(record);

        //        if (record.Length == iNumRows)
        //        {
        //            TweetRow tweet = new TweetRow()
        //                                    {
        //                                        UserID = record[0].ToString(),
        //                                        TweetID = record[1].ToString(),
        //                                        Tweet = record[2].ToString(),
        //                                        TimeOfTweet = record[3].ToString()
        //                                    };
        //            records.Add(tweet); 
        //        }
        //    }

        //    return records;
        //}

        //public static List<UserRow> GetUsers(string query, string connectionString, int iNumRows)
        //{
        //    SqlCeDataReader Response = sqlHandler.ExecuteSQLQuery(query, connectionString);

        //    List<UserRow> records = new List<UserRow>();

        //    while (Response.Read())
        //    {
        //        Object[] record = new Object[Response.FieldCount];
        //        Response.GetValues(record);

        //        if (record.Length == iNumRows)
        //        {
        //            UserRow user = new UserRow()
        //            {
        //                UserID = record[0].ToString(),
        //                Name = record[1].ToString(),
        //                Follows = record[2].ToString(),
        //                Description = record[3].ToString(),
        //                Screen_Name = record[4].ToString(),
        //                Followers_Count = record[5].ToString(),
        //                Friends_Count = record[6].ToString(),
        //                Favourites_Count = record[7].ToString(),
        //                Tweet_Count = record[8].ToString(),
        //                Location = record[9].ToString(),
        //                Twitter_Name = record[10].ToString(),
        //                Time_Zone = record[11].ToString(),
        //                Created_At = record[12].ToString(),
        //                Time_Stamp = record[13].ToString()
        //            };
        //            records.Add(user);
        //        }
        //    }

        //    return records;
        //}
    }
}
