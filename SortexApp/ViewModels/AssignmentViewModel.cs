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
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Assignment> AssignmentList { get; set; } = new ObservableCollection<Assignment>();

        public event PropertyChangedEventHandler PropertyChanged;

        internal async Task LoardAssignmentAsync()
        {

            try
            {
                string apiUrl = @"https://informatik13.ei.hv.se/SortexAPI/api/Assignments";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(new Uri(apiUrl));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    AssignmentList = JsonConvert.DeserializeObject<ObservableCollection<Assignment>>(content);
                    RaisePropertyChanged("AssignmentList");
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
            AssignmentList = new ObservableCollection<Assignment>(AssignmentList.OrderBy(i => i.Id).Reverse().ToList());
        }

        
        private void RaisePropertyChanged(string propertyname)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
