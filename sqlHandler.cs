using System;
using System.Data;
using System.Data.SqlServerCe;


namespace SqlCE2CSV
{
    class sqlHandler
    {
        SqlCeConnection mDBConnection = null;

        public static SqlCeDataReader ExecuteSQLQuery(string cmd, string ConnectionString)
        {
            SqlCeConnection mDBConnection = null;

            if (mDBConnection == null)
            {
                mDBConnection = new SqlCeConnection(ConnectionString);
            }

            if (mDBConnection.State == ConnectionState.Closed)
            {
                mDBConnection.Open();
            }

            SqlCeCommand cmd1 = new SqlCeCommand(cmd, mDBConnection);

            SqlCeDataReader Response = null;

            try
            {
                Response = cmd1.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Response;
        }
    }
}
