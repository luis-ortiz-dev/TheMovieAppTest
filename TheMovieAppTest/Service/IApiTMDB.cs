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
        Task<ObservableCollection<MovieListModel>> getMovieList(string list_name);

        Task<ObservableCollection<MovieDetailsModel>> getMovieDetails(string movie_id);

        Task<ObservableCollection<MovieCreditsModel>> GetMovieCredits(string movie_id);
    }
}
