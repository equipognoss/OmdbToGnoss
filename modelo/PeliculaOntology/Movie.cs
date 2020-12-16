using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.Model;
using Gnoss.ApiWrapper.Helpers;
using GnossBase;
using Es.Riam.Gnoss.Web.MVC.Models;
using Person = PersonaOntology.Person;

namespace PeliculaOntology
{
	public class Movie : GnossOCBase
	{

		public Movie() : base() { } 

		public Movie(SemanticResourceModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			this.mGNOSSID = pSemCmsModel.RootEntities[0].Entity.Uri;
			this.Schema_author = new List<Person>();
			SemanticPropertyModel propSchema_author = pSemCmsModel.GetPropertyByPath("http://schema.org/author");
			if(propSchema_author != null && propSchema_author.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_author.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_author = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_author.Add(schema_author);
					}
				}
			}
			this.Schema_rating = new List<Rating>();
			SemanticPropertyModel propSchema_rating = pSemCmsModel.GetPropertyByPath("http://schema.org/rating");
			if(propSchema_rating != null && propSchema_rating.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_rating.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Rating schema_rating = new Rating(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_rating.Add(schema_rating);
					}
				}
			}
			this.Schema_director = new List<Person>();
			SemanticPropertyModel propSchema_director = pSemCmsModel.GetPropertyByPath("http://schema.org/director");
			if(propSchema_director != null && propSchema_director.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_director.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_director = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_director.Add(schema_director);
					}
				}
			}
			this.Schema_actor = new List<Person>();
			SemanticPropertyModel propSchema_actor = pSemCmsModel.GetPropertyByPath("http://schema.org/actor");
			if(propSchema_actor != null && propSchema_actor.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_actor.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_actor = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_actor.Add(schema_actor);
					}
				}
			}
			SemanticPropertyModel propSchema_genre = pSemCmsModel.GetPropertyByPath("http://schema.org/genre");
			this.Schema_genre = new List<string>();
			if (propSchema_genre != null && propSchema_genre.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_genre.PropertyValues)
				{
					this.Schema_genre.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_url = pSemCmsModel.GetPropertyByPath("http://schema.org/url");
			this.Schema_url = new List<string>();
			if (propSchema_url != null && propSchema_url.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_url.PropertyValues)
				{
					this.Schema_url.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_aggregateRating = pSemCmsModel.GetPropertyByPath("http://schema.org/aggregateRating");
			this.Schema_aggregateRating = new List<string>();
			if (propSchema_aggregateRating != null && propSchema_aggregateRating.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_aggregateRating.PropertyValues)
				{
					this.Schema_aggregateRating.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_productionCompany = pSemCmsModel.GetPropertyByPath("http://schema.org/productionCompany");
			this.Schema_productionCompany = new List<string>();
			if (propSchema_productionCompany != null && propSchema_productionCompany.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_productionCompany.PropertyValues)
				{
					this.Schema_productionCompany.Add(propValue.Value);
				}
			}
			this.Schema_recordedAt = new List<int>();
			SemanticPropertyModel propSchema_recordedAt = pSemCmsModel.GetPropertyByPath("http://schema.org/recordedAt");
			if (propSchema_recordedAt != null && propSchema_recordedAt.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_recordedAt.PropertyValues)
				{
					this.Schema_recordedAt.Add(GetNumberIntPropertyMultipleValueSemCms(propValue).Value);
				}
			}
			SemanticPropertyModel propSchema_countryOfOrigin = pSemCmsModel.GetPropertyByPath("http://schema.org/countryOfOrigin");
			this.Schema_countryOfOrigin = new List<string>();
			if (propSchema_countryOfOrigin != null && propSchema_countryOfOrigin.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_countryOfOrigin.PropertyValues)
				{
					this.Schema_countryOfOrigin.Add(propValue.Value);
				}
			}
			this.Schema_duration = new List<int>();
			SemanticPropertyModel propSchema_duration = pSemCmsModel.GetPropertyByPath("http://schema.org/duration");
			if (propSchema_duration != null && propSchema_duration.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_duration.PropertyValues)
				{
					this.Schema_duration.Add(GetNumberIntPropertyMultipleValueSemCms(propValue).Value);
				}
			}
			SemanticPropertyModel propSchema_inLanguage = pSemCmsModel.GetPropertyByPath("http://schema.org/inLanguage");
			this.Schema_inLanguage = new List<string>();
			if (propSchema_inLanguage != null && propSchema_inLanguage.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_inLanguage.PropertyValues)
				{
					this.Schema_inLanguage.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_award = pSemCmsModel.GetPropertyByPath("http://schema.org/award");
			this.Schema_award = new List<string>();
			if (propSchema_award != null && propSchema_award.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_award.PropertyValues)
				{
					this.Schema_award.Add(propValue.Value);
				}
			}
			this.Schema_description = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/description"));
			this.Schema_image = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/image"));
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
			this.Schema_datePublished= GetDateValuePropertySemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/datePublished")).Value;
			this.Schema_contentRating = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/contentRating"));
		}

		public Movie(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			this.mGNOSSID = pSemCmsModel.Entity.Uri;
			this.mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			this.Schema_author = new List<Person>();
			SemanticPropertyModel propSchema_author = pSemCmsModel.GetPropertyByPath("http://schema.org/author");
			if(propSchema_author != null && propSchema_author.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_author.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_author = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_author.Add(schema_author);
					}
				}
			}
			this.Schema_rating = new List<Rating>();
			SemanticPropertyModel propSchema_rating = pSemCmsModel.GetPropertyByPath("http://schema.org/rating");
			if(propSchema_rating != null && propSchema_rating.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_rating.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Rating schema_rating = new Rating(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_rating.Add(schema_rating);
					}
				}
			}
			this.Schema_director = new List<Person>();
			SemanticPropertyModel propSchema_director = pSemCmsModel.GetPropertyByPath("http://schema.org/director");
			if(propSchema_director != null && propSchema_director.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_director.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_director = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_director.Add(schema_director);
					}
				}
			}
			this.Schema_actor = new List<Person>();
			SemanticPropertyModel propSchema_actor = pSemCmsModel.GetPropertyByPath("http://schema.org/actor");
			if(propSchema_actor != null && propSchema_actor.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_actor.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Person schema_actor = new Person(propValue.RelatedEntity,idiomaUsuario);
						this.Schema_actor.Add(schema_actor);
					}
				}
			}
			SemanticPropertyModel propSchema_genre = pSemCmsModel.GetPropertyByPath("http://schema.org/genre");
			this.Schema_genre = new List<string>();
			if (propSchema_genre != null && propSchema_genre.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_genre.PropertyValues)
				{
					this.Schema_genre.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_url = pSemCmsModel.GetPropertyByPath("http://schema.org/url");
			this.Schema_url = new List<string>();
			if (propSchema_url != null && propSchema_url.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_url.PropertyValues)
				{
					this.Schema_url.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_aggregateRating = pSemCmsModel.GetPropertyByPath("http://schema.org/aggregateRating");
			this.Schema_aggregateRating = new List<string>();
			if (propSchema_aggregateRating != null && propSchema_aggregateRating.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_aggregateRating.PropertyValues)
				{
					this.Schema_aggregateRating.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_productionCompany = pSemCmsModel.GetPropertyByPath("http://schema.org/productionCompany");
			this.Schema_productionCompany = new List<string>();
			if (propSchema_productionCompany != null && propSchema_productionCompany.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_productionCompany.PropertyValues)
				{
					this.Schema_productionCompany.Add(propValue.Value);
				}
			}
			this.Schema_recordedAt = new List<int>();
			SemanticPropertyModel propSchema_recordedAt = pSemCmsModel.GetPropertyByPath("http://schema.org/recordedAt");
			if (propSchema_recordedAt != null && propSchema_recordedAt.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_recordedAt.PropertyValues)
				{
					this.Schema_recordedAt.Add(GetNumberIntPropertyMultipleValueSemCms(propValue).Value);
				}
			}
			SemanticPropertyModel propSchema_countryOfOrigin = pSemCmsModel.GetPropertyByPath("http://schema.org/countryOfOrigin");
			this.Schema_countryOfOrigin = new List<string>();
			if (propSchema_countryOfOrigin != null && propSchema_countryOfOrigin.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_countryOfOrigin.PropertyValues)
				{
					this.Schema_countryOfOrigin.Add(propValue.Value);
				}
			}
			this.Schema_duration = new List<int>();
			SemanticPropertyModel propSchema_duration = pSemCmsModel.GetPropertyByPath("http://schema.org/duration");
			if (propSchema_duration != null && propSchema_duration.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_duration.PropertyValues)
				{
					this.Schema_duration.Add(GetNumberIntPropertyMultipleValueSemCms(propValue).Value);
				}
			}
			SemanticPropertyModel propSchema_inLanguage = pSemCmsModel.GetPropertyByPath("http://schema.org/inLanguage");
			this.Schema_inLanguage = new List<string>();
			if (propSchema_inLanguage != null && propSchema_inLanguage.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_inLanguage.PropertyValues)
				{
					this.Schema_inLanguage.Add(propValue.Value);
				}
			}
			SemanticPropertyModel propSchema_award = pSemCmsModel.GetPropertyByPath("http://schema.org/award");
			this.Schema_award = new List<string>();
			if (propSchema_award != null && propSchema_award.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSchema_award.PropertyValues)
				{
					this.Schema_award.Add(propValue.Value);
				}
			}
			this.Schema_description = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/description"));
			this.Schema_image = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/image"));
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
			this.Schema_datePublished= GetDateValuePropertySemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/datePublished")).Value;
			this.Schema_contentRating = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/contentRating"));
		}

		[LABEL(LanguageEnum.es,"http://schema.org/author")]
		[RDFProperty("http://schema.org/author")]
		public  List<Person> Schema_author { get; set;}
		public List<string> IdsSchema_author { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/rating")]
		[RDFProperty("http://schema.org/rating")]
		public  List<Rating> Schema_rating { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/director")]
		[RDFProperty("http://schema.org/director")]
		public  List<Person> Schema_director { get; set;}
		public List<string> IdsSchema_director { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/actor")]
		[RDFProperty("http://schema.org/actor")]
		public  List<Person> Schema_actor { get; set;}
		public List<string> IdsSchema_actor { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/genre")]
		[RDFProperty("http://schema.org/genre")]
		public  List<string> Schema_genre { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/url")]
		[RDFProperty("http://schema.org/url")]
		public  List<string> Schema_url { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/aggregateRating")]
		[RDFProperty("http://schema.org/aggregateRating")]
		public  List<string> Schema_aggregateRating { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/productionCompany")]
		[RDFProperty("http://schema.org/productionCompany")]
		public  List<string> Schema_productionCompany { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/recordedAt")]
		[RDFProperty("http://schema.org/recordedAt")]
		public  List<int> Schema_recordedAt { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/countryOfOrigin")]
		[RDFProperty("http://schema.org/countryOfOrigin")]
		public  List<string> Schema_countryOfOrigin { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/duration")]
		[RDFProperty("http://schema.org/duration")]
		public  List<int> Schema_duration { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/inLanguage")]
		[RDFProperty("http://schema.org/inLanguage")]
		public  List<string> Schema_inLanguage { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/award")]
		[RDFProperty("http://schema.org/award")]
		public  List<string> Schema_award { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/description")]
		[RDFProperty("http://schema.org/description")]
		public  string Schema_description { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/image")]
		[RDFProperty("http://schema.org/image")]
		public  string Schema_image { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/name")]
		[RDFProperty("http://schema.org/name")]
		public  string Schema_name { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/datePublished")]
		[RDFProperty("http://schema.org/datePublished")]
		public  DateTime Schema_datePublished { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/contentRating")]
		[RDFProperty("http://schema.org/contentRating")]
		public  string Schema_contentRating { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new ListStringOntologyProperty("schema:author", this.IdsSchema_author));
			propList.Add(new ListStringOntologyProperty("schema:director", this.IdsSchema_director));
			propList.Add(new ListStringOntologyProperty("schema:actor", this.IdsSchema_actor));
			propList.Add(new ListStringOntologyProperty("schema:genre", this.Schema_genre));
			propList.Add(new ListStringOntologyProperty("schema:url", this.Schema_url));
			propList.Add(new ListStringOntologyProperty("schema:aggregateRating", this.Schema_aggregateRating));
			propList.Add(new ListStringOntologyProperty("schema:productionCompany", this.Schema_productionCompany));
			List<string> Schema_recordedAtString = new List<string>();
			Schema_recordedAtString.AddRange(Array.ConvertAll(this.Schema_recordedAt.ToArray() , element => element.ToString()));
			propList.Add(new ListStringOntologyProperty("schema:recordedAt", Schema_recordedAtString));
			propList.Add(new ListStringOntologyProperty("schema:countryOfOrigin", this.Schema_countryOfOrigin));
			List<string> Schema_durationString = new List<string>();
			if (Schema_duration != null)
			{
				Schema_durationString.AddRange(Array.ConvertAll(this.Schema_duration.ToArray(), element => element.ToString()));
				propList.Add(new ListStringOntologyProperty("schema:duration", Schema_durationString));
			}
			propList.Add(new ListStringOntologyProperty("schema:inLanguage", this.Schema_inLanguage));
			propList.Add(new ListStringOntologyProperty("schema:award", this.Schema_award));
			propList.Add(new StringOntologyProperty("schema:description", this.Schema_description));
			//propList.Add(new StringOntologyProperty("schema:image", this.Schema_image));
			propList.Add(new StringOntologyProperty("schema:name", this.Schema_name));
			propList.Add(new DateOntologyProperty("schema:datePublished", this.Schema_datePublished));
			propList.Add(new StringOntologyProperty("schema:contentRating", this.Schema_contentRating));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			entList = new List<OntologyEntity>();
			if(Schema_rating!=null){
				foreach(Rating prop in Schema_rating){
					prop.GetProperties();
					prop.GetEntities();
					OntologyEntity entityRating = new OntologyEntity("http://schema.org/Rating", "http://schema.org/Rating", "schema:rating", prop.propList, prop.entList);
				entList.Add(entityRating);
				prop.Entity= entityRating;
				}
			}
		} 
		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, listaDeCategorias, Guid.Empty, Guid.Empty);
		}

        internal override void AddImages(ComplexOntologyResource pResource)
        {
            base.AddImages(pResource);
            List<ImageAction> listAcciones = new List<ImageAction>();
            listAcciones.Add(new ImageAction(234, ImageTransformationType.Crop, 100));
            listAcciones.Add(new ImageAction(318, ImageTransformationType.ResizeToWidth, 100));
			try
			{
				pResource.AttachImage(this.Schema_image, listAcciones, "schema:image", true, "jpg");
			}
			catch(Exception ex)
            {
				pResource.AttachImage("https://image.freepik.com/vector-gratis/tira-pelicula-icono-herramientas-cinematografia_24640-18839.jpg", listAcciones, "schema:image", true, "jpg");
			}
        }


        public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias, Guid idrecurso, Guid idarticulo)
		{
			ComplexOntologyResource resource = new ComplexOntologyResource();
			Ontology ontology=null;
			GetEntities();
			GetProperties();
			if(idrecurso.Equals(Guid.Empty) && idarticulo.Equals(Guid.Empty))
			{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, "http://schema.org/Movie", "http://schema.org/Movie", prefList, propList, entList);
			}
			else{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, "http://schema.org/Movie", "http://schema.org/Movie", prefList, propList, entList,idrecurso,idarticulo);
			}
			resource.Id = GNOSSID;
			resource.Ontology = ontology;
			resource.TextCategories=listaDeCategorias;
			AddResourceTitle(resource);
			AddResourceDescription(resource);
			AddImages(resource);
			AddFiles(resource);
			return resource;
		}

		public override List<string> ToOntologyGnossTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/Movie> . ");
			list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://www.w3.org/2000/01/rdf-schema#label> \"http://schema.org/Movie\" . ");
			list.Add($"<{resourceAPI.GraphsUrl}{ResourceID}> <http://gnoss/hasEntidad> <{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> . ");
			foreach(var item in this.Schema_rating)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/rating> <{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> . ");
				list.Add($"<{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/Rating> . ");
				list.Add($"<{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> <http://www.w3.org/2000/01/rdf-schema#label> \"http://schema.org/Rating\" . ");
				list.Add($"<{resourceAPI.GraphsUrl}{ResourceID}> <http://gnoss/hasEntidad> <{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> . ");
				if(item.Schema_ratingSource != null)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> <http://schema.org/ratingSource> \"{item.Schema_ratingSource.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
				if(item.Schema_ratingValue != null)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/Rating_{ResourceID}_{item.ArticleID}> <http://schema.org/ratingValue> {item.Schema_ratingValue.ToString()} . ");
				}
			}
			if(this.Schema_author != null)
			{
				foreach(var item in this.Schema_author)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/author> <{item}> . ");
				}
			}
			if(this.Schema_director != null)
			{
				foreach(var item in this.Schema_director)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/director> <{item}> . ");
				}
			}
			if(this.Schema_actor != null)
			{
				foreach(var item in this.Schema_actor)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/actor> <{item}> . ");
				}
			}
			if(this.Schema_genre != null)
			{
				foreach(var item in this.Schema_genre)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/genre> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_url != null)
			{
				foreach(var item in this.Schema_url)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/url> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_aggregateRating != null)
			{
				foreach(var item in this.Schema_aggregateRating)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/aggregateRating> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_productionCompany != null)
			{
				foreach(var item in this.Schema_productionCompany)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/productionCompany> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_recordedAt != null)
			{
				foreach(var item in this.Schema_recordedAt)
				{
					//list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/recordedAt> {item.Value.ToString()} . ");
                    list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/recordedAt> {item.ToString()} . ");
                }
			}
			if(this.Schema_countryOfOrigin != null)
			{
				foreach(var item in this.Schema_countryOfOrigin)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/countryOfOrigin> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_duration != null)
			{
				foreach(var item in this.Schema_duration)
				{
					//list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/duration> {item.Value.ToString()} . ");
                    list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/duration> {item.ToString()} . ");

                }
            }
			if(this.Schema_inLanguage != null)
			{
				foreach(var item in this.Schema_inLanguage)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/inLanguage> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_award != null)
			{
				foreach(var item in this.Schema_award)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/award> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_description != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/description> \"{this.Schema_description.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_image != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/image> \"{this.Schema_image.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_name != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/name> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_datePublished != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/datePublished> \"{this.Schema_datePublished.ToString("yyyyMMddHHmmss")}\" . ");
			}
			if(this.Schema_contentRating != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/contentRating> \"{this.Schema_contentRating.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			return list;
		}

		public override List<string> ToSearchGraphTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			list.Add($"<http://gnoss/{ResourceID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> \"movie\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/type> \"http://schema.org/Movie\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasfechapublicacion> 20201203112307 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hastipodoc> \"5\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasfechamodificacion> 20201203112307 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasnumeroVisitas>  0 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasprivacidadCom> \"publico\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://xmlns.com/foaf/0.1/firstName> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasnombrecompleto> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			string search = $"\"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}";
			if(!string.IsNullOrEmpty(this.Schema_description))
			{
				search += $"{this.Schema_description.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}";
				list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/search> {search}\" . ");
				}
			foreach(var item in this.Schema_rating)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}> <http://schema.org/rating> <{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> . ");
				list.Add($"<{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/Rating> . ");
				list.Add($"<{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> <http://www.w3.org/2000/01/rdf-schema#label> \"http://schema.org/Rating\" . ");
				list.Add($"<{resourceAPI.GraphsUrl}{ResourceID}> <http://gnoss/hasEntidad> <{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> . ");
				if(item.Schema_ratingSource != null)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> <http://schema.org/ratingSource> \"{item.Schema_ratingSource.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
				if(item.Schema_ratingValue != null)
				{
					list.Add($"<{resourceAPI.GraphsUrl}items/rating_{ResourceID}_{item.ArticleID}> <http://schema.org/ratingValue> {item.Schema_ratingValue.ToString()} . ");
				}
			}
			if(this.Schema_author != null)
			{
				foreach(var item in this.Schema_author)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/author> <{item}> . ");
				}
			}
			if(this.Schema_director != null)
			{
				foreach(var item in this.Schema_director)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/director> <{item}> . ");
				}
			}
			if(this.Schema_actor != null)
			{
				foreach(var item in this.Schema_actor)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/actor> <{item}> . ");
				}
			}
			if(this.Schema_genre != null)
			{
				foreach(var item in this.Schema_genre)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/genre> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_url != null)
			{
				foreach(var item in this.Schema_url)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/url> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_aggregateRating != null)
			{
				foreach(var item in this.Schema_aggregateRating)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/aggregaterating> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_productionCompany != null)
			{
				foreach(var item in this.Schema_productionCompany)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/productioncompany> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_recordedAt != null)
			{
				foreach(var item in this.Schema_recordedAt)
				{
                   // list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/recordedat> {item.Value.ToString()} . ");
                    list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/recordedat> {item.ToString()} . ");
				}
			}
			if(this.Schema_countryOfOrigin != null)
			{
				foreach(var item in this.Schema_countryOfOrigin)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/countryoforigin> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_duration != null)
			{
				foreach(var item in this.Schema_duration)
				{
					//list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/duration> {item.Value.ToString()} . ");
                    list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/duration> {item.ToString()} . ");
                }
			}
			if(this.Schema_inLanguage != null)
			{
				foreach(var item in this.Schema_inLanguage)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/inlanguage> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_award != null)
			{
				foreach(var item in this.Schema_award)
				{
					list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/award> \"{item.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
				}
			}
			if(this.Schema_description != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/description> \"{this.Schema_description.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_image != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/image> \"{this.Schema_image.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_name != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/name> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			if(this.Schema_datePublished != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/datepublished> \"{this.Schema_datePublished.ToString("yyyyMMddHHmmss")}\" . ");
			}
			if(this.Schema_contentRating != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/contentrating> \"{this.Schema_contentRating.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			return list;
		}

		public override KeyValuePair<Guid, string> ToAcidData(ResourceApi resourceAPI)
		{

			//Insert en la tabla Documento
			string tablaDoc = $"'{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"").Replace("'", "''")}', '{this.Schema_description.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"").Replace("'", "''")}', '{resourceAPI.GraphsUrl}'";
			KeyValuePair<Guid, string> valor = new KeyValuePair<Guid, string>(ResourceID, tablaDoc);

			return valor;
		}

		public override string GetURI(ResourceApi resourceAPI)
		{
			return $"{resourceAPI.GraphsUrl}items/PeliculaOntology_{ResourceID}_{ArticleID}";
		}

		internal void AddResourceTitle(ComplexOntologyResource resource)
		{
			resource.Title = this.Schema_name;
		}

		internal void AddResourceDescription(ComplexOntologyResource resource)
		{
			resource.Description = this.Schema_description;
		}


	}
}
