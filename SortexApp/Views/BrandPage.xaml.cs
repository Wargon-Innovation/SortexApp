using SortexApp.Models;
using SortexApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SortexApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrandPage : ContentPage
    {
        public BrandView brandId = new BrandView();
        private bool isPlaceHolder;
        public BrandPage()
        {
            InitializeComponent();
            BindingContext = App.Brand;
            brandListView.ItemsSource = App.Brand.BrandViewList;
            Title = "Märken";

        }

        private void brandSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            brandListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                isPlaceHolder = false;
                brandListView.ItemsSource = App.Brand.BrandViewList;

            }
            else
            {
                isPlaceHolder = true;
                brandListView.ItemsSource = App.Brand.SearchBrand(e.NewTextValue.ToLower());
            }
            brandListView.EndRefresh();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as BrandViewModel;
            var order = e.Item as BrandView;
            if (isPlaceHolder)
            {
                vm.HideOrShowBrandPlaceHolder(order);
            }
            else
            {
                vm.HideOrShowBrand(order);
            }

            brandId = e.Item as BrandView;
        }
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }

        private async void btnImage_Clicked(object sender, EventArgs e)
        {
            BrandDetailsPage brandDetails = new BrandDetailsPage();
            brandDetails.BindingContext = brandId;
            await Navigation.PushAsync(brandDetails);
        }
    }
}
