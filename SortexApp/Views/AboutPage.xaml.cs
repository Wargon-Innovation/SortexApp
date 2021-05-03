using SortexApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SortexApp.Views
{
    public partial class AboutPage : ContentPage
    {
        static public OrderViewModel Order { get; set; } = new OrderViewModel();
        public AboutPage()
        {
            InitializeComponent();
            
        }

        private void btnFraction_Clicked(object sender, EventArgs e)
        {
            
        }

        private async void btnOrder_Clicked(object sender, EventArgs e)
        {
            
            
            OrderPage orderPage = new OrderPage();
            
            await Navigation.PushAsync(orderPage);
            
           
        }
    }
}