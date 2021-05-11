using SortexApp.Models;
using SortexApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public TrendImageView trendId = new TrendImageView();
        public TrendPage()
        {
            InitializeComponent();
            BindingContext = App.Trend;
            Title = "Trender";
        }

        private void lstTrendImages_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as TrendViewModel;
            var trend = e.Item as TrendImageView;
            vm.HideOrShowTrends(trend);

            trendId = e.Item as TrendImageView;

            //var selectedItem = e.Item as TrendImageView;

            //if (selectedItem.Id == 2)
            //{
            //    await Shell.Current.GoToAsync($"//{nameof(TrendDetailsPage)}");
            //}

        }

        private async void BtnImage_Clicked(object sender, EventArgs e)
        {
            TrendDetailsPage trendPage = new TrendDetailsPage();
            
            trendPage.BindingContext = trendId;
            await Navigation.PushAsync(trendPage);

        }


    }
}
