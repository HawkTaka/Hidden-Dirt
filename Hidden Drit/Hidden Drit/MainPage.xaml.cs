using Hidden_Drit.Models;
using Hidden_Drit.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hidden_Drit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadTracks();
        }

        private void LoadTracks()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Track>();
                var books = conn.Table<Track>().ToList();
                TracksListView.ItemsSource = books;
            }
        }

        private void btnAddTrack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTrackPage());
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {

        }
    }
}
