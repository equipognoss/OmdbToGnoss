
using OmdbToGnoss.Controller;
using OmdbToGnoss.Model;
using OmdbToGnoss.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OmdbToGnoss
{
    class Program
    {
        private static string _omdbApiKey;
        static void Main(string[] args)
        {
            //Console.WriteLine("Insert your OMDB Api Key...");
            //_omdbApiKey = Console.ReadLine();

            GnossApiService gnossApiService = new GnossApiService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "config.xml"));
            gnossApiService.EliminarRecursosCargados();

            OmdbToGnossController omdbToGnossController = new OmdbToGnossController(_omdbApiKey);

            // Get some movies from OMDB API
            List<Movie> someMovies = omdbToGnossController.DownloadSomeMovies();

            // Get all people (actors, directors, writers...) from all these films
            List<string> people = omdbToGnossController.GetPeopleFromMovies(someMovies.ToList());

            // Get all people (actors, directors, writers...) from all these films
            List<string> genres = omdbToGnossController.GetGenresFromMovies(someMovies.ToList());

            // Upload people
            gnossApiService.UploadPeople(people);

            gnossApiService.UploadGenres(genres);

            // Upload movies
            gnossApiService.UploadMovies(someMovies.ToList());

            Console.WriteLine("Terminado. Pulsa cualquier tecla para continuar");
            Console.ReadKey();
        }
    }
}
