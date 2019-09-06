using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using OmdbToGnoss.Model;
using OmdbToGnoss.Services;
using System.IO;
using System.Text.RegularExpressions;

namespace OmdbToGnoss.Controller
{
    public class OmdbToGnossController
    {
        private static Regex regexPharenteses = new Regex("\\(([^)]+)\\)");

        private string _omdbApiKey;

        public OmdbToGnossController(string omdbApiKey)
        {
            _omdbApiKey = omdbApiKey;
        }

        public List<Movie> DownloadSomeMovies()
        {
            List<Movie> movies = new List<Movie>();
            OmdbService omdbService = new OmdbService(_omdbApiKey);

            CsvReader csvReader = new CsvReader(new StreamReader(@"..\..\Data\imdb_films_2000-2018_7.tsv"), new Configuration() { Delimiter = "\t", HasHeaderRecord = true, BadDataFound = null, IgnoreQuotes = true });
            int num = 0;

            while (csvReader.Read())
            {
                try
                {
                    var record = csvReader.GetRecord<ImdbTitleBasics>();
                    
                    movies.Add(omdbService.DownloadMovie(record.tconst));
                    Console.WriteLine($"{++num} Downloaded: {record.originalTitle}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return movies;
        }        

        public List<TVSeries> DownloadSomeSeries()
        {
            List<TVSeries> series = new List<TVSeries>();
            OmdbService omdbService = new OmdbService(_omdbApiKey);

            int num = 0;
            CsvReader csvReader = new CsvReader(new StreamReader(@"..\..\Data\imdb_series_2010-2018_8-10000.tsv"), new Configuration() { Delimiter = "\t", HasHeaderRecord = true, BadDataFound = null, IgnoreQuotes = true });

            while (csvReader.Read())
            {
                try
                {
                    var record = csvReader.GetRecord<ImdbTitleBasics>();

                    series.Add(omdbService.DownloadTVSerie(record.tconst));
                    Console.WriteLine($"{++num} Downloaded: {record.originalTitle}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return series;
        }

        public List<string> GetPeopleFromMovies(List<Movie> movies)
        {
            HashSet<string> peopleList = new HashSet<string>();

            peopleList.UnionWith(movies.SelectMany(movie => GetListStringFromString(movie.Writer)));
            peopleList.UnionWith(movies.SelectMany(movie => GetListStringFromString(movie.Director)));
            peopleList.UnionWith(movies.SelectMany(movie => GetListStringFromString(movie.Actors)));

            return peopleList.ToList();
        }

        public List<string> GetGenresFromMovies(List<Movie> movies)
        {
            HashSet<string> genres = new HashSet<string>();

            genres.UnionWith(movies.SelectMany(movie => GetListStringFromString(movie.genres)));

            return genres.ToList();
        }

        private IEnumerable<string> GetListStringFromString(string commaSeparatedField)
        {
            if (!string.IsNullOrEmpty(commaSeparatedField) && !commaSeparatedField.Equals("N/A"))
            {
                if (regexPharenteses.IsMatch(commaSeparatedField))
                {
                    // Remove special roles in people, like screenplay, novel...
                    foreach (Match match in regexPharenteses.Matches(commaSeparatedField))
                    {
                        commaSeparatedField = commaSeparatedField.Replace(match.Value, "");
                    }
                }

                return commaSeparatedField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(field => field.Trim().ToANSIFromUTF8());
            }
            return Enumerable.Empty<string>();
        }
    }
}
