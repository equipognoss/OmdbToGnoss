using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Model;
using PeliculaOntology;
using PersonaOntology;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OMDbToGnoss
{
    class GnossApiService
    {

        private ResourceApi mResourceApi;
        public GnossApiService()
        {
            
            mResourceApi = new ResourceApi(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\OAuth.config")));

        }
        public void CargarPersonas(List<Person> personas, Dictionary<string,string> guidDictionary)
        {
            mResourceApi.ChangeOntoly("persona");
            Person personAux;
            try
            {
                SparqlObject resultados = mResourceApi.VirtuosoQuery("Select distinct ?id  ", "Where { ?o <http://schema.org/name> ?id }", "persona");
                foreach (var resultado in resultados.results.bindings)
                {
                    personAux = personas.Find(item => item.Schema_name.Equals(resultado["id"].value));
                    if (personAux != null)
                    {
                        personas.Remove(personAux);
                    }
                }
            }
            catch (Exception e)
            {
                mResourceApi.Log.Error($"Error al hacer la consulta a Virtuoso: {e.Message} -> {e.StackTrace}");
            }


            
            foreach (Person persona in personas)
            {
                try
                {
                    ComplexOntologyResource complexResource = persona.ToGnossApiResource(mResourceApi, null, new Guid(guidDictionary[persona.Schema_name].Split('_')[1]), new Guid(guidDictionary[persona.Schema_name].Split('_')[2]));
                    ReaderEditor editorRec = new ReaderEditor();
                    editorRec.group_short_name = "editores-rec";
                    List<ReaderEditor> grupoEditores = new List<ReaderEditor>();
                    grupoEditores.Add(editorRec);
                    complexResource.EditorsGroups = grupoEditores;
                    mResourceApi.LoadComplexSemanticResource(complexResource);
                }
                catch (Exception e)
                {
                    mResourceApi.Log.Error(persona.Schema_name);
                }
            }

        }

        public void CargarPeliculas(List<Movie> peliculas, Dictionary<string, string> guidDictionary)
        {
            mResourceApi.ChangeOntoly("pelicula");
            Movie peliculaAux;
            try
            {
                SparqlObject resultados = mResourceApi.VirtuosoQuery("Select distinct ?id  ", "Where { ?o <http://schema.org/name> ?id  }", "pelicula");
                foreach (var resultado in resultados.results.bindings)
                {
                    peliculaAux = peliculas.Find(item => item.Schema_name.Equals(resultado["id"].value));
                    if (peliculaAux != null)
                    {
                        peliculas.Remove(peliculaAux);
                    }
                }
            }
            catch (Exception e)
            {
                mResourceApi.Log.Error($"Error al hacer la consulta a Virtuoso: {e.Message} -> {e.StackTrace}");
            }



            foreach (Movie pelicula in peliculas)
            {
                    try
                    {
                        ComplexOntologyResource complexResource = pelicula.ToGnossApiResource(mResourceApi, null, new Guid(guidDictionary[pelicula.Schema_name].Split('_')[1]), new Guid(guidDictionary[pelicula.Schema_name].Split('_')[2]));
                        mResourceApi.LoadComplexSemanticResource(complexResource);
                    }
                    catch (Exception e)
                    {
                        mResourceApi.Log.Error(pelicula.Schema_name);
                    }
            }

        }
    }
}
