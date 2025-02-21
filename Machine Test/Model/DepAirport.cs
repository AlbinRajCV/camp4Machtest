using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Model
{
    public class DepAirport
    {
        public int DepAirportId { get; set; }
        public string DepAirportName { get; set; }
        public int FlightId { get; set; }
        public DateTime DepDate { get; set; }
        public TimeSpan DepTime { get; set; }
    }
}
