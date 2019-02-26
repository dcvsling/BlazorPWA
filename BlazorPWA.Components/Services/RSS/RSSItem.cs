using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BlazorPWA.Components.Services.RSS
{
    public class RSSItem
    {
        public String Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public List<RSSCategory> Categories { get; set; }
        public List<RSSPerson> Contributors { get; set; }
        public List<RSSLink> Links { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public DateTimeOffset Published { get; set; }
        public ISyndicationContent Content { get; set; }
        public List<ISyndicationImage> Images { get; set; }


    }
}

