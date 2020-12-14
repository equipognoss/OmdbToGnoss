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

namespace PersonaOntology
{
	public class Person : GnossOCBase
	{

		public Person() : base() { } 

		public Person(SemanticResourceModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			this.mGNOSSID = pSemCmsModel.RootEntities[0].Entity.Uri;
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
		}

		public Person(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			this.mGNOSSID = pSemCmsModel.Entity.Uri;
			this.mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
		}

		[RDFProperty("http://schema.org/name")]
		public  string Schema_name { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new StringOntologyProperty("schema:name", this.Schema_name));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			entList = new List<OntologyEntity>();
		} 
		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, listaDeCategorias, Guid.Empty, Guid.Empty);
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias, Guid idrecurso, Guid idarticulo)
		{
			ComplexOntologyResource resource = new ComplexOntologyResource();
			Ontology ontology=null;
			GetProperties();
			if(idrecurso.Equals(Guid.Empty) && idarticulo.Equals(Guid.Empty))
			{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, "http://schema.org/Person", "http://schema.org/Person", prefList, propList, entList);
			}
			else{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, "http://schema.org/Person", "http://schema.org/Person", prefList, propList, entList,idrecurso,idarticulo);
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
			list.Add($"<{resourceAPI.GraphsUrl}items/PersonaOntology_{ResourceID}_{ArticleID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/Person> . ");
			list.Add($"<{resourceAPI.GraphsUrl}items/PersonaOntology_{ResourceID}_{ArticleID}> <http://www.w3.org/2000/01/rdf-schema#label> \"http://schema.org/Person\" . ");
			list.Add($"<{resourceAPI.GraphsUrl}{ResourceID}> <http://gnoss/hasEntidad> <{resourceAPI.GraphsUrl}items/PersonaOntology_{ResourceID}_{ArticleID}> . ");
			if(this.Schema_name != null)
			{
				list.Add($"<{resourceAPI.GraphsUrl}items/PersonaOntology_{ResourceID}_{ArticleID}> <http://schema.org/name> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			return list;
		}

		public override List<string> ToSearchGraphTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			list.Add($"<http://gnoss/{ResourceID}> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> \"person\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/type> \"http://schema.org/Person\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasfechapublicacion> 20201203112307 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hastipodoc> \"5\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasfechamodificacion> 20201203112307 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasnumeroVisitas>  0 . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasprivacidadCom> \"publico\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://xmlns.com/foaf/0.1/firstName> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/hasnombrecompleto> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			string search = $"\"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}";
			if(!string.IsNullOrEmpty(this.Schema_name))
			{
				search += $"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}";
				list.Add($"<http://gnoss/{ResourceID}> <http://gnoss/search> {search}\" . ");
				}
			if(this.Schema_name != null)
			{
				list.Add($"<http://gnoss/{ResourceID}> <http://schema.org/name> \"{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"")}\" . ");
			}
			return list;
		}

		public override KeyValuePair<Guid, string> ToAcidData(ResourceApi resourceAPI)
		{

			//Insert en la tabla Documento
			string tablaDoc = $"'{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"").Replace("'", "''")}', '{this.Schema_name.Replace("\r\n", " ").Replace("\n", " ").Replace("\"", "\"\"").Replace("'", "''")}', '{resourceAPI.GraphsUrl}'";
			KeyValuePair<Guid, string> valor = new KeyValuePair<Guid, string>(ResourceID, tablaDoc);

			return valor;
		}

		public override string GetURI(ResourceApi resourceAPI)
		{
			return $"{resourceAPI.GraphsUrl}items/PersonaOntology_{ResourceID}_{ArticleID}";
		}

		internal void AddResourceTitle(ComplexOntologyResource resource)
		{
			resource.Title = this.Schema_name;
		}

		internal void AddResourceDescription(ComplexOntologyResource resource)
		{
			resource.Description = this.Schema_name;
		}

		internal override void AddImages(ComplexOntologyResource pResource)
		{
			base.AddImages(pResource);
		}

	}
}
