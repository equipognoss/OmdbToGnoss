using Newtonsoft.Json;
using OmdbToGnoss.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Services
{
    public class OmdbService
    {
        private string _apiKey;
        
        public OmdbService(string apiKey)
        {
            _apiKey = apiKey;
        }

        private string DownloadContentFromOmdbAPI(string omdb_object_id, string parameters = "")
        {
            string dataFile = $@"..\..\Data\{omdb_object_id}{parameters.Replace("=", "_")}.json";
            string content = "";
            if (File.Exists(dataFile))
            {
                content = File.ReadAllText(dataFile);
            }
            else
            {
                WebClient clientWeb = new WebClient();
                string url = $"{Constants.Services.URL_OMDB_API}?apikey={_apiKey}&i={omdb_object_id}";
                if (!string.IsNullOrEmpty(parameters))
                {
                    url += $"&{parameters.Trim('&')}";
                }

                content = clientWeb.DownloadString(url);
                File.WriteAllText(dataFile, content);
            }
            return content;
        }

        public Movie DownloadMovie(string id)
        {
            return JsonConvert.DeserializeObject<Movie>(DownloadContentFromOmdbAPI(id));
        }

        public TVSeries DownloadTVSerie(string id)
        {
            TVSeries serie = JsonConvert.DeserializeObject<TVSeries>(DownloadContentFromOmdbAPI(id));

            DownloadTVSerieSeasons(serie, id);

            return serie;
        }

        private void DownloadTVSerieSeasons(TVSeries serie, string serieId)
        {
            for (int i = 1; i <= serie.totalSeasons; i++)
            {
                string parameters = $"Season={i}";
                TVSeriesSeason season = JsonConvert.DeserializeObject<TVSeriesSeason>(DownloadContentFromOmdbAPI(serieId, parameters));

                // The information of each episode is not complete. We need to download the complete episodes information
                DownloadTVSerieSeasonEpisodes(season);

                serie.Seasons.Add(season);
            }
        }

        private void DownloadTVSerieSeasonEpisodes(TVSeriesSeason season)
        {
            List<TVSeriesEpisode> completeEpisodes = new List<TVSeriesEpisode>();

            if (season.Episodes != null)
            {
                foreach (TVSeriesEpisode episode in season.Episodes)
                {
                    TVSeriesEpisode completeEpisode = JsonConvert.DeserializeObject<TVSeriesEpisode>(DownloadContentFromOmdbAPI(episode.imdbID));

                    completeEpisodes.Add(completeEpisode);
                }

                season.Episodes.Clear();
                season.Episodes = completeEpisodes;
            }
        }
    }
}
