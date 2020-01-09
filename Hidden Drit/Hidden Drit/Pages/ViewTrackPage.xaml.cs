using Hidden_Drit.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hidden_Drit.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewTrackPage : ContentPage
    {
        Track currentTrack = null;
        private MediaFile _mediaFile;

        public ViewTrackPage(Models.Track myItem)
        {
            InitializeComponent();
            currentTrack = myItem;
            PopulateView();
        }

        private void PopulateView()
        {
            lblName.Text = currentTrack.Name;
            lblDescription.Text = currentTrack.Description;

            //imgCoverImage.WidthRequest = currentTrack.Picture.Width;
            //imgCoverImage.HeightRequest = currentTrack.Picture.Height;
            imgCoverImage.Source = currentTrack.PictureSource;
            


            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {

                int LevelID = currentTrack.TrackLevelId + 1;
                int TypeID = currentTrack.TrackTypesId + 1;


                var level = conn.Table<TrackLevel>().Where(w => w.Id == LevelID).FirstOrDefault();
                var type = conn.Table<TrackType>().Where(w => w.Id == TypeID).FirstOrDefault();

                if (level != null)
                    lblLevel.Text = level.Name;

                if(type != null)
                {
                    lblType.Text = type.Name;
                }
            }



        }

        private void btnViewMap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapPage());
        }
    }
}