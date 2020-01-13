using Firebase.Storage;
using Hidden_Drit.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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
        private Track _currentTrack;
        private string URL { get; set; }

        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();
        //ImageSizeHelper imgSizeHelper = new ImageSizeHelper();

        public AddTrackPage()
        {
            InitializeComponent();

            populatePickers();
            _currentTrack = new Track();
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
                _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.MaxWidthHeight, MaxWidthHeight = 600 });
                if (_mediaFile == null) return;
                imgCoverImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                
            }
        }

        private async void btnTakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            else
            {
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new  Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 600,
                    Directory = "Images",
                    Name = $"{DateTime.UtcNow}.jpg"
                });

                if (_mediaFile == null) 
                    return;

                imgCoverImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
            }
        }

        
        private async void btnSave_ClickedAsync(object sender, EventArgs e)
        {
            _currentTrack = new Track();

            _currentTrack.CreatedDate = DateTime.Now;
            _currentTrack.CreatedID = 1;
            _currentTrack.Description = txtDescription.Text;
            _currentTrack.Name = txtTrackName.Text;
            _currentTrack.TrackLevelId = LevelPicker.SelectedIndex;
            _currentTrack.TrackTypesId = TrackTypePicker.SelectedIndex;
            _currentTrack.ImagePath = _mediaFile.Path;
            //Stream imgStream = imgSizeHelper.ResizeImage(_mediaFile, 640, 480);
            _currentTrack.ImageURL = await firebaseStorageHelper.UploadFile(_mediaFile.GetStream(), Path.GetFileName(_mediaFile.Path));

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DbPath))
            {
                conn.CreateTable<Track>();
                int numberOfRows = conn.Insert(_currentTrack);

                if (numberOfRows > 0)
                {
                    await DisplayAlert("Saved", "Track Details have been saved", "Ok");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Saved", "Trac Details have failed to saved", "Ok");
                }

            }

        }

        private string UploadImage()
        {
            throw new NotImplementedException();
        }

        private async void btnUploadGeoData_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                var filedata = await CrossFilePicker.Current.PickFile();
                if (filedata != null && File.Exists(filedata.FilePath))
                {
                    if (Path.GetExtension(filedata.FilePath).ToLower() != "kml")
                    {
                        //Please select a KML file.
                        return;
                    }


                    string FileContent = File.ReadAllText(filedata.FilePath);
                }

            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
