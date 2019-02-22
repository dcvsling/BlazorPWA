using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BlazorPWA.Components.Services.RSS
{
    public class RSSLink
    {
        public Uri Uri { get; set; }
        public String Title { get; set; }
        public String MediaType { get; set; }
        public String RelationshipType { get; set; }
        public Int64 Length { get; set; }
        public DateTimeOffset LastUpdated { get; set; }



    }
}

