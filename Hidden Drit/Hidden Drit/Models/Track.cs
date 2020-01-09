using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Hidden_Drit.Models
{
    public class Track
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TrackTypesId { get; set; }
        public int TrackLevelId { get; set; }
        public string ImagePath { get; set; }
        public string ImageURL { get; set; }

        public ImageSource PictureSource
        {
            get
            {
                ImageSource retval = null;

                if (!String.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
                    retval = ImageSource.FromFile(ImagePath);
                else
                {
                    if (!String.IsNullOrEmpty(ImageURL))
                        retval = ImageSource.FromUri(new Uri(ImageURL));
                }
                return retval;
            }
        }
        //public Android.Graphics.Bitmap Picture
        //{
        //    get 
        //    {
        //        string path = ImagePath;
        //        if (String.IsNullOrEmpty(path))
        //            path = ImageURL;
        //        if (String.IsNullOrEmpty(path))
        //            return null;
        //        else
        //            return GetImageBitmapFromUrl(path);
        //    }
        //}

        //private Android.Graphics.Bitmap GetImageBitmapFromUrl(string url)
        //{
        //    Android.Graphics.Bitmap imageBitmap = null;

        //    using (var webClient = new System.Net.WebClient())
        //    {
        //        var imageBytes = webClient.DownloadData(url);
        //        if (imageBytes != null && imageBytes.Length > 0)
        //        {
        //            imageBitmap = Android.Graphics.BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
        //        }


        //    }

        //    return imageBitmap;
        //}
    }
}

