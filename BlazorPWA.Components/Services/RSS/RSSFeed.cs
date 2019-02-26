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
    public static class RSSFeed
    {
        public static async Task ReadFeed(string filepath)
        {
            //
            // Create an XmlReader from file
            // Example: ..\tests\TestFeeds\rss20-2items.xml
            using (var xmlReader = XmlReader.Create(filepath, new XmlReaderSettings() { Async = true }))
            {
                var parser = new RssParser();
                var feedReader = new RssFeedReader(xmlReader, parser);

                //
                // Read the feed
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType == SyndicationElementType.Item)
                    {
                        //
                        // Read the item as generic content
                        ISyndicationContent content = await feedReader.ReadContent();

                        //
                        // Parse the item if needed (unrecognized tags aren't available)
                        // Utilize the existing parser
                        ISyndicationItem item = parser.CreateItem(content);

                        Console.WriteLine($"Item: {item.Title}");

                        //
                        // Get <example:customElement> field
                        ISyndicationContent customElement = content.Fields.FirstOrDefault(f => f.Name == "example:customElement");

                        if (customElement != null)
                        {
                            Console.WriteLine($"{customElement.Name}: {customElement.Value}");
                        }
                    }
                }
            }
        }
    }
}

