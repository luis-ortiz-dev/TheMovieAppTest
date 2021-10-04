using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TheMovieAppTest.Model;

namespace TheMovieAppTest.Service
{
    interface IApiTMDB
    {
        Task<MovieListModel> getMovieList(string list_name);

        Task<MovieDetailsModel> getMovieDetails(string movie_id);

        Task<MovieCreditsModel> getMovieCredits(string movie_id);
    }
}
