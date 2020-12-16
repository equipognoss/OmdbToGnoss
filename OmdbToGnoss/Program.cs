using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OMDbToGnoss
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxPeliculas = 100; //Numero de peliculas a cargar
            LectorPeliculas lector = new LectorPeliculas(maxPeliculas);
            GnossApiService gnossApiService = new GnossApiService();
            gnossApiService.CargarPeliculas(lector.Movies, lector.PeliculasGuid);
            gnossApiService.CargarPersonas(lector.Persons, lector.PersonasGuid);
        }
    }
}
