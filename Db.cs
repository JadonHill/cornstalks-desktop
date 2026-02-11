using System.Data;
using System.Data.SqlClient;
using System;

namespace cornstalks_app
{
    /// <summary>
    /// Db = Database helper class.
    ///
    /// This class is the ONLY place that talks to SQL Server.
    /// All WinForms code calls stored procedures through here.
    /// </summary>
    public static class Db
    {
        // ------------------------------------------------------------
        // CONNECTION STRING
        // ------------------------------------------------------------
        // This tells C# how to connect to SQL Server.
        //
        // If this does NOT work later, we will only change this line.
        //
        // Common server values:
        //   localhost
        //   .
        //   .\\SQLEXPRESS
        //   (localdb)\\MSSQLLocalDB
        //
        public static string ConnectionString =
    Environment.GetEnvironmentVariable("CORNSTALKS_DEMO")
    ?? "Server=CHANGE_ME;Database=CHANGE_ME;Trusted_Connection=True;";



        /// <summary>
        /// Runs a stored procedure that RETURNS rows (SELECT).
        ///
        /// Example:
        ///   dbo.usp_Customers_GetActive
        ///   dbo.usp_Packages_GetAvailable
        ///
        /// Returns a DataTable (in-memory table).
        /// </summary>
        public static DataTable ExecDataTable(
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            // Create an empty in-memory table
            DataTable result = new DataTable();

            // SqlConnection = connection to SQL Server
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                // SqlCommand = what we want SQL Server to run
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                {
                    // VERY IMPORTANT:
                    // tells SQL Server we are calling a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters if we were given any
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    // Adapter executes the command and fills the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Runs a stored procedure that does NOT return rows.
        ///
        /// Used for:
        ///   INSERT
        ///   UPDATE
        ///   DELETE
        ///
        /// Example:
        ///   dbo.usp_Orders_CreateWithPackages
        /// </summary>
        public static void ExecNonQuery(
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                // Must open connection manually for ExecuteNonQuery
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
