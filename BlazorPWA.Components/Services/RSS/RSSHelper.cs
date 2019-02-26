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

    public static class RSSHelper
    {
        public static RSSLink MapTo(this ISyndicationLink link)
            => new RSSLink
            {
                Uri = link.Uri,
                Title = link.Title,
                MediaType = link.MediaType,
                RelationshipType = link.RelationshipType,
                Length = link.Length,
                LastUpdated = link.LastUpdated
            };
        public static RSSPerson MapTo(this ISyndicationPerson person)
        => new RSSPerson
        {
            Name = person.Name,
            Email = person.Email,
            Uri = person.Uri,
            RelationshipType = person.RelationshipType
        };


        public static RSSCategory MapTo(this ISyndicationCategory category)
            => new RSSCategory
            {
                Name = category.Name,
                Label = category.Label,
                Scheme = category.Scheme
            };
        public static RSSItem MapTo(this ISyndicationItem item)
            => new RSSItem
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Categories = item.Categories.Select(MapTo).ToList(),
                Contributors = item.Contributors.Select(MapTo).ToList(),
                Links = item.Links.Select(MapTo).ToList(),
                LastUpdated = item.LastUpdated,
                Published = item.Published,
            };

        public static ISyndicationCategory MapTo(this RSSCategory category)
               => new SyndicationCategory(category.Name)
               {
                   Label = category.Label,
                   Scheme = category.Scheme,
               };

        public static ISyndicationPerson MapTo(this RSSPerson person)
              => new SyndicationPerson(person.Name, person.Email)
              {
                  Uri = person.Uri,
                  RelationshipType = person.RelationshipType
              };
        public static ISyndicationLink MapTo(this RSSLink link)
               => new SyndicationLink(link.Uri, link.MediaType)
               {
                   Title = link.Title,
                   Length = link.Length,
                   LastUpdated = link.LastUpdated
               };
        public static ISyndicationItem MapTo(this RSSItem item)
        {
            var result = new SyndicationItem
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                LastUpdated = item.LastUpdated,
                Published = item.Published
            };

            item.Categories.Select(MapTo).ToList().ForEach(result.AddCategory);
            item.Links.Select(MapTo).ToList().ForEach(result.AddLink);
            item.Contributors.Select(MapTo).ToList().ForEach(result.AddContributor);
            return result;
        }
    }
}

