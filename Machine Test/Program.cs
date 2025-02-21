using Machine_Test.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Machine_Test.Model;

namespace Machine_Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to FlyWithMe Admin Portal");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            ILoginService loginService = new LoginServiceImpl();
            bool isAuthenticated = await loginService.LoginAsync(username, password);

            if (isAuthenticated)
            {
                Console.WriteLine("Login successful!");
                ShowMenu();
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        private static void ShowMenu()
        {
            while (true)
            {
                Console.Clear(); // Clear the console to simulate a new page
                Console.WriteLine("Menu:");
                Console.WriteLine("1. List All Flight Details");
                Console.WriteLine("2. Search Flight by Id");
                Console.WriteLine("3. Add a Flight Details");
                Console.WriteLine("4. Edit a Flight Details");
                Console.WriteLine("5. Delete a Flight Details");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllFlightDetails().Wait();
                        break;
                    case "2":
                        SearchFlightById().Wait();
                        break;
                    case "3":
                        AddFlightDetails().Wait();
                        break;
                    case "4":
                        EditFlightDetails().Wait();
                        break;
                    case "5":
                        DeleteFlightDetails().Wait();
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }

        private static async Task ListAllFlightDetails()
        {
            ILoginService loginService = new LoginServiceImpl();
            List<FlightDetails> flightDetailsList = await loginService.GetFlightDetailsAsync();

            Console.WriteLine("\nFlight Details:");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("| FlightId | FlightName | DepAirport | DepDate     |  DepTime  | ArrAirport |  ArrDate | ArrTime |");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            foreach (var flight in flightDetailsList)
            {
                Console.WriteLine($"| {flight.FlightId,8} | {flight.FlightName,-10} | {flight.DepAirportName,-10} | {flight.DepDate.ToShortDateString(),-10} | {flight.DepTime,-8} | {flight.ArrAirportName,-10} | {flight.ArrDate.ToShortDateString(),-10} | {flight.ArrTime,-8} |");
            }

            Console.WriteLine("--------------------------------------------------------------------------------------------------");
        }
        private static async Task SearchFlightById()
        {
            Console.Write("Enter FlightId: ");
            int flightId = int.Parse(Console.ReadLine());

            ILoginService loginService = new LoginServiceImpl();
            FlightDetails flightDetails = await loginService.SearchFlightByIdAsync(flightId);

            if (flightDetails != null)
            {
                Console.WriteLine("\nFlight Details:");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("| FlightId | FlightName |");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"| {flightDetails.FlightId,8} | {flightDetails.FlightName,-10} |");
                Console.WriteLine("-----------------------------------");
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
        private static async Task AddFlightDetails()
        {
            Console.Write("Enter Flight Name: ");
            string flightName = Console.ReadLine();

            Console.Write("Enter Departure Airport Name: ");
            string depAirportName = Console.ReadLine();

            Console.Write("Enter Departure Date (yyyy-mm-dd): ");
            DateTime depDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Departure Time (HH:mm:ss): ");
            TimeSpan depTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Arrival Airport Name: ");
            string arrAirportName = Console.ReadLine();

            Console.Write("Enter Arrival Date (yyyy-mm-dd): ");
            DateTime arrDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Arrival Time (HH:mm:ss): ");
            TimeSpan arrTime = TimeSpan.Parse(Console.ReadLine());

            FlightDetails flightDetails = new FlightDetails
            {
                FlightName = flightName,
                DepAirportName = depAirportName,
                DepDate = depDate,
                DepTime = depTime,
                ArrAirportName = arrAirportName,
                ArrDate = arrDate,
                ArrTime = arrTime
            };

            ILoginService loginService = new LoginServiceImpl();
            await loginService.AddFlightDetailsAsync(flightDetails);
        }
        private static async Task EditFlightDetails()
        {
            Console.Write("Enter FlightId: ");
            int flightId = int.Parse(Console.ReadLine());

            ILoginService loginService = new LoginServiceImpl();
            FlightDetails existingFlightDetails = await loginService.SearchFlightByIdAsync(flightId);

            if (existingFlightDetails != null)
            {
                // Display current flight details in tabular form
                Console.WriteLine("\nCurrent Flight Details:");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("| FlightId | FlightName | DepAirport | DepDate     |  DepTime  | ArrAirport |  ArrDate | ArrTime |");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| {existingFlightDetails.FlightId,8} | {existingFlightDetails.FlightName,-10} | {existingFlightDetails.DepAirportName,-10} | {existingFlightDetails.DepDate.ToShortDateString(),-10} | {existingFlightDetails.DepTime,-8} | {existingFlightDetails.ArrAirportName,-10} | {existingFlightDetails.ArrDate.ToShortDateString(),-10} | {existingFlightDetails.ArrTime,-8} |");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");

                // Prompt user for new values
                Console.WriteLine("\nEnter new details (leave blank to keep current values):");

                Console.Write("Enter new Departure Airport Name: ");
                string depAirportName = Console.ReadLine();
                if (string.IsNullOrEmpty(depAirportName))
                {
                    depAirportName = existingFlightDetails.DepAirportName;
                }

                Console.Write("Enter new Departure Date (yyyy-mm-dd): ");
                string depDateInput = Console.ReadLine();
                DateTime depDate = string.IsNullOrEmpty(depDateInput) ? existingFlightDetails.DepDate : DateTime.Parse(depDateInput);

                Console.Write("Enter new Departure Time (HH:mm:ss): ");
                string depTimeInput = Console.ReadLine();
                TimeSpan depTime = string.IsNullOrEmpty(depTimeInput) ? existingFlightDetails.DepTime : TimeSpan.Parse(depTimeInput);

                Console.Write("Enter new Arrival Airport Name: ");
                string arrAirportName = Console.ReadLine();
                if (string.IsNullOrEmpty(arrAirportName))
                {
                    arrAirportName = existingFlightDetails.ArrAirportName;
                }

                Console.Write("Enter new Arrival Date (yyyy-mm-dd): ");
                string arrDateInput = Console.ReadLine();
                DateTime arrDate = string.IsNullOrEmpty(arrDateInput) ? existingFlightDetails.ArrDate : DateTime.Parse(arrDateInput);

                Console.Write("Enter new Arrival Time (HH:mm:ss): ");
                string arrTimeInput = Console.ReadLine();
                TimeSpan arrTime = string.IsNullOrEmpty(arrTimeInput) ? existingFlightDetails.ArrTime : TimeSpan.Parse(arrTimeInput);

                FlightDetails updatedFlightDetails = new FlightDetails
                {
                    FlightId = flightId,
                    FlightName = existingFlightDetails.FlightName, // Keep the current FlightName
                    DepAirportName = depAirportName,
                    DepDate = depDate,
                    DepTime = depTime,
                    ArrAirportName = arrAirportName,
                    ArrDate = arrDate,
                    ArrTime = arrTime
                };

                await loginService.EditFlightDetailsAsync(updatedFlightDetails);
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
        private static async Task DeleteFlightDetails()
        {
            Console.Write("Enter FlightId: ");
            int flightId = int.Parse(Console.ReadLine());

            ILoginService loginService = new LoginServiceImpl();
            FlightDetails existingFlightDetails = await loginService.SearchFlightByIdAsync(flightId);

            if (existingFlightDetails != null)
            {
                // Display current flight details in tabular form
                Console.WriteLine("\nCurrent Flight Details:");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("| FlightId | FlightName | DepAirport | DepDate     |  DepTime  | ArrAirport |  ArrDate | ArrTime |");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine($"| {existingFlightDetails.FlightId,8} | {existingFlightDetails.FlightName,-10} | {existingFlightDetails.DepAirportName,-10} | {existingFlightDetails.DepDate.ToShortDateString(),-10} | {existingFlightDetails.DepTime,-8} | {existingFlightDetails.ArrAirportName,-10} | {existingFlightDetails.ArrDate.ToShortDateString(),-10} | {existingFlightDetails.ArrTime,-8} |");
                Console.WriteLine("--------------------------------------------------------------------------------------------------");

                Console.Write("Are you sure you want to delete this flight? (y/n): ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "y")
                {
                    await loginService.DeleteFlightDetailsAsync(flightId);
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Flight not found.");
            }
        }
    }
}