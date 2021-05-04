using SortexApp.Services;
using SortexApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SortexApp.ViewModels;

namespace SortexApp
{
    public partial class App : Application
    {
        static public OrderViewModel Order { get; set; } = new OrderViewModel();
        static public FractionViewModel Fraction { get; set; } = new FractionViewModel();

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Order.LoadOrderAsync();
            await Fraction.LoadFractionAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
