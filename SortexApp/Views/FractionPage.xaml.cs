using SortexApp.Models;
using SortexApp.ViewModels;
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
    public partial class FractionPage : ContentPage
    {
        public FractionPage()
        {
            InitializeComponent();
            BindingContext = App.Fraction;
            Title = "Fraktioner";
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as FractionViewModel;
            var fraction = e.Item as Fraction;
            vm.HideOrShowFractions(fraction);
        }
    }
}