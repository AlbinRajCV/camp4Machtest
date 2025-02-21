using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Machine_Test.Model;

namespace Machine_Test.Repository
{
    public class LoginRepoImpl : ILoginRepository
    {
        string winConnString = ConfigurationManager.ConnectionStrings["Project"].ConnectionString;

        public async Task<bool> AuthenticateUserAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username and password cannot be null or empty.");
            }

            bool isAuthenticated = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("sp_AdminLogin", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Password", password);

                        int result = (int)await command.ExecuteScalarAsync();
                        isAuthenticated = result == 1;
                    }
                }

                if (isAuthenticated)
                {
                    Console.WriteLine($"Login successful for user: {userName}");
                }
                else
                {
                    Console.WriteLine($"Login failed for user: {userName}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
            return isAuthenticated;
        }
        public async Task<List<FlightDetails>> GetFlightDetailsAsync()
        {
            List<FlightDetails> flightDetailsList = new List<FlightDetails>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("GetFlightDetails", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                FlightDetails flightDetails = new FlightDetails
                                {
                                    FlightId = reader.GetInt32(0),
                                    FlightName = reader.GetString(1),
                                    DepAirportName = reader.GetString(2),
                                    DepDate = reader.GetDateTime(3),
                                    DepTime = reader.GetTimeSpan(4),
                                    ArrAirportName = reader.GetString(5),
                                    ArrDate = reader.GetDateTime(6),
                                    ArrTime = reader.GetTimeSpan(7)
                                };
                                flightDetailsList.Add(flightDetails);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

            return flightDetailsList;
        }
        public async Task<FlightDetails> SearchFlightByIdAsync(int flightId)
        {
            FlightDetails flightDetails = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("GetFlightDetailsById", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FlightId", flightId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                flightDetails = new FlightDetails
                                {
                                    FlightId = reader.GetInt32(0),
                                    FlightName = reader.GetString(1),
                                    DepAirportName = reader.GetString(2),
                                    DepDate = reader.GetDateTime(3),
                                    DepTime = reader.GetTimeSpan(4),
                                    ArrAirportName = reader.GetString(5),
                                    ArrDate = reader.GetDateTime(6),
                                    ArrTime = reader.GetTimeSpan(7)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

            return flightDetails;
        }
        public async Task AddFlightDetailsAsync(FlightDetails flightDetails)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("AddFlightDetails", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FlightName", flightDetails.FlightName);
                        command.Parameters.AddWithValue("@DepAirportName", flightDetails.DepAirportName);
                        command.Parameters.AddWithValue("@DepDate", flightDetails.DepDate);
                        command.Parameters.AddWithValue("@DepTime", flightDetails.DepTime);
                        command.Parameters.AddWithValue("@ArrAirportName", flightDetails.ArrAirportName);
                        command.Parameters.AddWithValue("@ArrDate", flightDetails.ArrDate);
                        command.Parameters.AddWithValue("@ArrTime", flightDetails.ArrTime);

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Flight details added successfully.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
        public async Task EditFlightDetailsAsync(FlightDetails flightDetails)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("EditFlightDetails", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FlightId", flightDetails.FlightId);
                        command.Parameters.AddWithValue("@FlightName", flightDetails.FlightName);
                        command.Parameters.AddWithValue("@DepAirportName", flightDetails.DepAirportName);
                        command.Parameters.AddWithValue("@DepDate", flightDetails.DepDate);
                        command.Parameters.AddWithValue("@DepTime", flightDetails.DepTime);
                        command.Parameters.AddWithValue("@ArrAirportName", flightDetails.ArrAirportName);
                        command.Parameters.AddWithValue("@ArrDate", flightDetails.ArrDate);
                        command.Parameters.AddWithValue("@ArrTime", flightDetails.ArrTime);

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Flight details updated successfully.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
        public async Task DeleteFlightDetailsAsync(int flightId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Project"].ConnectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand("DeleteFlightDetails", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FlightId", flightId);

                        await command.ExecuteNonQueryAsync();
                        Console.WriteLine("Flight details deleted successfully.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A SqlException error occurred: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }
}