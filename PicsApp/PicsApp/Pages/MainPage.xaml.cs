using PicsApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PicsApp
{
	public partial class MainPage : ContentPage
	{
		private string _pin;
		public MainPage()
		{
			InitializeComponent();
		}

		private async void pinB_Clicked(object sender, EventArgs e)
		{
			if(pinE.Text==null)
			{
				pinL.Text = "пин не может быть пустым";
				return;
			}
			if(pinE.Text.Length!=4)
			{
				pinL.Text = "пин должен быть 4 знака";
				return;
			}
			if(_pin is null)
			{
				_pin= pinE.Text;
				Preferences.Set("pin", _pin);
				await Navigation.PushAsync(new PicsPage());
			}
			else
			{
				if(_pin==pinE.Text)
				{
					await Navigation.PushAsync(new PicsPage());
				}
			}
        }
		protected override void OnAppearing()
		{
			_pin = Preferences.Get("pin", null);
			if (_pin == null) 
			{
				pinL.Text = "Необходимо задать пин";
				pinB.Text = "Подтвердить";
			}
			else
			{
				pinL.Text = "Введите пин";
				pinB.Text = "Войти";
			}
			base.OnAppearing();
		}
	}
}
