using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine_Test.Model;
using Machine_Test.Repository;

namespace Machine_Test.Service
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginServiceImpl()
        {
            _loginRepository = new LoginRepoImpl();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            return await _loginRepository.AuthenticateUserAsync(username, password);
        }
        public async Task<List<FlightDetails>> GetFlightDetailsAsync()
        {
            return await _loginRepository.GetFlightDetailsAsync();
        }
        public async Task<FlightDetails> SearchFlightByIdAsync(int flightId)
        {
            return await _loginRepository.SearchFlightByIdAsync(flightId);
        }
        public async Task AddFlightDetailsAsync(FlightDetails flightDetails)
        {
            await _loginRepository.AddFlightDetailsAsync(flightDetails);
        }
        public async Task EditFlightDetailsAsync(FlightDetails flightDetails)
        {
            await _loginRepository.EditFlightDetailsAsync(flightDetails);
        }
        public async Task DeleteFlightDetailsAsync(int flightId)
        {
            await _loginRepository.DeleteFlightDetailsAsync(flightId);
        }
    }
}
