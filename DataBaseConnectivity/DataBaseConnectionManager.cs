using Microsoft.Data.SqlClient;

namespace DataBaseConnectivity
{
    public class DataBaseConnectionManager
    {
        // Each project is having App.config --- fetch connection string from there
        // Establish the connection and return
        // 
        // sqlConnection
        public static SqlConnection OpenConnection(string _connString)
        {

            // field 
            SqlConnection connection = null;
            try
            {
                //Step 1: Configure Connection String
                if (_connString != null || Convert.ToString(connection.State) == "Closed")
                {
                    //Open Connection
                    connection = new SqlConnection(_connString);
                    connection.Open();
                }

                return connection;

            }
            catch (SqlException ex)
            {
                Console.WriteLine("oops SQL server error ocuured");
                Console.WriteLine(ex.Message);
                return null;




            }
            catch (Exception ex)
            {
                Console.WriteLine("OOPs, something went wrong. \n" + ex);
                Console.WriteLine(ex.Message);
                return null;
            }
            // Open Connection


        }
    }
}
