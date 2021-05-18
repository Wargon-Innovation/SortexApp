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
   public class BrandViewModel : INotifyPropertyChanged
    {
        private BrandView _oldBrand;

        public ObservableCollection<Brand> BrandList { get; set; } = new ObservableCollection<Brand>();
        public ObservableCollection<Tag> BrandTagList { get; set; } = new ObservableCollection<Tag>();
        public ObservableCollection<BrandTagMM> BrandTagMMList { get; set; } = new ObservableCollection<BrandTagMM>();
        public ObservableCollection<BrandImages> BrandImageList { get; set; } = new ObservableCollection<BrandImages>();
        public ObservableCollection<BrandView> BrandViewList { get; set; } = new ObservableCollection<BrandView>();

        public event PropertyChangedEventHandler PropertyChanged;

        internal async Task LoadBrandAsync()
        {
            //HÄMTA ALLA LISTOR SOM MÄRKEN BESTÅR AV FRÅN API
            try
            {
                //HÄMTA MÄRKEN
                string apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Brands";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    BrandList = JsonConvert.DeserializeObject<ObservableCollection<Brand>>(content);
                    RaisePropertyChanged("BrandList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable", "Cancel");
                }

                //HÄMTA MÄRKESBILDER
                apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/BrandImages";
                response = await httpClient.GetAsync(new Uri(apiURL));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    BrandImageList = JsonConvert.DeserializeObject<ObservableCollection<BrandImages>>(content);
                    RaisePropertyChanged("BrandImageList");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable", "Cancel");
                }

                //HÄMTA TAGS FÖR SÖKNING PÅ MÄRKEN
                apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Tags";
                response = await httpClient.GetAsync(new Uri(apiURL));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    BrandTagList = JsonConvert.DeserializeObject<ObservableCollection<Tag>>(content);
                    RaisePropertyChanged("BrandTagList");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Connection unstable", "Cancel");
                }

                //HÄMTA SAMBANDSTABELL FÖR ATT JÄMFÖRA ID 
                apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/BrandTagMMs";
                response = await httpClient.GetAsync(new Uri(apiURL));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    BrandTagMMList = JsonConvert.DeserializeObject<ObservableCollection<BrandTagMM>>(content);
                    RaisePropertyChanged("BrandTagMMList");
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

            //JÄMFÖRA VÄRDEN
            foreach (var brand in BrandList)
            {
                var brandView = new BrandView();
                brandView.Id = brand.Id;
                brandView.Manufacturer = brand.Manufacturer;
                brandView.Classification = brand.Classification;
                brandView.Gender = brand.Gender;
                
                foreach (var brandTag in BrandTagMMList)
                {
                    if (brand.Id == brandTag.BrandId)
                    {

                        foreach (var tag in BrandTagList)
                        {
                            if (tag.Id == brandTag.TagId)
                            {
                                brandView.TagList.Add(tag);
                            }
                        }
                    }
                }

                foreach (var image in BrandImageList)
                {
                    if (image.brandId == brand.Id)
                    {
                        brandView.brandImages.Add(image);
                    }
                }

                BrandViewList.Add(brandView);

            }
            BrandViewList = new ObservableCollection<BrandView>(BrandViewList.OrderBy(i => i.Manufacturer).ToList());
            RaisePropertyChanged("BrandViewList");
        }

        internal void HideOrShowBrand(BrandView brand)
        {
            brand.IsVisible = true;
            UpdateBrand(brand);
            if(_oldBrand == brand)
            {
                brand.IsVisible = !brand.IsVisible;
                UpdateBrand(brand);
            }
            else
            {
                if(_oldBrand != null)
                {
                    _oldBrand.IsVisible = false;
                    UpdateBrand(_oldBrand);
                }
                brand.IsVisible = true;
                UpdateBrand(brand);
            }
            _oldBrand = brand;
        }

        private void UpdateBrand(BrandView brand)
        {
            var index = BrandViewList.IndexOf(brand);
            if(index != -1)
            {
                BrandViewList.Remove(brand);
                BrandViewList.Insert(index, brand);
            }
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
