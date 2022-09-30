using System.Collections.Generic;
using System.Linq;
using PointsCalculator.Models.ClassResults;

namespace PointsCalculator
{
    public class ClassPointsGenerator
    {
        private readonly List<EventResults> _results;

        public ClassPointsGenerator(List<EventResults> results)
        {
            _results = results;
        }

        public List<SeasonPoints> GenerateSeasonPoints()
        {
            // Flatten the event results
            var allClassResults = _results.SelectMany(x => x.Results);

            // Group them by class name
            var groupedClassResults = allClassResults.GroupBy(x => x.Name);

            var seasonPoints = new List<SeasonPoints>();

            foreach (var c in groupedClassResults)
            {
                // Flatten the entries across the season
                var seasonEntries = c.SelectMany(x => x.Entries);

                // Group entries by person
                var personResults = seasonEntries.GroupBy(x => x.Name);

                var aggregatedPersonData = personResults
                    .Select(x => new PersonAggregateData(x.Key, x.ToList()))
                    .Where(x => x.NumberOfEvents >= Constants.MinimumEventsForTrophy).OrderByDescending(x => x.PointsWithDrops).ToList();

                if (aggregatedPersonData.Any())
                {
                    seasonPoints.Add(new SeasonPoints
                    {
                        ClassName = c.Key.ToUpper(),
                        Entries = aggregatedPersonData
                    });
                }
            }

            return seasonPoints;
        }

    }
}