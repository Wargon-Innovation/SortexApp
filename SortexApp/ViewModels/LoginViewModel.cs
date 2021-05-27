using SortexApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SortexApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public async void OnSubmit()
        {
            if (userName != "test" || password != "test")
            {
                DisplayInvalidLoginPrompt();
            }

            else
            {
                App.isLogedIn = true;
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            
        }
    }

    //    public Command LoginCommand { get; }

    //    public LoginViewModel()
    //    {
    //        LoginCommand = new Command(OnLoginClicked);
    //    }

    //    private async void OnLoginClicked(object obj)
    //    {
    //        //App.isLogedIn = true;
    //        //// Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
    //        //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
    //    }
    //    public async void checkPw(string username, string password)
    //    {
    //        if(username == "test" && password == "test")
    //        {
    //            App.isLogedIn = true;
    //            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
    //            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
    //        }


    //        return;
    //    }
    //}
}
