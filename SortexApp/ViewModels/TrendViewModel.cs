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
    public class TrendViewModel : INotifyPropertyChanged
    {
        private TrendImageView _oldTrend;

        public ObservableCollection<Trend> TrendList { get; set; } = new ObservableCollection<Trend>();
        public ObservableCollection<TrendImage> TrendImageList { get; set; } = new ObservableCollection<TrendImage>();
        public ObservableCollection<TrendImageMM> TrendImageMMList { get; set; } = new ObservableCollection<TrendImageMM>();

        

        public ObservableCollection<TrendImageView> TrendImageViewList { get; set; } = new ObservableCollection<TrendImageView>();

        internal async Task LoadTrendAsync()
        {
            try
            {
                //HÄMTA TRENDER
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
                    await Application.Current.MainPage.DisplayAlert("Fel", "Internetuppkopplingen är ostabil, kolla anslutningen", "Cancel");
                }

                //Hämta trendImages
                apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/TrendImages";
                response = await htppClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TrendImageList = JsonConvert.DeserializeObject<ObservableCollection<TrendImage>>(content);
                    RaisePropertyChanged("TrendImageList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fel", "Internetuppkopplingen är ostabil, kolla anslutningen", "Cancel");
                }

                //HÄMTA TRENDIMAGEMM
                apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/TrendImageMMs";
                response = await htppClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TrendImageMMList = JsonConvert.DeserializeObject<ObservableCollection<TrendImageMM>>(content);
                    RaisePropertyChanged("TrendImageMMList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fel", "Internetuppkopplingen är ostabil, kolla anslutningen", "Cancel");
                }
            }
            catch (Exception)
            {

                await Application.Current.MainPage.DisplayAlert("Fel", "Internetuppkopplingen är ostabil, kolla anslutningen", "Cancel");
            }

            //ÖPPNA EJ!!!!!
            foreach (var trend in TrendList)
            {
                var trendImageView = new TrendImageView();
                trendImageView.Id = trend.Id;
                trendImageView.Season = trend.Season;
                trendImageView.Description = trend.Description;

                var trendImages = (from rowsTrendImages in TrendImageList
                                   join rowsTrendImagesMM in TrendImageMMList on rowsTrendImages.Id equals rowsTrendImagesMM.TrendImageId
                                   where rowsTrendImagesMM.TrendId == trend.Id
                                   select rowsTrendImages).ToList();
                
                foreach (var image in trendImages)
                {
                    trendImageView.TrendImages.Add(image);
                }
                /*
                foreach (var imageMM in TrendImageMMList)
                {
                    if (trend.Id == imageMM.TrendId)
                    {
                        foreach (var image in TrendImageList)
                        {
                            if(image.Id == imageMM.TrendImageId)
                            {
                                trendImageView.TrendImages.Add(image);
                            }
                        }
                    }
                }
                */
                
                TrendImageViewList.Add(trendImageView);
                
            }
            TrendImageViewList = new ObservableCollection<TrendImageView>(TrendImageViewList.OrderBy(i => i.Season).ToList());
            RaisePropertyChanged("TrendImageViewList");


        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        

        //toggle
        internal void HideOrShowTrends(TrendImageView trend)
        {
            //trend.IsVisible = true;

            UpdateTrend(trend);
            if (_oldTrend == trend && trend.IsVisible)
            {
                //Klicka två gånger för att gömma
                trend.IsVisible = !trend.IsVisible;
                UpdateTrend(trend);
            }
            else
            {
                //Göm föregående objekt
                if (_oldTrend != null)
                {
                    _oldTrend.IsVisible = false;
                    UpdateTrend(_oldTrend);
                }

                //Visa valt objekt
                trend.IsVisible = true;
                UpdateTrend(trend);
            }
            _oldTrend = trend;
        }

        private void UpdateTrend(TrendImageView trend)
        {
            var index = TrendImageViewList.IndexOf(trend);
            if (index != -1)
            {
                TrendImageViewList.Remove(trend);
                TrendImageViewList.Insert(index, trend);
            }
            return;
        }
    }
}
