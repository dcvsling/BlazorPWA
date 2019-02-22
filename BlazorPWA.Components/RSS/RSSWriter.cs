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
    public class RSSWriter
    {
        async public Task<string> Write(RSSItem rss)
        {
            using (var sw = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true }))
                {
                    var writer = new RssFeedWriter(xmlWriter);
                    await writer.Write(rss.MapTo());
                    xmlWriter.Flush();
                }
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}

