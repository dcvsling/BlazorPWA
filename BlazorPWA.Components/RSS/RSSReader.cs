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
    public class RSSReader
    {
        async public Task<List<RSSItem>> Read(string path)
        {
            var list = new List<RSSItem>();

            using (var xmlReader = XmlReader.Create(path, new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);
                RSSItem rss = null;
                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            rss.Categories.Add((await feedReader.ReadCategory()).MapTo());
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            rss.Images.Add(await feedReader.ReadImage());
                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            rss = (await feedReader.ReadItem()).MapTo();
                            break;

                        // Read link
                        case SyndicationElementType.Link:
                            rss.Links.Add((await feedReader.ReadLink()).MapTo());
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            rss.Contributors.Add((await feedReader.ReadPerson()).MapTo());
                            break;

                        // Read content
                        default:
                            rss.Content = await feedReader.ReadContent();
                            break;
                    }
                }
                list.Add(rss);
            }
            return list;
        }
    }
}

