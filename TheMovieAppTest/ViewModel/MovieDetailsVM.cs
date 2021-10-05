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
    class MovieDetailsVM
    {
        private IApiTMDB ApiService => DependencyService.Get<IApiTMDB>();
        private readonly INavigation Navigation;
        private readonly int MovieId;
        private readonly string Image_url = "http://image.tmdb.org/t/p/";
        public MovieDetailsModel MovieDetails;

        public Command GetMovieDetailsCommand;
        public Command GetMovieCreditsCommand;

        public ObservableCollection<CastDetailModel> MovieCredits;
        
        public MovieDetailsVM(INavigation navigation)
        {
            Navigation = navigation;
            var obj = Application.Current.Properties["movieId"];
            MovieId = int.Parse(obj.ToString());

            InitCommands();

            MovieCredits = new ObservableCollection<CastDetailModel>();

            GetMovieDetailsCommand.Execute(null);
        }

        public void InitCommands()
        {
            GetMovieDetailsCommand = new Command(async () => await GetMovieDetailsCommandExecute());
            GetMovieCreditsCommand = new Command(async () => await GetMovieCreditsCommandExecute());
        }

        private async Task GetMovieDetailsCommandExecute()
        {
            MovieDetailsModel Response = await ApiService.getMovieDetails(MovieId.ToString());

            MovieDetails = Response;
        }

        private async Task GetMovieCreditsCommandExecute()
        {
            MovieCreditsModel Response = await ApiService.getMovieCredits(MovieId.ToString());

            MovieCredits = new ObservableCollection<CastDetailModel>(Response.Cast);
        }
    }
}
