using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class CreativeWork
    {
        [JsonProperty(PropertyName = "Ratings")]
        public List<Rating> AggregateRating { get; set; }
        public List<string> Authors { get; set; }
        /// <summary>
        /// Varios valores separados por comas dentro del string ????
        /// </summary>
        public string Awards { get; set; }
        [JsonProperty(PropertyName = "Rated")]
        public string ContentRating { get; set; }
        [JsonProperty(PropertyName = "Released")]
        public DateTime DatePublished { get; set; }
        [JsonProperty(PropertyName = "Plot")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "Poster")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "Language")]
        /// <summary>
        /// Varios valores separados por comas dentro del string ????
        /// </summary>
        public string InLanguage { get; set; }
        public string Title { get; set; }
        public string RecorededAt { get; set; }
        public List<string> Url { get; set; }

    }
}
