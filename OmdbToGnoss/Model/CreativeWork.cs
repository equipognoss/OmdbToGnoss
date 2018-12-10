using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class CreativeWork
    {
        public string Title { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Writer { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public List<Rating> Ratings { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string BoxOffice { get; set; }
        public string Website { get; set; }
        public string Runtime { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Country { get; set; }

        private string _imdbVotes;
        public string imdbVotes
        {
            get
            {
                return _imdbVotes;
            }
            set
            {
                _imdbVotes = value.Replace(",", "");
            }
        }

        private int _imdbVotesInt;
        internal int imdbVotesInt
        {
            get
            {
                _imdbVotesInt = 0;
                int.TryParse(_imdbVotes, out _imdbVotesInt);
                return _imdbVotesInt;
            }
        }

        private string _year;
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value.Substring(0, 4);
            }
        }

        private int _YearInt;
        internal int YearInt
        {
            get
            {
                _YearInt = 0;
                int.TryParse(_year, out _YearInt);
                return _YearInt;
            }
        }
    }
}
