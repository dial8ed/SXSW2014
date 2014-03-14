using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Newtonsoft.Json;
using NUnit.Framework;

namespace SxswFeed.Tests
{
    [TestFixture]
    public class FeedWorkflowTests
    {
        private SxswFeed _feed;
        private XPathDocument _feedAsXmlDoc;

        public FeedWorkflowTests()
        {
            _feed = new SxswFeed();
            _feedAsXmlDoc = _feed.RetrieveAsXml();
            Assert.IsNotNull(_feedAsXmlDoc);
        }

        [Test]
        public void can_download_feed_as_xml_doc()
        {
            _feed = new SxswFeed();
            var webdocument = _feed.RetrieveAsXml();            
            Assert.IsNotNull(webdocument);            
        }

        [Test]
        public void can_parse_events_by_day_from_feed()
        {            
            dynamic schedules = _feed.ParseAllSchedules(_feedAsXmlDoc);                 
            Assert.IsNotNull(schedules);
        }

        [Test]
        public void can_serialize_schedules_as_json()
        {
            dynamic schedules = _feed.ParseAllSchedules(_feedAsXmlDoc);
            var json = Dynamic.ToJson(schedules);
            Assert.IsNotNullOrEmpty(json);
            Console.Write(json);
            
        }
             
    }
}


