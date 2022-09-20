using System.Collections.Generic;

public static class Constants
{
    // Points structure in the format of <finishing position, points payout>
    public static readonly Dictionary<int, int> PointsMap = new Dictionary<int, int>
    {
        { 1, 9 },
        { 2, 6 },
        { 3, 4 },
        { 4, 3 },
        { 5, 2 },
        { 6, 1 }
    };


    public const int NumberOfCountedEvents = 8;
    public const int MinimumEventsForTrophy = 5;
}