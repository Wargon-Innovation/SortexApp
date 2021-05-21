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
    public partial class TrendDetailsPage : ContentPage
    {
        public TrendDetailsPage()
        {
            InitializeComponent();
            BindingContext = App.Trend;
            FlowListView.Init();
        }

    }
}