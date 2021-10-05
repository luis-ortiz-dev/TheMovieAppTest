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
        public ObservableCollection<MovieResumeModel> AuxTopRatedList { get; set; }
        public ObservableCollection<MovieResumeModel> AuxUpcomingList { get; set; }
        public ObservableCollection<MovieResumeModel> AuxPopularList { get; set; }

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
            AuxTopRatedList = new ObservableCollection<MovieResumeModel>();
            AuxUpcomingList = new ObservableCollection<MovieResumeModel>();
            AuxPopularList = new ObservableCollection<MovieResumeModel>();

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
            AuxTopRatedList = new ObservableCollection<MovieResumeModel>(Response.Results);
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
            AuxUpcomingList = new ObservableCollection<MovieResumeModel>(Response.Results);
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
            AuxPopularList = new ObservableCollection<MovieResumeModel>(Response.Results);
            OnPropertyChanged(nameof(PopularMoviesList));
        }

        private async Task SelectionChangedTopRatedCommandExecute()
        {
            Application.Current.Properties["movieId"] = SelectedItemTopRated.Id;

            MovieDetailsPage newPage = new MovieDetailsPage();
            await Navigation.PushAsync(newPage);
            //await Shell.Current.GoToAsync(nameof(MovieDetailsPage));
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
                TopRatedMoviesList = new ObservableCollection<MovieResumeModel>(AuxTopRatedList);
                UpcomingMoviesList = new ObservableCollection<MovieResumeModel>(AuxUpcomingList);
                PopularMoviesList = new ObservableCollection<MovieResumeModel>(AuxPopularList);
                return;
            }

            text = text.ToLower();

            List<MovieResumeModel> newList1 = new List<MovieResumeModel>();
            List<MovieResumeModel> newList2 = new List<MovieResumeModel>();
            List<MovieResumeModel> newList3 = new List<MovieResumeModel>();

            foreach (MovieResumeModel Resume in AuxTopRatedList)
            {
                if (Resume.Title.ToLower().Contains(text))
                {
                    newList1.Add(Resume);
                }
            }
            
            foreach (MovieResumeModel Resume in AuxUpcomingList)
            {
                if (Resume.Title.ToLower().Contains(text))
                {
                    newList2.Add(Resume);
                }
            }
            
            foreach (MovieResumeModel Resume in AuxPopularList)
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
