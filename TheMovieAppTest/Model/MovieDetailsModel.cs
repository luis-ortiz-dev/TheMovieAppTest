using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheMovieAppTest.Model
{
    class MovieDetailsModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("genres")]
        public List<GenreModel> Genres { get; set; }

        [JsonProperty("production_companies")]
        public List<ProductionCompanyModel> ProductionCompanies { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("popularity")]
        public float Popularity { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }
    }

    class GenreModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    class ProductionCompanyModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }
    
        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }
}
