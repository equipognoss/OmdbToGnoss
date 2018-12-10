using System.Collections.Generic;
using System.Linq;

namespace OmdbToGnoss.Model
{
    internal class ImdbTitleBasics
    {
        public string tconst { get; set; }
        public string titleType { get; set; }
        public string primaryTitle { get; set; }
        public string originalTitle { get; set; }
        public string isAdult { get; set; }
        public string startYear { get; set; }
        public string endYear { get; set; }
        public string runtimeMinutes { get; set; }
        public string genres { get; set; }

        internal List<string> GenresList
        {
            get
            {
                return genres.Split(',').ToList();
            }
        }

        private bool _isAdultBool;
        internal bool isAdultBool
        {
            get
            {
                _isAdultBool = false;
                bool.TryParse(isAdult, out _isAdultBool);
                return _isAdultBool;
            }
        }

        private int _startYearInt;
        internal int startYearInt
        {
            get
            {
                _startYearInt = 0;
                int.TryParse(startYear, out _startYearInt);
                return _startYearInt;
            }
        }

        private int _endYearInt;
        internal int endYearInt
        {
            get
            {
                _endYearInt = 0;
                int.TryParse(endYear, out _endYearInt);
                return _endYearInt;
            }
        }

        private int _runtimeMinutesInt;
        internal int runtimeMinutesInt
        {
            get
            {
                _runtimeMinutesInt = 0;
                int.TryParse(runtimeMinutes, out _runtimeMinutesInt);
                return _runtimeMinutesInt;
            }
        }
    }

    internal class ImdbTitleRating 
    {

        public string tconst { get; set; }
        public string averageRating { get; set; }

        public string numVotes { get; set; }
    }

    internal class ImdbTitleBasicsWithRating : ImdbTitleBasics
    {
        public ImdbTitleBasicsWithRating(ImdbTitleBasics other) 
        {
            this.endYear = other.endYear;
            this.genres = other.genres;
            this.isAdult = other.isAdult;
            this.originalTitle = other.originalTitle;
            this.primaryTitle = other.primaryTitle;
            this.runtimeMinutes = other.runtimeMinutes;
            this.startYear = other.startYear;
            this.tconst = other.tconst;
            this.titleType = other.titleType;
        }

        private string _averageRating;
        public string averageRating
        {
            get
            {
                return _averageRating;
            }
            set
            {
                _averageRating = value.Replace(".", ",");
            }
        }

        private float _averageRatingFloat;
        internal float averageRatingFloat
        {
            get
            {
                _averageRatingFloat = 0;
                float.TryParse(_averageRating, out _averageRatingFloat);
                return _averageRatingFloat;
            }
        }

        public string numVotes { get; set; }
    }
}