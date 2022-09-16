using System.Collections.Generic;
using System.Linq;

public class PersonAggregateData
{
    private List<ClassEntry> _entries { get; set; }
    private string _name;
    public string Name => _name;
    public int NumberOfEvents => _entries.Count;

    public PersonAggregateData(string name, List<ClassEntry> entries)
    {
        _name = name;
        _entries = entries;
    }

    public int TotalPoints => _entries.OrderByDescending(x => x.Points).Sum(x => x.Points);
    public int PointsWithDrops => _entries.OrderByDescending(x => x.Points).Take(Constants.NumberOfCountedEvents).Sum(x => x.Points);

    private Dictionary<int, ClassEntry> EntriesByEvent => _entries.ToDictionary(k => k.PointsEvent);

    public int? Points1 => EntriesByEvent.ContainsKey(1) ? EntriesByEvent[1].Points : null;
    public int? Points2 => EntriesByEvent.ContainsKey(2) ? EntriesByEvent[2].Points : null;
    public int? Points3 => EntriesByEvent.ContainsKey(3) ? EntriesByEvent[3].Points : null;
    public int? Points4 => EntriesByEvent.ContainsKey(4) ? EntriesByEvent[4].Points : null;
    public int? Points5 => EntriesByEvent.ContainsKey(5) ? EntriesByEvent[5].Points : null;
    public int? Points6 => EntriesByEvent.ContainsKey(6) ? EntriesByEvent[6].Points : null;
    public int? Points7 => EntriesByEvent.ContainsKey(7) ? EntriesByEvent[7].Points : null;
    public int? Points8 => EntriesByEvent.ContainsKey(8) ? EntriesByEvent[8].Points : null;
    public int? Points9 => EntriesByEvent.ContainsKey(9) ? EntriesByEvent[9].Points : null;
}