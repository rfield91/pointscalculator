using System;
using System.Collections.Generic;
using System.Linq;

namespace PointsCalculator.Models.PaxResults
{
    public class PersonAggregateData
    {
        private List<EventEntry> _entries;

        private string _name;

        public string Name => _name;

        public int NumberOfEvents => _entries.Count;

        public PersonAggregateData(string name, List<EventEntry> entries)
        {
            _name = name;
            _entries = entries;
        }

        public double TotalPoints => Math.Round(_entries.OrderByDescending(x => x.PaxPoints).Sum(x => x.PaxPoints), 3);

        public double PointsWithDrops => Math.Round(_entries.OrderByDescending(x => x.PaxPoints).Take(5).Sum(x => x.PaxPoints), 3);

        private Dictionary<int, EventEntry> EntriesByEvent => _entries.ToDictionary(k => k.EventNumber);

        public double? Points1 => EntriesByEvent.ContainsKey(1) ? Math.Round(EntriesByEvent[1].PaxPoints, 3) : null;
        public double? Points2 => EntriesByEvent.ContainsKey(2) ? Math.Round(EntriesByEvent[2].PaxPoints, 3) : null;

        public double? Points3 => EntriesByEvent.ContainsKey(3) ? Math.Round(EntriesByEvent[3].PaxPoints, 3) : null;
        public double? Points4 => EntriesByEvent.ContainsKey(4) ? Math.Round(EntriesByEvent[4].PaxPoints, 3) : null;
        public double? Points5 => EntriesByEvent.ContainsKey(5) ? Math.Round(EntriesByEvent[5].PaxPoints, 3) : null;
        public double? Points6 => EntriesByEvent.ContainsKey(6) ? Math.Round(EntriesByEvent[6].PaxPoints, 3) : null;
        public double? Points7 => EntriesByEvent.ContainsKey(7) ? Math.Round(EntriesByEvent[7].PaxPoints, 3) : null;
        public double? Points8 => EntriesByEvent.ContainsKey(8) ? Math.Round(EntriesByEvent[8].PaxPoints, 3) : null;
        public double? Points9 => EntriesByEvent.ContainsKey(9) ? Math.Round(EntriesByEvent[9].PaxPoints, 3) : null;

    }
}