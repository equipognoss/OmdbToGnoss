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

namespace PeliculaOntology
{
	public class Rating : GnossOCBase
	{

		public Rating() : base() { } 

		public Rating(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			this.mGNOSSID = pSemCmsModel.Entity.Uri;
			this.mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			this.Schema_ratingSource = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/ratingSource"));
			this.Schema_ratingValue = GetNumberIntPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/ratingValue")).Value;
		}

		public OntologyEntity Entity { get; set; }

		[LABEL(LanguageEnum.es,"http://schema.org/ratingSource")]
		[RDFProperty("http://schema.org/ratingSource")]
		public  string Schema_ratingSource { get; set;}

		[LABEL(LanguageEnum.es,"http://schema.org/ratingValue")]
		[RDFProperty("http://schema.org/ratingValue")]
		public  int Schema_ratingValue { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new StringOntologyProperty("schema:ratingSource", this.Schema_ratingSource));
			propList.Add(new StringOntologyProperty("schema:ratingValue", this.Schema_ratingValue.ToString()));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			entList = new List<OntologyEntity>();
		} 







		internal override void AddImages(ComplexOntologyResource pResource)
		{
			base.AddImages(pResource);
		}

	}
}
