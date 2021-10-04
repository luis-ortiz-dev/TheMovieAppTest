using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TheMovieAppTest.Model
{
    class MovieListModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        
        [JsonProperty("results")]
        public ObservableCollection<MovieResumeModel> Results { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
