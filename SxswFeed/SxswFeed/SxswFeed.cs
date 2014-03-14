using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using SxswFeed.Tests;

namespace SxswFeed
{
    
    public class SxswFeed
    {

        public XPathDocument RetrieveAsXml()
        {            
            return new XPathDocument("http://feed43.com/1563618815845483.xml");                       
        }

        public dynamic ParseAllSchedules(XPathDocument schedulesXml)
        {
            var nav = schedulesXml.CreateNavigator().Select("//item");

            dynamic dailySchedule = new ExpandoObject();
            dailySchedule.Events = new List<dynamic>();
            dynamic allSchedules = new ExpandoObject();
            allSchedules.List = new List<dynamic>();
            
            while (nav.MoveNext())
            {
                // schedule date
                var eventDate = nav.Current.SelectSingleNode("title").Value;
                dailySchedule.Date = eventDate;

                // event details dirty
                var eventDetails = nav.Current.SelectSingleNode("description").Value;

                //event details clean
                eventDetails = Tidy(eventDetails);

                var eventRows = eventDetails.Split(char.Parse("|"));
                foreach (var row in eventRows)
                {
                    //Console.WriteLine(row);

                    var eventCell = row.Split(char.Parse("^"));
                    if (eventCell.Length == 2)
                    {
                        var time = eventCell[0];

                        var titleCells = eventCell[1].Split(new string[] {@""">"}, StringSplitOptions.RemoveEmptyEntries);
                        var link = titleCells[0];
                        var title = titleCells[1];

                        dailySchedule.Events.Add(Event.New(link, time, title).JsonObject);
                    }
                }
                allSchedules.List.Add(dailySchedule);
            }

            return allSchedules;
        }

        private string Tidy(string eventDetails)
        {
            eventDetails = eventDetails.Replace(@"</td><td>", "^");
            eventDetails = eventDetails.Replace(@"</tr>", "|");
            eventDetails = eventDetails.Replace(@"<p>", "*(");
            eventDetails = eventDetails.Replace(@"</p>", ")*");
            eventDetails = eventDetails.Replace(@"<a href=""", string.Empty);
            eventDetails = eventDetails.Replace(@"</a>", string.Empty);
            //                      
            eventDetails = Regex.Replace(eventDetails, "(.*?)", string.Empty);
            eventDetails = Regex.Replace(eventDetails, "<.*?>", string.Empty);

            return eventDetails;
        }

    }
}