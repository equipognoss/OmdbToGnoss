using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss
{
    public class Constants
    {
        public static class Prefixes
        {
            public const string Schema = "xmlns:schema=\"http://schema.org/\"";
            public const string Rdf = "xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\"";
            public const string Rdfs = "xmlns:rdfs=\"http://www.w3.org/2000/01/rdf-schema#\"";
            public const string Owl = "xmlns:owl=\"http://www.w3.org/2002/07/owl#\"";
            public const string Xsd = "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema#\"";
            public const string Sc = "xmlns:sc=\"http://schema.org/\"";

            public static readonly List<string> PrefixesList = new List<string>() {
                 Rdf,
                 Rdfs,
                 Owl,
                 Xsd,
                 Sc
            };
        }

        public static class Properties
        {
            public const string Actor = "sc:actor";
            public const string Name = "sc:name";
            public const string CountryOfOrigin = "sc:countryOfOrigin";
            public const string Director = "sc:director";
            public const string Duration = "sc:duration";
            public const string ProductionCompany = "sc:productionCompany";
            public const string AggregateRating = "sc:aggregateRating";
            public const string Award = "sc:award";
            public const string ContentRating = "sc:contentRating";
            public const string DatePublished = "sc:datePublished";
            public const string Description = "sc:description";
            public const string Genre = "sc:genre";
            public const string Image = "sc:image";
            public const string InLanguage = "sc:inLanguage";
            public const string RecordedAt = "sc:recordedAt";
            public const string Url = "sc:url";
            public const string SeasonNumber = "sc:seasonNumber";
            public const string EpisodeNumber = "sc:episodeNumber";
            public const string ContainsSeason = "sc:containsSeason";
            public const string Episode = "sc:episode";
            public const string Author = "sc:author";
            public const string RatingSource = "sc:ratingSource";
            public const string RatingValue = "sc:ratingValue";
            public const string Rating = "sc:rating";
        }

        public static class Classes
        {
            public const string Person = "http://schema.org/Person";
            public const string Movie = "http://schema.org/Movie";
            public const string Rating = "http://schema.org/Rating";
            public const string TVSeries = "http://schema.org/TVSeries";
            public const string CreativeWorkSeason = "http://schema.org/CreativeWorkSeason";
            public const string Episode = "http://schema.org/Episode";
            public const string Genre = "http://schema.org/Genre";
        }

        public static class Services
        {
            public const string URL_OMDB_API = "http://www.omdbapi.com/";
        }
    }
}
