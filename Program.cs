using System;
using System.Collections.Generic;
using System.IO;
using ConsoleTables;
using PointsCalculator.Models.ClassResults;

namespace PointsCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start");

            HandleClassResults();

            HandlePaxResults();

            Console.WriteLine("End");
        }

        private static void HandleClassResults()
        {
            Console.WriteLine("Event Results");

            var files = Directory.GetFiles(@"results\2022\Class");

            var eventResults = new List<Models.ClassResults.EventResults>();

            foreach (var file in files)
            {
                Console.WriteLine($"Reading File {file}");

                var html = File.ReadAllText(file);

                var resultsParser = new ClassResultsParser(html);

                eventResults.Add(resultsParser.GenerateResults());
            }

            var pointsGenerator = new ClassPointsGenerator(eventResults);

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

            File.WriteAllText("output/2022/ClassPoints.tsv", tsv);

            Console.WriteLine("Done Event Results");
        }

        private static void HandlePaxResults()
        {
            Console.WriteLine("Pax Results");

            var files = Directory.GetFiles(@"results\2022\Pax");

            var eventResults = new List<Models.PaxResults.EventResults>();

            foreach (var file in files)
            {
                Console.WriteLine($"Reading File {file}");

                var html = File.ReadAllText(file);

                var resultsParser = new PaxResultsParser(html);

                eventResults.Add(resultsParser.GenerateResults());
            }

            var pointsGenerator = new PaxPointsGenerator(eventResults);

            var seasonPoints = pointsGenerator.GeneratePaxPoints();

            ConsoleTable.From<Models.PaxResults.PersonAggregateData>(seasonPoints).Write(Format.Alternative);

            var tsv = $"Position\tName\tNumber Of Events\tTotal Points\tPoints With Drops\tPoints 1\tPoints 2\tPoints 3\tPoints 4\tPoints 5\tPoints 6\tPoints 7\tPoints 8\tPoints 9\r\n";

            for (var i = 0; i < seasonPoints.Count; i++)
            {
                var entry = seasonPoints[i];
                var position = i + 1;

                tsv += $"{position}\t{entry.Name}\t{entry.NumberOfEvents}\t{entry.TotalPoints}\t{entry.PointsWithDrops}\t{entry.Points1}\t{entry.Points2}\t{entry.Points3}\t{entry.Points4}\t{entry.Points5}\t{entry.Points6}\t{entry.Points7}\t{entry.Points8}\t{entry.Points9}\r\n";
            }

            File.WriteAllText("output/2022/PaxPoints.tsv", tsv);

            Console.WriteLine("Done Pax Results");
        }
    }
}