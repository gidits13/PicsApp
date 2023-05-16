using PicsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicsApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowPicPage : ContentPage
	{
		private Pic _pic;
		public ShowPicPage(Pic pic)
		{
			_pic = pic;
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			pic.Source = _pic.FullName;
			picL.Text= _pic.CreationTime.ToString();
			base.OnAppearing();
		}
	}
}