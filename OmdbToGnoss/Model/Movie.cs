using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Helpers;
using Gnoss.ApiWrapper.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Model
{
    public class Movie : CreativeWork
    {
        public Movie()
        {
            ActorSubjects = new List<string>();
            DirectorSubjects = new List<string>();
            WriterSubjects = new List<string>();
            GenreSubjects = new List<string>();
        }

        public string Actors { get; set; }

        [JsonProperty(PropertyName = "Country")]
        public string CountryOfOrigin { get; set; }

        public string Director { get; set; }

        [JsonProperty(PropertyName = "Runtime")]
        public string Duration { get; set; }

        [JsonProperty(PropertyName = "Production")]
        public string ProductionCompany { get; set; }

        public string Writer { get; set; }

        public List<Rating> Ratings { get; set; }

        public List<string> ActorSubjects { get; set; }

        public List<string> DirectorSubjects { get; set; } 

        public List<string> WriterSubjects { get; set; }

        public List<string> GenreSubjects { get; set; }

        [JsonProperty(PropertyName = "Genre")]
        public string genres { get; set; }

        public ComplexOntologyResource ToGnossResource(ResourceApi resourceApi)
        {
            ComplexOntologyResource resource = new ComplexOntologyResource();
            List<OntologyEntity> relatedEntityes = new List<OntologyEntity>();
            List<OntologyProperty> properties = GetMovieProperties(relatedEntityes);

            resource.TextCategories = new List<string> { "Peliculas" };

            Ontology ont = new Ontology(resourceApi.GraphsUrl, resourceApi.OntologyUrl, Constants.Classes.Movie, Constants.Classes.Movie, Constants.Prefixes.PrefixesList, properties, relatedEntityes);
            resource.Ontology = ont;

            //Image
            List<ImageAction> listAcciones = new List<ImageAction>();
            listAcciones.Add(new ImageAction(234, ImageTransformationType.Crop, 100));
            listAcciones.Add(new ImageAction(318, ImageTransformationType.ResizeToWidth, 100));

            if (this.Image != null)
            {
                try
                {
                    resource.AttachImage(this.Image, listAcciones, Constants.Properties.Image, true, "jpg");
                }
                catch (Exception e)
                {

                    resourceApi.Log.Info(e.Message);
                }

                resource.Title = this.Title;
                resource.Visibility = ResourceVisibility.open;

                //resource.TextCategories = new List<string>();
                //foreach (string genre in this.genres.Split(',').ToList())
                //{
                //    resource.TextCategories.Add(genre.Trim());
                //}
            }
            return resource;
        }

        private List<OntologyProperty> GetMovieProperties(List<OntologyEntity> relatedEntities)
        {
            List<OntologyProperty> properties = new List<OntologyProperty>();

            if (!string.IsNullOrEmpty(CountryOfOrigin) && !CountryOfOrigin.Equals("N/A"))
            {
                properties.Add(new ListStringOntologyProperty(Constants.Properties.CountryOfOrigin, this.CountryOfOrigin.Split(',').Select(s => s.Trim()).ToList()));
            }
            if (!string.IsNullOrEmpty(Duration) && !Duration.Equals("N/A"))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Duration, this.Duration.Replace("min", "").Trim()));
            }
            if (!string.IsNullOrEmpty(ProductionCompany) && !ProductionCompany.Equals("N/A"))
            {
                properties.Add(new ListStringOntologyProperty(Constants.Properties.ProductionCompany, this.ProductionCompany.Split(',').Select(s => s.Trim()).ToList()));
            }
            if (!string.IsNullOrEmpty(Awards) && !Awards.Equals("N/A"))
            {
                properties.Add(new ListStringOntologyProperty(Constants.Properties.Award, this.Awards.Split(',').Select(s => s.Trim()).ToList()));
            }
            if (!string.IsNullOrEmpty(ContentRating) && !ContentRating.Equals("N/A"))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.ContentRating, this.ContentRating));
            }
            properties.Add(new DateOntologyProperty(Constants.Properties.DatePublished, this.DatePublished));
            if (!string.IsNullOrEmpty(Description) && !Description.Equals("N/A"))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Description, this.Description));
            }
            if (!string.IsNullOrEmpty(InLanguage) && !InLanguage.Equals("N/A"))
            {
                properties.Add(new ListStringOntologyProperty(Constants.Properties.InLanguage, this.InLanguage.Split(',').Select(s => s.Trim()).ToList()));
            }
            if (!string.IsNullOrEmpty(Title) && !Title.Equals("N/A"))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Name, this.Title));
            }
            if (!string.IsNullOrEmpty(RecorededAt) && !RecorededAt.Equals("N/A"))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.RecordedAt, this.RecorededAt));
            }

            properties.Add(new ListStringOntologyProperty(Constants.Properties.Url, this.Url));

            properties.Add(new ListStringOntologyProperty(Constants.Properties.Director, this.DirectorSubjects));
            properties.Add(new ListStringOntologyProperty(Constants.Properties.Author, this.WriterSubjects));
            properties.Add(new ListStringOntologyProperty(Constants.Properties.Actor, this.ActorSubjects));
            properties.Add(new ListStringOntologyProperty(Constants.Properties.Genre, this.GenreSubjects));

            int maxRating = 0;
            
            foreach (Rating rating in this.Ratings)
            {
                List<OntologyProperty> relatedEntityProperties = new List<OntologyProperty>();
                relatedEntityProperties.Add(new StringOntologyProperty(Constants.Properties.RatingSource, rating.Source));
                relatedEntityProperties.Add(new StringOntologyProperty(Constants.Properties.RatingValue, rating.Value));
                OntologyEntity entRating = new OntologyEntity(Constants.Classes.Rating, Constants.Classes.Rating, Constants.Properties.Rating, relatedEntityProperties);
                relatedEntities.Add(entRating);

                int auxRatingValue = 0;
                if (int.TryParse(rating.Value, out auxRatingValue) && auxRatingValue > maxRating)
                {
                    maxRating = auxRatingValue;
                }
            }

            if (maxRating > 0)
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.RatingValue, maxRating.ToString()));
            }

            return properties;
        }

        public override string ToString()
        {
            return Writer;
        }
    }
}