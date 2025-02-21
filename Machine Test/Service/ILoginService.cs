using System.Threading.Tasks;
using Machine_Test.Model;

namespace Machine_Test.Service
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<List<FlightDetails>> GetFlightDetailsAsync();
        Task<FlightDetails> SearchFlightByIdAsync(int flightId);
        Task AddFlightDetailsAsync(FlightDetails flightDetails);

        Task EditFlightDetailsAsync(FlightDetails flightDetails);
        Task DeleteFlightDetailsAsync(int flightId);


    }
}