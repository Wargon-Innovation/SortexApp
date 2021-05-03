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

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Order.LoadOrderAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
