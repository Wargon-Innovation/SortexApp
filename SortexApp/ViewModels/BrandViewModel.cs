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
        public ObservableCollection<BrandView> brandHolderList { get; set; }

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

                //TAGGAR
                var brandTags = (from rowsBrandTagMM in BrandTagMMList
                                 join rowsTag in BrandTagList on rowsBrandTagMM.TagId equals rowsTag.Id
                                 where rowsBrandTagMM.BrandId == brand.Id
                                 select rowsTag).ToList();
                foreach (var tag in brandTags)
                {
                    brandView.TagList.Add(tag);
                }

                //BILDER
                var brandImages = (from rowsImage in BrandImageList
                                   where rowsImage.brandId == brand.Id
                                   select rowsImage).ToList();
                foreach (var image in brandImages)
                {
                    brandView.brandImages.Add(image);
                }

                BrandViewList.Add(brandView);
                /*foreach (var brandTag in BrandTagMMList)
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
                }*/

                /*
                foreach (var image in BrandImageList)
                {
                    if (image.brandId == brand.Id)
                    {
                        brandView.brandImages.Add(image);
                    }
                }
                */

                //1. flytta funktion till viewmodel
                //2. listor, baslista och displaylist som kopia. Ny sökning som ersätter displaylistan.
                //property visible = false
            }
            BrandViewList = new ObservableCollection<BrandView>(BrandViewList.OrderBy(i => i.Manufacturer).ToList());

            RaisePropertyChanged("BrandViewList");
        }



        public ObservableCollection<BrandView> SearchBrand(string tag)
        {
            brandHolderList = new ObservableCollection<BrandView>();
            brandHolderList.Clear();

            var brandList = (from tags in App.Brand.BrandTagList
                             join brandTagsMM in App.Brand.BrandTagMMList on tags.Id equals brandTagsMM.TagId
                             join brands in App.Brand.BrandViewList on brandTagsMM.BrandId equals brands.Id
                             where tags.Value.Contains(tag)
                             select brands).ToList();




            //LINQ
            foreach (var brand in brandList)
            {
                var brands = (from rowsBrand in App.Brand.BrandViewList
                              where rowsBrand.Id == brand.Id
                              select rowsBrand).FirstOrDefault();

                brands.Visible = true;

                brandHolderList.Add(brands);
                RaisePropertyChanged("brandHolderList");


                //foreach (var item in BrandViewList)
                //{
                //    if (item.Id == brand.Id)
                //    {
                //        item.Visible = true;

                //        brandHolderList.Add(item);
                //        RaisePropertyChanged("brandHolderList");

                //    }

                //}

            }
            return brandHolderList;

        }


        internal void HideOrShowBrandPlaceHolder(BrandView brand)
        {
            brand.IsVisible = true;
            UpdateBrandPlaceHolder(brand);
            if (_oldBrand == brand)
            {
                brand.IsVisible = !brand.IsVisible;
                UpdateBrandPlaceHolder(brand);
            }
            else
            {
                if (_oldBrand != null)
                {
                    _oldBrand.IsVisible = false;
                    UpdateBrandPlaceHolder(_oldBrand);
                }
                brand.IsVisible = true;
                UpdateBrandPlaceHolder(brand);
            }
            _oldBrand = brand;
        }

        private void UpdateBrandPlaceHolder(BrandView brand)
        {
            var index = brandHolderList.IndexOf(brand);
            if (index != -1)
            {
                brandHolderList.Remove(brand);
                brandHolderList.Insert(index, brand);
            }
        }

       

        internal void HideOrShowBrand(BrandView brand)
        {
            brand.IsVisible = true;
            UpdateBrand(brand);
            if (_oldBrand == brand)
            {
                brand.IsVisible = !brand.IsVisible;
                UpdateBrand(brand);
            }
            else
            {
                if (_oldBrand != null)
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
            if (index != -1)
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
