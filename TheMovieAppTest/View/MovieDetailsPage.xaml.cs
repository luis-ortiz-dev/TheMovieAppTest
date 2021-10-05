using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TheMovieAppTest.ViewModel;

namespace TheMovieAppTest.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsPage : ContentPage
    {
        MovieDetailsVM vm;
        public MovieDetailsPage()
        {
            InitializeComponent();
            vm = new MovieDetailsVM(Navigation);
            BindingContext = vm;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Hola mundo");
        }
    }
}