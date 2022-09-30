using System;
using System.Collections.Generic;
using System.Linq;
using PointsCalculator.Models.PaxResults;

namespace PointsCalculator
{
    public class PaxPointsGenerator
    {
        private readonly List<EventResults> _results;

        public PaxPointsGenerator(List<EventResults> results)
        {
            _results = results;
        }

        public List<PersonAggregateData> GeneratePaxPoints()
        {
            foreach (var ev in _results)
            {
                var ranking = ev.Results.OrderBy(x => x.Position).ToList();

                var topPax = ranking[0];

                foreach (var fin in ranking)
                {
                    fin.PaxPoints = (topPax.PaxTime / fin.PaxTime) * 100.0;
                }
            }

            var allPersonResults = _results.SelectMany(x => x.Results);

            var groupedByPerson = allPersonResults.GroupBy(x => x.Name);

            var seasonPoints = new List<PersonAggregateData>();

            foreach (var p in groupedByPerson)
            {
                var personAggregateData = new PersonAggregateData(p.Key, p.ToList());

                seasonPoints.Add(personAggregateData);
            }

            return seasonPoints.OrderByDescending(x => x.PointsWithDrops).ToList();
        }
    }
}