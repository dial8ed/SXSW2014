using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;

namespace SxswFeed.Tests
{



//    {
//    "Data": [],
//    "StartTime": 0,
//    "Duration": 0,
//    "Comments": [],
//    "Attendees": [],
//    "RecurringType": 0,
//    "Url": "http://www.sxsw.com",
//    "Location": {},
//    "LocalizedDatum": {
//        "Title": "SXSW",
//        "Description": "South by Southwestey stuff"
//    },
//    "Security": { "Name": "", "MemberOf": 0, "ViewableBy": 0 }
//    "ID": "880C378B-75C3-4AD3-97C7-1C1FAF3FAF31",
//    "Indices": ["Awesome", "Cool", "Event"]
//}
    public class Event
    {
        

        private dynamic _jsonObject = new ExpandoObject();
        public dynamic JsonObject { get { return _jsonObject; } } 

        private Event()
        {
            //    "Data": [],
            _jsonObject.Data = new object();
            //    "StartTime": 0,
            _jsonObject.StartTime = "0";
            //    "Duration": 0,
            _jsonObject.Duration = "0";
            //    "Comments": [],
            _jsonObject.Comments = new string[] {};
            //    "Attendees": [],
            _jsonObject.Attendees = new string[] {};
            //    "RecurringType": 0,
            _jsonObject.RecurringType = "0";

            //    "Url": "http://www.sxsw.com",
            _jsonObject.Url = "";

            //    "LocalizedDatum": {
            //        "Title": "SXSW",
            //        "Description": "South by Southwestey stuff"
            //    },
            _jsonObject.LocalizedDatum = new ExpandoObject();
            _jsonObject.LocalizedDatum.Title = "";
            _jsonObject.LocalizedDatum.Description = "";

            //    "Security": { "Name": "", "MemberOf": 0, "ViewableBy": 0 }
            _jsonObject.Security = new ExpandoObject();
            _jsonObject.Security.Name = "";
            _jsonObject.Security.MemberOf = "0";
            _jsonObject.Security.ViewableBy = "0";

            //    "ID": "880C378B-75C3-4AD3-97C7-1C1FAF3FAF31",
            _jsonObject.ID = Guid.NewGuid();

            //    "Indices": ["Awesome", "Cool", "Event"]
            _jsonObject.Indices = new string[] {};

        }

        public static Event New(string url, string time, string title)
        {
            var newEvent = new Event();
            newEvent._jsonObject.LocalizedDatum.Title = title;
            newEvent._jsonObject.LocalizedDatum.Description = title;
            newEvent.MapTitleToIndicies(title);
            newEvent.MapTimeAndDuration(time);
           
            return newEvent;
        }

        private void MapTimeAndDuration(string time)
        {
            if (string.IsNullOrEmpty(time))
                return;

            
            var hyphen = char.Parse("-");//1:00PM-4:30PM
            var timeParts = time.Split(hyphen);
            if (timeParts.Length == 2)
            {
                this._jsonObject.StartTime =timeParts[0];
                var timeDiff = Convert.ToDateTime(timeParts[1]).Subtract(Convert.ToDateTime(timeParts[0])).TotalMinutes;
                this._jsonObject.Duration = timeDiff;
            }            
        }
                  
        private string[] MapTitleToIndicies(string title)
        {
            var comma = char.Parse(",");
            var space = char.Parse(" ");

            title.Replace(comma, space);
                                 
             var titleKeywords =  title.Split(space);
            _jsonObject.Indices = titleKeywords;

            return titleKeywords;
        }
                               
    }    
}