using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class TVSeries : CreativeWork
    {
        public TVSeries()
        {
            Seasons = new List<TVSeriesSeason>();
        }

        public int totalSeasons { get; set; }
        public List<TVSeriesSeason> Seasons { get; set; }
    }
}
