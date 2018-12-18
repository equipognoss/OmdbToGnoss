using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Model;
using OmdbToGnoss.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Services
{
    public class GnossApiService
    {
        private ResourceApi _resourceAPI;

        public GnossApiService(string oauthConfigPath)
        {
            _resourceAPI = new ResourceApi(oauthConfigPath);
        }

        public void UploadFilms(List<Movie> someMovies)
        {
            // Prefijos de ontología
            List<string> prefixes = new List<string>() {
                 Constants.Prefixes.Schema
            };

            foreach (Movie movie in someMovies)
            {
                ComplexOntologyResource resource = new ComplexOntologyResource();

                List<OntologyProperty> properties = GetMovieProperties(movie);
                byte[] imageBytes = GetCreativeWorkImage(movie);

                Ontology ont = new Ontology(_resourceAPI.GraphsUrl, _resourceAPI.OntologyUrl, Constants.Classes.Movie, Constants.Classes.Movie, prefixes, properties/*, relatedEntitiesList*/);

                resource.Ontology = ont;

                resource.CreationDate = DateTime.Now;
                resource.Visibility = ResourceVisibility.open;

                if(imageBytes != null)
                {
                    List<ImageAction> actions = new List<ImageAction>();
                    actions.Add(new ImageAction(234, Gnoss.ApiWrapper.Helpers.ImageTransformationType.Crop, 100));
                    actions.Add(new ImageAction(660, Gnoss.ApiWrapper.Helpers.ImageTransformationType.ResizeToHeight, 100));

                    resource.AttachImage(imageBytes, actions, Constants.Properties.Image, true, ImageService.GetImageExtensionFromURL(movie.Poster));
                }

                try
                {
                    _resourceAPI.LoadComplexSemanticResource(resource);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void UploadTvSeries(List<TVSeries> someSeries)
        {
            // TODO
        }

        public byte[] GetCreativeWorkImage(CreativeWork creativeWork)
        {
            byte[] imageBytes = null;
            if (!string.IsNullOrEmpty(creativeWork.Poster))
            {
                imageBytes = ImageService.DownloadImage(creativeWork.Poster);
            }

            return imageBytes;
        }

        public List<OntologyProperty> GetCreativeWorkProperties(CreativeWork creativeWork)
        {
            List<OntologyProperty> properties = new List<OntologyProperty>();

            if (!string.IsNullOrEmpty(creativeWork.Title))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Name, creativeWork.Title));
            }

            if (!string.IsNullOrEmpty(creativeWork.Plot))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Description, creativeWork.Plot));
            }

            if (!string.IsNullOrEmpty(creativeWork.Country))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.CountryOfOrigin, creativeWork.Country));
            }

            if (creativeWork.Ratings != null && creativeWork.Ratings.Count > 0)
            {
                foreach (Rating rating in creativeWork.Ratings)
                {
                    properties.Add(new StringOntologyProperty(Constants.Properties.AggregateRating, $"{rating.Source}: {rating.Value}"));
                }
            }

            if (!string.IsNullOrEmpty(creativeWork.Runtime))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Duration, creativeWork.Runtime));
            }

            if (!string.IsNullOrEmpty(creativeWork.Awards))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Award, creativeWork.Awards));
            }

            if (!string.IsNullOrEmpty(creativeWork.Rated))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.ContentRating, creativeWork.Rated));
            }

            if (!string.IsNullOrEmpty(creativeWork.Language))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.InLanguage, creativeWork.Language));
            }

            if (!string.IsNullOrEmpty(creativeWork.Released))
            {
                properties.Add(new DateOntologyProperty(Constants.Properties.DatePublished, DateTime.Parse(creativeWork.Released)));
            }

            if (!string.IsNullOrEmpty(creativeWork.Year))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.RecordedAt, creativeWork.Year));
            }

            if (!string.IsNullOrEmpty(creativeWork.Website))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.Url, creativeWork.Website));
            }

            return properties;
        }

        

        public List<OntologyProperty> GetMovieProperties(Movie movie)
        {
            List<OntologyProperty> properties = GetCreativeWorkProperties(movie);

            if (!string.IsNullOrEmpty(movie.Production))
            {
                properties.Add(new StringOntologyProperty(Constants.Properties.ProductionCompany, movie.Production));
            }

            return properties;
        }
    }
}
