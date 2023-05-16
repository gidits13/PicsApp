using AutoMapper;
using PicsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicsApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicsPage : ContentPage
	{
		public ObservableCollection<Pic> Pics { get; set; }
		private StackLayout _selectedItem;
		private const string PATH = @"/storage/emulated/0/DCIM/Camera";
		public PicsPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			if(Directory.Exists(PATH))
			{
				var fileList = new DirectoryInfo(PATH).GetFiles();
				var picsList = App.Mapper.Map<Pic[]>(fileList);
				Pics = new ObservableCollection<Pic>(picsList.Where(x=>x.Name.EndsWith(".jpg")));

				ShowPics();
				Pics.CollectionChanged += (sender, e) => ShowPics();
			}
			base.OnAppearing();
		}

		private void ShowPics()
		{
			_selectedItem=null;
			int ccol = 0;int crow = 0;
			Grid picsGrid = new Grid();
			picsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			picsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
			
			for (int i = 0; i < Pics.Count / 2;i++)
			{
				picsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			}

			foreach (var item in Pics)
			{
				StackLayout pic = new StackLayout
				{
					Children =
					{
						new Image{Source=item.FullName , HeightRequest=200},
						new Label {Text = item.Name}
					},
					Padding = new Thickness(10, 0, 10, 0)
				};
				
				var tapRecognizer = new TapGestureRecognizer();
				tapRecognizer.Tapped += TappedPic;
				pic.GestureRecognizers.Add(tapRecognizer);
				picsGrid.Children.Add(pic, ccol, crow);
				ccol++;
				if (ccol==2)
				{
					crow++;
					ccol = 0;
				}
			}
			scrollView.Content = picsGrid;
		}

		private void TappedPic(object sender, EventArgs e)
		{
			var tapped = (StackLayout)sender;
			if (tapped == _selectedItem)
			{
				return;
			}
			if (_selectedItem != null){
				_selectedItem.BackgroundColor = Color.White;
				_selectedItem = tapped;
				_selectedItem.BackgroundColor = Color.Red;
			}
			else
			{
				_selectedItem = tapped;
				_selectedItem.BackgroundColor = Color.Red;
			}

		}


		private void delete_Clicked(object sender, EventArgs e)
		{
			if(_selectedItem!=null)
			{
				var image = (Image)_selectedItem.Children[0];
				var imagePath = image.Source.ToString().Substring(6);
				if(File.Exists(imagePath)) File.Delete(imagePath);
				var deletedPic = App.Mapper.Map<Pic>(new FileInfo(imagePath));
				Pics.Remove(deletedPic);
			}
		}

		private async void show_Clicked(object sender, EventArgs e)
		{
			if(_selectedItem !=null)
			{
				var image = (Image)_selectedItem.Children[0];
				var imagePath = image.Source.ToString().Substring(6);
				var fPic=new FileInfo(imagePath);
				var pic = App.Mapper.Map<Pic>(fPic);
				await Navigation.PushAsync(new ShowPicPage(pic));

			}
		}
	}
}