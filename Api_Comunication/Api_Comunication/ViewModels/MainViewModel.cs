using Api_Comunication.Models;
using Api_Comunication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Api_Comunication.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        IGoogleApiService _apiGoogle = new GoogleApiService();

        public event PropertyChangedEventHandler PropertyChanged;

        public string originPlace { get; set; }
        public string destinationPlace { get; set; }
        public string ShowDistance { get; set; };
        public List<Root> Info { get; set; }
        //public GoogleData Data { get; set; }
        public ICommand Search { get; set; }
        public MainViewModel()
        {
            Search = new Command(async () =>
            {
                await GetDistance();
            });
        }

        async Task GetDistance()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    var classes = await _apiGoogle.GetDistance(originPlace, destinationPlace);
                    ShowDistance = classes.rows[0].elements[0].distance.text;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                    await App.Current.MainPage.DisplayAlert("error", $"{ex}", "ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No tienes Conexion a internet", "Ok");
            }
        }
    }
}
