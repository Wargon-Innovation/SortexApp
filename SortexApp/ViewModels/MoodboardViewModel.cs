using Newtonsoft.Json;
using SortexApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SortexApp.ViewModels
{
    public class MoodboardViewModel
    {
        public ObservableCollection<Moodboard> MoodboardList { get; set; } = new ObservableCollection<Moodboard>();

        public event PropertyChangedEventHandler PropertyChanged;
        internal async Task LoadMoodboardAsync()
        {
            try
            {
                //HÄMTA MOODBOARD
                string apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Modeboards";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    MoodboardList = JsonConvert.DeserializeObject<ObservableCollection<Moodboard>>(content);
                    RaisePropertyChanged("MoodboardList");
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
            MoodboardList = new ObservableCollection<Moodboard>(MoodboardList.OrderBy(i => i.Name).ToList());
        }

        private void RaisePropertyChanged(string propertyName)
        {
           if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
