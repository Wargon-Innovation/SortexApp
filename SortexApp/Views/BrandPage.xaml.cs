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

        public BrandPage()
        {
            InitializeComponent();
            BindingContext = App.Brand;
            Title = "Märken";

        }

        private void brandSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            brandListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                brandListView.ItemsSource = App.Brand.BrandViewList;

            }
            else
            {

                var brand = (from tags in App.Brand.BrandTagList
                             join brandTagsMM in App.Brand.BrandTagMMList on tags.Id equals brandTagsMM.TagId
                             join brands in App.Brand.BrandViewList on brandTagsMM.BrandId equals brands.Id
                             where tags.Value.Contains(e.NewTextValue)
                             select brands).ToList();

                brandListView.ItemsSource = brand;


            }

            brandListView.EndRefresh();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as BrandViewModel;
            var order = e.Item as BrandView;
            vm.HideOrShowBrand(order);
        }
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }
    }
}
