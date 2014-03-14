using System;
using NUnit.Framework;

namespace SxswFeed.Tests
{
    [TestFixture]
    public class FeedWorkflowTests
    {
        
        [Test]
        public void can_download_feed_as_xml_doc()
        {
            var feed = new SxswFeed();
            var webdocument = feed.RetrieveAsXml();
            Console.WriteLine(webdocument.CreateNavigator().InnerXml);
            Assert.IsNotNull(webdocument);            
        }

       
        [Test]
        public void can_serialize_schedules_as_json()
        {
            var feed = new SxswFeed();
            var webdocument = feed.RetrieveAsXml();

            dynamic schedules = feed.ParseAllSchedules(webdocument);

            var json = Dynamic.ToJson(schedules);

            Assert.IsNotNullOrEmpty(json);
            Console.WriteLine(Dynamic.ToJson(schedules));
            
        }
             
    }
}


