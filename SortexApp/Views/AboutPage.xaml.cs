using SortexApp.Models;
using SortexApp.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SortexApp.Views
{
    public partial class AboutPage : ContentPage
    {

        public int trendCount = App.Trend.TrendList.Count();
        public AboutPage()
        {
            InitializeComponent();
            Title = "Hem";
        }

        private async void BtnFraction_Clicked(object sender, EventArgs e)
        {
            
            await App.Fraction.LoadFractionAsync();
            await Shell.Current.GoToAsync($"//{nameof(FractionPage)}");

        }

        private async void BtnOrder_Clicked(object sender, EventArgs e)
        {
            trendCount = App.Trend.TrendList.Count();
            
            await App.Order.LoadOrderAsync();
            await Shell.Current.GoToAsync($"//{nameof(OrderPage)}");
            
        }

        private async void BtnTrend_Clicked(object sender, EventArgs e)
        {
                    trendCount = App.Trend.TrendList.Count();

                    if(App.Trend.TrendList.Count() != trendCount)
                    {
                           await App.Trend.LoadTrendAsync();
                    }
               
                await Shell.Current.GoToAsync($"//{nameof(TrendPage)}");
        }
    }
}