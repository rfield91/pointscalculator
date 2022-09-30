using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PointsCalculator.Models.ClassResults;

namespace PointsCalculator
{
    public class ClassResultsParser
    {
        private readonly string _html;

        public ClassResultsParser(string html)
        {
            _html = html;
        }

        public EventResults GenerateResults()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(_html);

            // Pluck out the event number; this parsing is awful because axware is awful
            var eventNumberElement = htmlDocument.DocumentNode.SelectNodes("//table/tbody").First().ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList()[1];

            var pointsEventNumber = int.Parse(eventNumberElement.OuterHtml[eventNumberElement.OuterHtml.IndexOf('#') + 1].ToString());

            var eventResults = new EventResults
            {
                EventNumber = pointsEventNumber,
                Results = new List<ClassResults>()
            };

            // Results table we care about happens to be the last table on the page
            var resultsTable = htmlDocument.DocumentNode.SelectNodes("//table/tbody").Last(); // this is the results table

            var rows = resultsTable.ChildNodes;

            ClassResults classResult = new ClassResults();

            foreach (var row in rows)
            {
                if (row.NodeType == HtmlNodeType.Element)
                {
                    // Determine if the current row is a class header row
                    var isHeaderRow = row.ChildNodes.FirstOrDefault(x => x.Name.ToLower().Contains("th")) != null;

                    // Class header row
                    if (isHeaderRow)
                    {
                        // Get the class name
                        var a = row.ChildNodes.First(x => x.Name.ToLower().Contains("th")).Element("a");

                        classResult = new ClassResults
                        {
                            Name = a.GetAttributeValue("name", string.Empty),
                            Entries = new List<ClassEntry>()
                        };

                        eventResults.Results.Add(classResult);
                    }
                    // Results
                    else
                    {
                        var position = int.Parse(Regex.Replace(row.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList()[0].InnerHtml.ToString(), "[^0-9]", ""));
                        var carClass = row.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList()[1].InnerHtml;
                        var name = row.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList()[3].InnerHtml;

                        classResult.Entries.Add(new ClassEntry
                        {
                            Position = position,
                            PointsEvent = pointsEventNumber,
                            ClassName = carClass,
                            Name = name
                        });
                    }
                }
            }

            return eventResults;
        }

    }
}