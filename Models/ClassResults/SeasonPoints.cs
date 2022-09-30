using System.Collections.Generic;

namespace PointsCalculator.Models.ClassResults
{
    public class SeasonPoints
    {
        public string ClassName { get; set; }
        public List<PersonAggregateData> Entries { get; set; }
    }
}