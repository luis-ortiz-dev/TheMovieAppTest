using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheMovieAppTest.Model;

namespace TheMovieAppTest.Service
{
    class ApiTMDB : IApiTMDB
    {
        string base_url = "https://api.themoviedb.org/3/movie/";
        string image_url = "http://image.tmdb.org/t/p/";
        string api_key = "6341b9a2262c44a43467bc1f5e23bf5e";
        string size_backdrop = "w300";        
        string size_profile = "w45";

        public async Task<MovieCreditsModel> getMovieCredits(string movie_id)
        {
            try
            {
                string url = base_url + $"{movie_id}/credits?api_key={api_key}";

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<MovieCreditsModel>(result);
                    return json;
                }

                return null;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<MovieDetailsModel> getMovieDetails(string movie_id)
        {
            try
            {
                string url = base_url + $"{movie_id}?api_key={api_key}";

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<MovieDetailsModel>(result);
                    return json;
                }

                return null;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<MovieListModel> getMovieList(string list_name)
        {
            try
            {
                string url = base_url + $"{list_name}?api_key={api_key}";

                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<MovieListModel>(result);
                    return json;
                }

                return null;

            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
