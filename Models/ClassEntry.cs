public class ClassEntry
{
    public int Position { get; set; }
    public int PointsEvent { get; set; }
    public string ClassName { get; set; }
    public string Name { get; set; }

    public int Points => Constants.PointsMap.ContainsKey(Position) ? Constants.PointsMap[Position] : 1;
}