using Newtonsoft.Json;
using SortexApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SortexApp.ViewModels
{
    public class TrendViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Trend> TrendList { get; set; } = new ObservableCollection<Trend>();

        

        internal async Task LoadTrendAsync()
        {
            try
            {
                string apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Trends";
                HttpClient htppClient = new HttpClient();

                HttpResponseMessage response = await htppClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TrendList = JsonConvert.DeserializeObject<ObservableCollection<Trend>>(content);
                    RaisePropertyChanged("TrendList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable", "Cancel");
                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable (" + ex.Message + ")", "Cancel");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
