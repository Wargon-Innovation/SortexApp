using SortexApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SortexApp.Views
{
    public partial class AboutPage : ContentPage
    {
        //static public OrderViewModel Order { get; set; } = new OrderViewModel();
        public AboutPage()
        {
            InitializeComponent();
            Title = "Hem";
        }

        private async void BtnFraction_Clicked(object sender, EventArgs e)
        {
            //FractionPage fractionPage = new FractionPage();
            //await Navigation.PushAsync(fractionPage);
            await Shell.Current.GoToAsync($"//{nameof(FractionPage)}");

        }

        private async void BtnOrder_Clicked(object sender, EventArgs e)
        {

            //OrderPage orderPage = new OrderPage();            
            //await Navigation.PushAsync(orderPage);
            await App.Order.LoadOrderAsync();
            await Shell.Current.GoToAsync($"//{nameof(OrderPage)}");

        }
    }
}