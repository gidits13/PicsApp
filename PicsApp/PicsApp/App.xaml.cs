using AutoMapper;
using PicsApp.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicsApp
{
	public partial class App : Application
	{
		public static IMapper Mapper { get; set; }
		public App()
		{
			InitializeComponent();
			Mapper = CreateMapper();
			MainPage = new NavigationPage(new MainPage());
		}

		private static IMapper CreateMapper()
		{
			var config = new MapperConfiguration(
				cfg =>
				{
					cfg.CreateMap<FileInfo, Pic>();
				});
			return config.CreateMapper();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
