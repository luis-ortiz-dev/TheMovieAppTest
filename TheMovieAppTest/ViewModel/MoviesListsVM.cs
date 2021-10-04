using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TheMovieAppTest.Model;
using TheMovieAppTest.Service;
using TheMovieAppTest.View;
using Xamarin.Forms;

namespace TheMovieAppTest.ViewModel
{
    class MoviesListsVM : INotifyPropertyChanged
    {
        private IApiTMDB ApiService => DependencyService.Get<IApiTMDB>();
        private readonly INavigation Navigation;

        public Command GetTopRatedMoviesListCommand { get; set; }
        public Command GetUpcomingMoviesListCommand { get; set; }
        public Command GetPopularMoviesListCommand { get; set; }
        public Command SelectionChangedTopRatedCommand { get; set; }
        public Command SelectionChangedUpcomingCommand { get; set; }
        public Command SelectionChangedPopularCommand { get; set; }

        public ObservableCollection<MovieResumeModel> TopRatedMoviesList { get; set; }
        public ObservableCollection<MovieResumeModel> UpcomingMoviesList { get; set; }
        public ObservableCollection<MovieResumeModel> PopularMoviesList { get; set; }
        public ObservableCollection<MovieResumeModel> auxTopRatedList { get; set; }
        public ObservableCollection<MovieResumeModel> auxUpcomingList { get; set; }
        public ObservableCollection<MovieResumeModel> auxPopularList { get; set; }

        public MovieResumeModel SelectedItemTopRated { get; set; }
        public MovieResumeModel SelectedItemUpcoming { get; set; }
        public MovieResumeModel SelectedItemPopular { get; set; }
        
        string image_url = "http://image.tmdb.org/t/p/w342/";

        public MoviesListsVM(INavigation navigation)
        {
            Navigation = navigation;

            InitCommands();

            TopRatedMoviesList = new ObservableCollection<MovieResumeModel>();
            UpcomingMoviesList = new ObservableCollection<MovieResumeModel>();
            PopularMoviesList = new ObservableCollection<MovieResumeModel>();
            auxTopRatedList = new ObservableCollection<MovieResumeModel>();
            auxUpcomingList = new ObservableCollection<MovieResumeModel>();
            auxPopularList = new ObservableCollection<MovieResumeModel>();

            GetTopRatedMoviesListCommand.Execute(null);
            GetUpcomingMoviesListCommand.Execute(null);
            GetPopularMoviesListCommand.Execute(null);
        }

        public void InitCommands()
        {
            GetTopRatedMoviesListCommand = new Command(async () => await GetTopRatedMoviesListCommandExecute());
            GetUpcomingMoviesListCommand = new Command(async () => await GetUpcomingMoviesListCommandExecute());
            GetPopularMoviesListCommand = new Command(async () => await GetPopularMoviesListCommandExecute());
            SelectionChangedTopRatedCommand = new Command(async () => await SelectionChangedTopRatedCommandExecute());
            SelectionChangedUpcomingCommand = new Command(async () => await SelectionChangedUpcomingCommandExecute());
            SelectionChangedPopularCommand = new Command(async () => await SelectionChangedPopularCommandExecute());
        }
                
        private async Task GetTopRatedMoviesListCommandExecute()
        {
            MovieListModel Response = await ApiService.getMovieList("top_rated");

            foreach(MovieResumeModel Resume in Response.Results)
            {
                Resume.PosterPath = image_url + Resume.PosterPath;
            }

            TopRatedMoviesList = new ObservableCollection<MovieResumeModel>(Response.Results);
            auxTopRatedList = new ObservableCollection<MovieResumeModel>(Response.Results);
            OnPropertyChanged(nameof(TopRatedMoviesList));
        }

        private async Task GetUpcomingMoviesListCommandExecute()
        {
            MovieListModel Response = await ApiService.getMovieList("upcoming");

            foreach (MovieResumeModel Resume in Response.Results)
            {
                Resume.PosterPath = image_url + Resume.PosterPath;
            }

            UpcomingMoviesList = new ObservableCollection<MovieResumeModel>(Response.Results);
            auxUpcomingList = new ObservableCollection<MovieResumeModel>(Response.Results);
            OnPropertyChanged(nameof(UpcomingMoviesList));
        }

        private async Task GetPopularMoviesListCommandExecute()
        {
            MovieListModel Response = await ApiService.getMovieList("popular");

            foreach (MovieResumeModel Resume in Response.Results)
            {
                Resume.PosterPath = image_url + Resume.PosterPath;
            }

            PopularMoviesList = new ObservableCollection<MovieResumeModel>(Response.Results);
            auxPopularList = new ObservableCollection<MovieResumeModel>(Response.Results);
            OnPropertyChanged(nameof(PopularMoviesList));
        }

        private async Task SelectionChangedTopRatedCommandExecute()
        {
            Application.Current.Properties["movieId"] = SelectedItemTopRated.Id;

            await Navigation.PushAsync(new MovieDetailsPage());
        }

        private async Task SelectionChangedUpcomingCommandExecute()
        {
            Application.Current.Properties["movieId"] = SelectedItemUpcoming.Id;

            await Navigation.PushAsync(new MovieDetailsPage());
        }

        private async Task SelectionChangedPopularCommandExecute()
        {
            Application.Current.Properties["movieId"] = SelectedItemPopular.Id;

            await Navigation.PushAsync(new MovieDetailsPage());
        }

        public void SearchList(string text)
        {
            if(text.Equals(null))
            {
                TopRatedMoviesList = new ObservableCollection<MovieResumeModel>(auxTopRatedList);
                UpcomingMoviesList = new ObservableCollection<MovieResumeModel>(auxUpcomingList);
                PopularMoviesList = new ObservableCollection<MovieResumeModel>(auxPopularList);
                return;
            }

            text = text.ToLower();

            List<MovieResumeModel> newList1 = new List<MovieResumeModel>();
            List<MovieResumeModel> newList2 = new List<MovieResumeModel>();
            List<MovieResumeModel> newList3 = new List<MovieResumeModel>();

            foreach (MovieResumeModel Resume in auxTopRatedList)
            {
                if (Resume.Title.ToLower().Contains(text))
                {
                    newList1.Add(Resume);
                }
            }
            
            foreach (MovieResumeModel Resume in auxUpcomingList)
            {
                if (Resume.Title.ToLower().Contains(text))
                {
                    newList2.Add(Resume);
                }
            }
            
            foreach (MovieResumeModel Resume in auxPopularList)
            {
                if (Resume.Title.ToLower().Contains(text))
                {
                    newList3.Add(Resume);
                }
            }

            TopRatedMoviesList = new ObservableCollection<MovieResumeModel>(newList1);
            UpcomingMoviesList = new ObservableCollection<MovieResumeModel>(newList2);
            PopularMoviesList = new ObservableCollection<MovieResumeModel>(newList3);
            OnPropertyChanged(nameof(TopRatedMoviesList));
            OnPropertyChanged(nameof(UpcomingMoviesList));
            OnPropertyChanged(nameof(PopularMoviesList));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
