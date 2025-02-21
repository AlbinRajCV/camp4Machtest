using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Test.Model
{
    public class FlightDetails
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; }
        public string DepAirportName { get; set; }
        public DateTime DepDate { get; set; }
        public TimeSpan DepTime { get; set; }
        public string ArrAirportName { get; set; }
        public DateTime ArrDate { get; set; }
        public TimeSpan ArrTime { get; set; }
    }
}
