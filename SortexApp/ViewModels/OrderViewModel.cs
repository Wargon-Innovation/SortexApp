﻿using Newtonsoft.Json;
using SortexApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace SortexApp.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private Order _oldOrder;

        public ObservableCollection<Order> OrderList { get; set; } = new ObservableCollection<Order>();
        internal async System.Threading.Tasks.Task LoadOrderAsync()
        {
            try
            {
                string apiURL = @"https://informatik13.ei.hv.se/SortexAPI/api/Orders";
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = await httpClient.GetAsync(new Uri(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    OrderList = JsonConvert.DeserializeObject<ObservableCollection<Order>>(content);
                    RaisePropertyChanged("OrderList");
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

        public void HideOrShowOrder(Order order)
        {
            order.isVisible = true;

            UpdateOrder(order);

            if(_oldOrder == order)
            {
                //Klicka två gånger för att gömma
                order.isVisible = !order.isVisible;
                UpdateOrder(order);
            }
            else
            {
                if(_oldOrder != null)
                {
                    //Göm föregående objekt
                    _oldOrder.isVisible = false;
                    UpdateOrder(_oldOrder);
                }
                //show selected item
                order.isVisible = true;
                UpdateOrder(order);
            }

            _oldOrder = order;
            

        }

        private void UpdateOrder(Order order)
        {
            var index = OrderList.IndexOf(order);
            OrderList.Remove(order);
            OrderList.Insert(index, order);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged(string properyName)
        {
            if ( PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(properyName));
            }
        }
    }
  }

