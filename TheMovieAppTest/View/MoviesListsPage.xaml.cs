using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieAppTest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieAppTest.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesListsPage : ContentPage
    {
        MoviesListsVM vm;
        public MoviesListsPage()
        {
            InitializeComponent();
            vm = new MoviesListsVM(Navigation);
            BindingContext = vm;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            string text = searchBar.Text;
            vm.SearchList(text);
        }
    }
}