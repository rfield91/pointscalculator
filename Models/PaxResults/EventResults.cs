using System.Collections.Generic;

namespace PointsCalculator.Models.PaxResults
{
    public class EventResults
    {
        public int EventNumber { get; set; }
        public List<EventEntry> Results { get; set; }
    }
}