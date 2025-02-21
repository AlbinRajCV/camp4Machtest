using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine_Test.Model;

namespace Machine_Test.Repository
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateUserAsync(string username, string password);
        Task<List<FlightDetails>> GetFlightDetailsAsync();
        Task<FlightDetails> SearchFlightByIdAsync(int flightId);
        Task AddFlightDetailsAsync(FlightDetails flightDetails);
        Task EditFlightDetailsAsync(FlightDetails flightDetails);

        Task DeleteFlightDetailsAsync(int flightId);



    }
}