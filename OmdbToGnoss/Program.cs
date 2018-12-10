using CsvHelper;
using CsvHelper.Configuration;
using Gnoss.ApiWrapper.Model;
using OmdbToGnoss.Model;
using OmdbToGnoss.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss
{
    class Program
    {
        private static string _omdbApiKey;
        static void Main(string[] args)
        {
            Console.WriteLine("Insert your OMDB Api Key...");
            _omdbApiKey = Console.ReadLine();

            List<Movie> someMovies = DownloadSomeFilms();
            //List<TVSeries> someSeries = DownloadSomeSeries();

            // Upload to GNOSS
            //GnossApiService gnossApiService = new GnossApiService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "config.xml"));
            //gnossApiService.UploadFilms(someMovies);
        }

        

        private static List<Movie> DownloadSomeFilms()
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
                    throw;
                }
            }

            return movies;
        }

        private static List<TVSeries> DownloadSomeSeries()
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
                    throw;
                }
            }

            return series;
        }
    }
}
