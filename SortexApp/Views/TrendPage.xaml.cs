using SortexApp.Models;
using SortexApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SortexApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrendPage : ContentPage
    {
        public TrendPage()
        {
            InitializeComponent();
            BindingContext = App.Trend;
        }

        private void lstTrendImages_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as TrendViewModel;
            var trend = e.Item as TrendImageView;
            vm.HideOrShowTrends(trend);
        }

        private void btnTrendDetails_Clicked(object sender, EventArgs e)
        {

        }
    }
}
//await Shell.Current.GoToAsync($"//{nameof(TrendDetailsPage)}");