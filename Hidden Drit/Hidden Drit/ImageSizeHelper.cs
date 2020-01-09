using System.IO;
using Plugin.Media.Abstractions;

namespace Hidden_Drit
{
    public class ImageSizeHelper
    {
        //public MemoryStream ResizeImage(MediaFile _mediaFile, float width, float height)
        //{

        //    //byte[] imageData = GetImageBytes(_mediaFile);

        //    //// Load the bitmap 
        //    //BitmapFactory.Options options = new BitmapFactory.Options();// Create object of bitmapfactory's option method for further option use
        //    //options.InPurgeable = true; // inPurgeable is used to free up memory while required
        //    //Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

        //    //float newHeight = 0;
        //    //float newWidth = 0;

        //    //var originalHeight = originalImage.Height;
        //    //var originalWidth = originalImage.Width;

        //    //if (originalHeight > originalWidth)
        //    //{
        //    //    newHeight = height;
        //    //    float ratio = originalHeight / height;
        //    //    newWidth = originalWidth / ratio;
        //    //}
        //    //else
        //    //{
        //    //    newWidth = width;
        //    //    float ratio = originalWidth / width;
        //    //    newHeight = originalHeight / ratio;
        //    //}

        //    //Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);

        //    //originalImage.Recycle();

        //    //MemoryStream ms = new MemoryStream();

        //    //resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);

        //    //resizedImage.Recycle();

        //    //return ms;//.ToArray();

        //}

        private byte[] GetImageBytes(MediaFile _mediaFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                _mediaFile.GetStream().CopyTo(memoryStream);
                _mediaFile.Dispose();
                return memoryStream.ToArray();
            }
        }

    }
}
