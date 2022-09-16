using System;
using System.Collections.Generic;
using System.IO;
using ConsoleTables;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Start");

        var files = Directory.GetFiles("results");

        var eventResults = new List<EventResults>();

        foreach (var file in files)
        {
            Console.WriteLine($"Reading File {file}");

            var html = File.ReadAllText(file);

            var resultsParser = new ResultsParser(html);

            eventResults.Add(resultsParser.GenerateResults());
        }

        var pointsGenerator = new PointsGenerator(eventResults);

        var seasonPoints = pointsGenerator.GenerateSeasonPoints();

        var tsv = $"Name\tNumber Of Events\tTotal Points\tPoints With Drops\tPoints 1\tPoints 2\tPoints 3\tPoints 4\tPoints 5\tPoints 6\tPoints 7\tPoints 8\tPoints 9\r\n";

        foreach (var classResults in seasonPoints)
        {
            Console.WriteLine(classResults.ClassName);

            ConsoleTable.From<PersonAggregateData>(classResults.Entries).Write(Format.Alternative);

            tsv += $"{classResults.ClassName}\r\n";

            foreach (var entry in classResults.Entries)
            {
                tsv += $"{entry.Name}\t{entry.NumberOfEvents}\t{entry.TotalPoints}\t{entry.PointsWithDrops}\t{entry.Points1}\t{entry.Points2}\t{entry.Points3}\t{entry.Points4}\t{entry.Points5}\t{entry.Points6}\t{entry.Points7}\t{entry.Points8}\t{entry.Points9}\r\n";
            }
        }

        File.WriteAllText("output/seasonpoints.tsv", tsv);

        Console.WriteLine("End");
    }
}