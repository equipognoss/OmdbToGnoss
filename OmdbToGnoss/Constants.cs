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
        }

        public static class Properties
        {
            public const string Actor = "schema:actor";
            public const string Name = "schema:name";
            public const string CountryOfOrigin = "schema:countryOfOrigin";
            public const string Director = "schema:director";
            public const string Duration = "schema:duration";
            public const string ProductionCompany = "schema:productionCompany";
            public const string AggregateRating = "schema:aggregateRating";
            public const string Award = "schema:award";
            public const string ContentRating = "schema:contentRating";
            public const string DatePublished = "schema:datePublished";
            public const string Description = "schema:description";
            public const string Genre = "schema:genre";
            public const string Image = "schema:image";
            public const string InLanguage = "schema:inLanguage";
            public const string RecordedAt = "schema:recordedAt";
            public const string Url = "schema:url";
            public const string SeasonNumber = "schema:seasonNumber";
            public const string EpisodeNumber = "schema:episodeNumber";
            public const string ContainsSeason = "schema:containsSeason";
            public const string Episode = "schema:episode";
        }

        public static class Classes
        {
            public const string Movie = "http://schema.org/Movie";
            public const string TVSeries = "http://schema.org/TVSeries";
            public const string CreativeWorkSeason = "http://schema.org/CreativeWorkSeason";
            public const string Episode = "http://schema.org/Episode";
        }
    }
}
