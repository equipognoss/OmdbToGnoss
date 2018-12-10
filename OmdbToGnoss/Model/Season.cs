using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class TVSeriesSeason
    {
        public string Title { get; set; }
        public int Season { get; set; }
        public int totalSeasons { get; set; }
        public List<TVSeriesEpisode> Episodes { get; set; }
    }
}
