using SortexApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SortexApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}