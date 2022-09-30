using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using PointsCalculator.Models.PaxResults;

namespace PointsCalculator
{
    public class PaxResultsParser
    {
        private readonly string _html;

        public PaxResultsParser(string html)
        {
            _html = html;
        }

        public EventResults GenerateResults()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(_html);

            var eventNumberElement = htmlDocument.DocumentNode.SelectNodes("//table/tbody").First().ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList()[1];

            var pointsEventNumber = int.Parse(eventNumberElement.OuterHtml[eventNumberElement.OuterHtml.IndexOf('#') + 1].ToString());

            var eventResults = new EventResults
            {
                EventNumber = pointsEventNumber,
                Results = new List<EventEntry>()
            };

            // Results table we care about happens to be the last table on the page
            var resultsTable = htmlDocument.DocumentNode.SelectNodes("//table/tbody").Last(); // this is the results table

            var rows = resultsTable.ChildNodes;

            var tableRows = rows.Where(x => x.NodeType == HtmlNodeType.Element).Skip(1).ToList(); // Skip header

            foreach (var row in tableRows)
            {
                var entry = ParseRow(pointsEventNumber, row);

                if (entry != null)
                {
                    eventResults.Results.Add(entry);
                }
            }

            return eventResults;
        }

        private EventEntry ParseRow(int eventNumber, HtmlNode row)
        {
            var cells = row.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element).ToList();

            var position = int.Parse(cells[0].InnerHtml.ToString());
            var name = cells[4].InnerHtml.ToString();

            if (double.TryParse(cells[8].InnerHtml.ToString(), out double time))
            {
                return new EventEntry
                {
                    Position = position,
                    Name = name,
                    PaxTime = time,
                    EventNumber = eventNumber
                };
            }

            return null;
        }
    }
}