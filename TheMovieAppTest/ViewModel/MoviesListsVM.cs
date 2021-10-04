using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TheMovieAppTest.Model;
using TheMovieAppTest.Service;
using Xamarin.Forms;

namespace TheMovieAppTest.ViewModel
{
    class MoviesListsVM
    {
        private readonly IApiTMDB ApiService;
        private readonly INavigation Navigation;

        public Command GetTopRatedMoviesListCommand { get; set; }
        public Command GetUpcomingMoviesListCommand { get; set; }
        public Command GetPopularMoviesListCommand { get; set; }
        public Command SelectionChangedTopCommand { get; set; }
        public Command SelectionChangedUpcomingCommand { get; set; }
        public Command SelectionChangedPopularCommand { get; set; }

        public ObservableCollection<MovieResumeModel> TopRatedMoviesList { get; set; }
        public ObservableCollection<MovieResumeModel> UpcomingMoviesList { get; set; }
        public ObservableCollection<MovieResumeModel> PopularMoviesList { get; set; }

        public MovieResumeModel SelectedItemTop { get; set; }
        public MovieResumeModel SelectedItemUpcoming { get; set; }
        public MovieResumeModel SelectedItemPopular { get; set; }
        
        string image_url = "http://image.tmdb.org/t/p/w92/";

        public MoviesListsVM( IApiTMDB apiTMDB, INavigation navigation)
        {
            ApiService = apiTMDB;
            Navigation = navigation;

            InitCommands();

            TopRatedMoviesList = new ObservableCollection<MovieResumeModel>();
            UpcomingMoviesList = new ObservableCollection<MovieResumeModel>();
            PopularMoviesList = new ObservableCollection<MovieResumeModel>();
        }

        public void InitCommands()
        {
            GetTopRatedMoviesListCommand = new Command(async () => await GetTopRatedMoviesListCommandExecute());
            SelectionChangedTopCommand = new Command(async () => await SelectionChangedCommandTopExecute());
        }
                
        private async Task GetTopRatedMoviesListCommandExecute()
        {
            MovieListModel Response = await ApiService.getMovieList("top_rated");

            foreach(MovieResumeModel Resume in Response.Results)
            {
                Resume.PosterPath = image_url + Resume.PosterPath;
            }

            TopRatedMoviesList = Response.Results;
        }

        private async Task SelectionChangedCommandTopExecute()
        {
            Application.Current.Properties["listName"] = SelectedItemTop.Id;

            await Navigation.PushAsync(new MainPage());
        }

        public void SearchList(string text)
        {

        }

    }
}
