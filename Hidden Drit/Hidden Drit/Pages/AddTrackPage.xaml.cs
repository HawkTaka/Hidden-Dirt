using Firebase.Storage;
using Hidden_Drit.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hidden_Drit.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTrackPage : ContentPage
    {
        private MediaFile _mediaFile;
        private string URL { get; set; }

        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();

        public AddTrackPage()
        {
            InitializeComponent();

            populatePickers();
        }

        private void populatePickers()
        {

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                var TrackTypes = conn.Table<TrackType>().Select(s => s.Name).ToList();
                TrackTypePicker.ItemsSource = TrackTypes;
                TrackTypePicker.SelectedIndex = 0;

                var TrackLevels = conn.Table<TrackLevel>().Select(s => s.Name).ToList();
                LevelPicker.ItemsSource = TrackLevels;
                LevelPicker.SelectedIndex = 0;
            }

        }

        private async void btnSelectImage_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;
                imgCoverImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());   
            }
        }

        private async void btnSave_ClickedAsync(object sender, EventArgs e)
        {
            var newTrack = new Track();

            newTrack.CreatedDate = DateTime.Now;
            newTrack.CreatedID = 1;
            newTrack.Description = txtDescription.Text;
            newTrack.Name = txtTrackName.Text;
            newTrack.TrackLevelId = LevelPicker.SelectedIndex;
            newTrack.TrackTypesId = TrackTypePicker.SelectedIndex;
            newTrack.ImagePath = _mediaFile.Path;
            newTrack.ImageURL = await firebaseStorageHelper.UploadFile(_mediaFile.GetStream(), Path.GetFileName(_mediaFile.Path));

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Track>();
                int numberOfRows = conn.Insert(newTrack);

                if (numberOfRows > 0)
                {
                    DisplayAlert("Saved", "Track Details have been saved", "Ok");
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    DisplayAlert("Saved", "Trac Details have failed to saved", "Ok");
                }

            }

        }

        private string UploadImage()
        {
            throw new NotImplementedException();
        }

    }
}
