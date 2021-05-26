using DLToolkit.Forms.Controls;
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
    public partial class BrandDetailsPage : ContentPage
    {
        public BrandDetailsPage()
        {
            InitializeComponent();
            BindingContext = App.Brand;
            FlowListView.Init();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var brand = int.Parse(brandId.Text);

            var brandImage = (from rowsBrand in App.Brand.BrandViewList
                              where rowsBrand.Id == brand
                              select rowsBrand.brandImages).ToList();

            if (brandImage[0].Count == 0 || brandImage == null)
            {
                emptyImageText.IsVisible = true;
            }

        }
       
    }
}