using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheMovieAppTest.Model
{
    class MovieCreditsModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

    }

    class CastDetailModel
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }
}
