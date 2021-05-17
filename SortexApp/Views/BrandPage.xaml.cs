using SortexApp.Models;
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
    public partial class BrandPage : ContentPage
    {
         
        public BrandPage()
        {
            InitializeComponent();
            BindingContext = App.Brand;
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
           
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
                brandListView.ItemsSource = App.Brand.BrandTagList.Where(i => i.Value.Contains(e.NewTextValue));
                foreach (var item in App.Brand.BrandTagMMList)
                {
                    if(item.BrandId == )
                }
                
            }

            brandListView.EndRefresh();
        }
    }
}