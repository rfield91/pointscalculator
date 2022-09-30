using System.Collections.Generic;

namespace PointsCalculator.Models.ClassResults
{
    public class EventResults
    {
        public int EventNumber { get; set; }
        public List<ClassResults> Results { get; set; }
    }
}