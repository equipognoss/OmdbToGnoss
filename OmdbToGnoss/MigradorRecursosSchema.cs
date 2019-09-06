using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.Model;
using Newtonsoft.Json;
using OmdbToGnoss.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss
{
    class MigradorRecursosSchema
    {
        ResourceApi mRACarga = new ResourceApi(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Config\\configOAuth\\OAuth_V3_Omdb.xml");
        static string mRutaJson = @"C:\Users\urudiez\OmdbToGnoss-master\OmdbToGnoss\Data";
        string[] mJsonFiles = Directory.GetFiles(mRutaJson);
        List<string> mListPeople = new List<string>();
        List<Movie> mListPeliculas = new List<Movie>();
        Dictionary<string, string> mDicPersonasVirtuoso = new Dictionary<string, string>();
        public void LlamadaMetodos()
        {            
            CargarPersonas();
            mDicPersonasVirtuoso = ObtenerPersonasVirtuoso();
            ObtenerPeliculasJson();
            CargarPeliculas();
        }

        private List<Movie> ObtenerPeliculasJson()
        { 
            foreach (string ruta in mJsonFiles)
            {
                string archivoJson = File.ReadAllText(ruta);
                if (archivoJson.Contains("\"Type\":\"movie\",") && !archivoJson.Contains("Ã"))
                {
                    Movie movie = new Movie();
                    movie = JsonConvert.DeserializeObject<Movie>(archivoJson);

                    movie.ActorSubjects = new List<string>();
                    movie.DirectorSubjects = new List<string>();
                    movie.WriterSubjects = new List<string>();

                    //Asociamos la persona cargada a la peli
                    foreach (string actor in movie.Actors.Split(',').ToList())
                    {
                        if (mDicPersonasVirtuoso.ContainsKey(actor))
                        {
                            movie.ActorSubjects.Add(mDicPersonasVirtuoso[actor]);
                        }
                    }
                    foreach (string writer in movie.Writer.Split(',').ToList())
                    {
                        if (mDicPersonasVirtuoso.ContainsKey(writer))
                        {
                            movie.WriterSubjects.Add(mDicPersonasVirtuoso[writer]);
                        }
                    }
                    foreach (string director in movie.Director.Split(',').ToList())
                    {
                        if (mDicPersonasVirtuoso.ContainsKey(director))
                        {
                            movie.DirectorSubjects.Add(mDicPersonasVirtuoso[director]);
                        }
                    }
                    foreach (Rating rating in movie.Ratings)
                    {
                        if (rating.Source.Equals("Internet Movie Database"))
                        {
                            if (rating.Value.Contains("/"))
                            {
                                rating.Value = rating.Value.Replace(rating.Value.Substring(rating.Value.IndexOf("/")), "").Replace(".", "");
                            }
                        }
                        else if (rating.Source.Equals("Rotten Tomatoes"))
                        {
                            if (rating.Value.Contains("%"))
                            {
                                rating.Value = rating.Value.Replace("%", "");
                            }
                        }
                        else if (rating.Source.Equals("Metacritic"))
                        {
                            if (rating.Value.Contains("/"))
                            {
                                rating.Value = rating.Value.Replace(rating.Value.Substring(rating.Value.IndexOf("/")), "").Replace(".", "");
                            }
                        }
                    }
                    mListPeliculas.Add(movie);
                    if (mListPeliculas.Count == 30)
                    {
                        break;
                    }
                }
            }
            return mListPeliculas;
        }        

        private Dictionary<string, string> ObtenerPersonasVirtuoso()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string select = "SELECT *";
            string where = " WHERE {?s <http://schema.org/name> ?name}";
            Gnoss.ApiWrapper.ApiModel.SparqlObject resultadoConsulta = mRACarga.VirtuosoQuery(select, where, "person");
            if (resultadoConsulta != null && resultadoConsulta.results != null && resultadoConsulta.results.bindings != null && resultadoConsulta.results.bindings.Count > 0)
            {
                foreach (Dictionary<string, Gnoss.ApiWrapper.ApiModel.SparqlObject.Data> dicpersonasQuery in resultadoConsulta.results.bindings)
                {
                    if (!dict.ContainsKey(dicpersonasQuery["name"].value))
                    {
                        dict.Add(dicpersonasQuery["name"].value, dicpersonasQuery["s"].value);
                    }
                }
            }
            return dict;
        }


        public void CargarPersonas()
        {
            mDicPersonasVirtuoso = ObtenerPersonasVirtuoso();
            mRACarga.ChangeOntoly("person");
            Person persona = new Person();
            List<string> listaPeople = persona.obtenerPersonasJson(mJsonFiles, mDicPersonasVirtuoso);
            List<ComplexOntologyResource> listaRecursosCarga = persona.toRecurso(mRACarga, listaPeople);
            mRACarga.LoadComplexSemanticResourceList(listaRecursosCarga, false);
        }

        public void CargarPeliculas()
        {
            Movie pelicula = new Movie();
            List<ComplexOntologyResource> listaRecursosCarga = pelicula.ToGnossResource(mRACarga, mListPeliculas);
            mRACarga.LoadComplexSemanticResourceList(listaRecursosCarga, false);
        }
    }
}