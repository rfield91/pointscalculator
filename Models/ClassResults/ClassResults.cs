using System.Collections.Generic;

namespace PointsCalculator.Models.ClassResults
{
    public class ClassResults
    {
        public string Name { get; set; }

        public List<ClassEntry> Entries { get; set; }
    }
}