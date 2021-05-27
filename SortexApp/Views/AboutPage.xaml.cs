using SortexApp.Models;
using SortexApp.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
        protected override void OnAppearing()
        {
            base.OnAppearing();

            CheckLogin();
        }

        private async Task CheckLogin()
        {
            // should check for valid login instead
            await Task.Delay(2000);

            if (!App.isLogedIn)
            {
                // only open Login page when no valid login
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
               
            

        }

        private async void BtnFraction_Clicked(object sender, EventArgs e)
        {
            
            await App.Fraction.LoadFractionAsync();
            await Shell.Current.GoToAsync($"//{nameof(FractionPage)}");

        }

        private async void BtnOrder_Clicked(object sender, EventArgs e)
        {
           
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

        private async void BtnMoodboard_Clicked(object sender, EventArgs e)
        {
            await App.Moodboard.LoadMoodboardAsync();
            await Shell.Current.GoToAsync($"//{nameof(MoodboardPage)}");
        }

        private async void BtnBrands_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(BrandPage)}");
        }

        private async void BtnAssignment_Clicked(object sender, EventArgs e)
        {
            await App.Assignment.LoardAssignmentAsync();
            await Shell.Current.GoToAsync($"//{nameof(AssignmentPage)}");
        }
    }
}