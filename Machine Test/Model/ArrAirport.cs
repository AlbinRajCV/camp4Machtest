using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Model
{
    public class ArrAirport
    {
        public int ArrAirportId { get; set; }
        public string ArrAirportName { get; set; }
        public int FlightId { get; set; }
        public DateTime ArrDate { get; set; }
        public TimeSpan ArrTime { get; set; }
    }
}
