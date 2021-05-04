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
    public class FractionViewModel : INotifyPropertyChanged
    {

        private Fraction _oldfraction;
        public ObservableCollection<Fraction> FractionList { get; set; } = new ObservableCollection<Fraction>();
        internal async Task LoadFractionAsync()
        {
            try
            {
                string apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Fractions";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(new Uri (apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    FractionList = JsonConvert.DeserializeObject<ObservableCollection<Fraction>>(content);
                    RaisePropertyChanged("FractionList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable", "Cancel");
                }
            }

            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable (" + ex.Message + ")", "Cancel");
            }
        }

        public void HideOrShowFractions(Fraction fraction)
        {
            fraction.IsVisible = true;

            UpdateFraction(fraction);
            if (_oldfraction==fraction)
            {
                //Klicka två gånger för att gömma
                fraction.IsVisible = !fraction.IsVisible;
                UpdateFraction(fraction);
            }
            else
            {
                //Göm föregående objekt
                if (_oldfraction != null)
                {
                    _oldfraction.IsVisible = false;
                    UpdateFraction(_oldfraction);
                }

                //Visa valt objekt
                fraction.IsVisible = true;
                UpdateFraction(fraction);
            }
            _oldfraction = fraction;
        }

        private void UpdateFraction(Fraction fraction)
        {
            var index = FractionList.IndexOf(fraction);
            FractionList.Remove(fraction);
            FractionList.Insert(index, fraction);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
