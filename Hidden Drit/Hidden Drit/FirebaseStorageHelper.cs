using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hidden_Drit
{
    public class FirebaseStorageHelper
    {
        FirebaseStorage firebaseStorage = new FirebaseStorage("gs://hidden-dirt.appspot.com");

        public FirebaseStorageHelper(string overWriteURL = "")
        {
            if (overWriteURL != "")
            {
                firebaseStorage = new FirebaseStorage(overWriteURL);                
            }
        }


        public async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            try
            {
                var imageUrl = await firebaseStorage
                                    .Child("XamarinMonkeys")
                                    .Child(fileName)
                                    .PutAsync(fileStream);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return imageUrl;
        }
    }
}
