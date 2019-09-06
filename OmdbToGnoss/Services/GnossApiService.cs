using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Model;
using OmdbToGnoss.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Services
{
    public class GnossApiService
    {
        private ResourceApi _resourceAPI;
        private Dictionary<string, string> _peopleSubjectInGraphByName;
        private Dictionary<string, string> _genreSubjectInGraphByName;
        

        public GnossApiService(string oauthConfigPath)
        {
            _resourceAPI = new ResourceApi(oauthConfigPath);
        }

        public void EliminarRecursosCargados()
        {
            
            List<Guid> listaIds = new List<Guid>();
            string[] lines = File.ReadAllLines(@"C:\Users\juan\Desktop\temporales\recursos_borrados_omdb.txt");
            foreach (string line in lines)
            {
                listaIds.Add(new Guid(line));
            }
            _resourceAPI.PersistentDeleteResourceIdList(listaIds);
        }

        private Dictionary<string, string> PeopleSubjectInGraphByName
        {
            get
            {
                if (_peopleSubjectInGraphByName == null)
                {
                    _peopleSubjectInGraphByName = new Dictionary<string, string>();

                    _peopleSubjectInGraphByName = GetPeopleFromGraph();
                }
                return _peopleSubjectInGraphByName;
            }
        }

        private Dictionary<string, string> GenreSubjectInGraphByName
        {
            get
            {
                if (_genreSubjectInGraphByName == null)
                {
                    _genreSubjectInGraphByName = new Dictionary<string, string>();

                    _genreSubjectInGraphByName = GetGenresFromGraph();
                }
                return _genreSubjectInGraphByName;
            }
        }

        public void UploadMovies(List<Movie> someMovies)
        {
            int numero = 1;
            _resourceAPI.ChangeOntoly("schema");
            foreach (Movie movie in someMovies)
            {
                if (!string.IsNullOrEmpty(movie.Title))
                {
                    foreach (string actor in movie.Actors.Split(',').ToList())
                    {
                        if (PeopleSubjectInGraphByName.ContainsKey(actor.Trim()))
                        {
                            movie.ActorSubjects.Add(PeopleSubjectInGraphByName[actor.Trim()]);
                        }
                    }
                    foreach (string writer in movie.Writer.Split(',').ToList())
                    {
                        if (PeopleSubjectInGraphByName.ContainsKey(writer.Trim()))
                        {
                            movie.WriterSubjects.Add(PeopleSubjectInGraphByName[writer.Trim()]);
                        }
                    }
                    foreach (string director in movie.Director.Split(',').ToList())
                    {
                        if (PeopleSubjectInGraphByName.ContainsKey(director.Trim()))
                        {
                            movie.DirectorSubjects.Add(PeopleSubjectInGraphByName[director.Trim()]);
                        }
                    }
                    foreach (string genre in movie.genres.Split(',').ToList())
                    {
                        if (GenreSubjectInGraphByName.ContainsKey(genre.Trim()))
                        {
                            movie.GenreSubjects.Add(GenreSubjectInGraphByName[genre.Trim()]);
                        }
                    }

                    ComplexOntologyResource gnossResource = movie.ToGnossResource(_resourceAPI);

                    try
                    {
                        _resourceAPI.LoadComplexSemanticResource(gnossResource);
                        Console.WriteLine($"Loaded: {movie.Title} {numero++}/{someMovies.Count()}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void UploadPeople(List<string> people)
        {
            _resourceAPI.ChangeOntoly("person");
            List<string> peopleAlreadyImported = PeopleSubjectInGraphByName.Keys.ToList();
            var peopleToAdd = people.Except(peopleAlreadyImported);

            int numero = 1;
            foreach (string person in peopleToAdd)
            {
                ComplexOntologyResource personGnossResource = new ComplexOntologyResource();

                List<OntologyProperty> propertyList = new List<OntologyProperty>();
                propertyList.Add(new StringOntologyProperty(Constants.Properties.Name, person));

                Ontology ont = new Ontology(_resourceAPI.GraphsUrl, _resourceAPI.OntologyUrl, Constants.Classes.Person, Constants.Classes.Person, Constants.Prefixes.PrefixesList, propertyList, null);
                personGnossResource.Ontology = ont;
                personGnossResource.Title = person;
                personGnossResource.Visibility = ResourceVisibility.open;

                string subject = _resourceAPI.LoadComplexSemanticResource(personGnossResource, false);
                Console.WriteLine($"Loaded: {person} {numero++}/{peopleToAdd.Count()}");
                PeopleSubjectInGraphByName.Add(person, subject);
            }            
        }

        public void UploadGenres(List<string> genres)
        {
            _resourceAPI.ChangeOntoly("genre");
            List<string> genresAlreadyImported = GenreSubjectInGraphByName.Keys.ToList();
            var genresToAdd = genres.Except(genresAlreadyImported); 

            foreach (string genre in genresToAdd)
            {
                SecondaryResource genreGnossResource = new SecondaryResource();

                List<OntologyProperty> propertyList = new List<OntologyProperty>();
                propertyList.Add(new StringOntologyProperty(Constants.Properties.Name, genre));

                genreGnossResource.Id = $"http://omdb.gnoss.com/items/{genre}";

                SecondaryOntology ont = new SecondaryOntology(_resourceAPI.GraphsUrl, _resourceAPI.OntologyUrl, Constants.Classes.Genre, Constants.Classes.Genre, Constants.Prefixes.PrefixesList, propertyList, genreGnossResource.Id);
                
                genreGnossResource.SecondaryOntology = ont;
                
                if (_resourceAPI.LoadSecondaryResource(genreGnossResource))
                {
                    GenreSubjectInGraphByName.Add(genre, genreGnossResource.Id);
                }
            }
        }        

        private Dictionary<string, string> GetPeopleFromGraph()
        {
            Dictionary<string, string> people = new Dictionary<string, string>();

            string select = "SELECT distinct ?name ?subject";
            string where = " WHERE {?subject <http://schema.org/name> ?name}";
            Gnoss.ApiWrapper.ApiModel.SparqlObject resultadoConsulta = _resourceAPI.VirtuosoQuery(select, where, "person");
            if (resultadoConsulta != null && resultadoConsulta.results != null && resultadoConsulta.results.bindings != null && resultadoConsulta.results.bindings.Count > 0)
            {
                foreach (Dictionary<string, Gnoss.ApiWrapper.ApiModel.SparqlObject.Data> dicpersonasQuery in resultadoConsulta.results.bindings)
                {
                    if (!people.ContainsKey(dicpersonasQuery["name"].value))
                    {
                        people.Add(dicpersonasQuery["name"].value, dicpersonasQuery["subject"].value);
                    }
                }

            }
            return people;
        }

        private Dictionary<string, string> GetGenresFromGraph()
        {
            Dictionary<string, string> genres = new Dictionary<string, string>();

            string select = "SELECT distinct ?name ?subject";
            string where = " WHERE {?subject <http://schema.org/name> ?name}";
            Gnoss.ApiWrapper.ApiModel.SparqlObject resultadoConsulta = _resourceAPI.VirtuosoQuery(select, where, "genre");
            if (resultadoConsulta != null && resultadoConsulta.results != null && resultadoConsulta.results.bindings != null && resultadoConsulta.results.bindings.Count > 0)
            {
                foreach (Dictionary<string, Gnoss.ApiWrapper.ApiModel.SparqlObject.Data> dicpersonasQuery in resultadoConsulta.results.bindings)
                {
                    if (!genres.ContainsKey(dicpersonasQuery["name"].value))
                    {
                        genres.Add(dicpersonasQuery["name"].value, dicpersonasQuery["subject"].value);
                    }
                }

            }
            return genres;
        }

        
    }
}
