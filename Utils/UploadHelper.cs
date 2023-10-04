using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

public class UploadHelper
{
        public static async Task<ActionResult> UpLoadFileToFirebaseStorage(IFormFile fileToUpload, string apiKey, string authEmail, string authPassword, string bucket, string pathFromRoot = "")
        {
            string messageResult = "";

            if (fileToUpload == null || fileToUpload.Length == 0) 
            {
                return new OkObjectResult(new {message="File null"}); 
            }
            string fileName = Path.GetFileName(fileToUpload.FileName); 

            string contentType = fileToUpload.ContentType; 

            var fpath = Path.Combine("./something", Path.GetRandomFileName());
            
            //var streamFs = System.IO.File.Open("./something/iztfn4jw.txt", FileMode.Open);

            // of course you can login using other method, not just email+password

            var config = new FirebaseAuthConfig
            {
                ApiKey = apiKey,
                AuthDomain = "arenkid-projectbe.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    // Add and configure individual providers
                    new GoogleProvider().AddScopes("email"),
                    //new EmailProvider
                    new EmailProvider()
                },

            };

            // ...and create your FirebaseAuthClient
            var client = new FirebaseAuthClient(config);

            var a = await client.SignInWithEmailAndPasswordAsync(authEmail, authPassword);

            var idToken = await a.User.GetIdTokenAsync();

            // you can use CancellationTokenSource to cancel the upload midway
            var cancellation = new CancellationTokenSource();

            // FirebaseStorage.Put method accepts any type of stream.
        
            try
            {   

                using(var streamFile = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(streamFile);
                    
                    Console.WriteLine("Byte got: " + streamFile.Length);

                    streamFile.Position = 0;

                    var listDir = pathFromRoot.Split("/");

                    var fsInstance = new FirebaseStorage(
                        bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory =  () => Task.FromResult(idToken),
                            ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                        });


                    FirebaseStorageReference? fsRef = null;

                    for(int i=0; i<listDir.Length; i++)
                    {
                        if(i == 0)
                        {
                            fsRef = fsInstance.Child(listDir[i]);
                        }
                        else
                        {
                            fsRef = fsRef!.Child(listDir[i]);
                        }
                    }
                        
                    FirebaseStorageTask? task = null;
                    if(fsRef != null)
                    {
                        fsRef = fsRef.Child(fileToUpload.FileName);
                        
                    }
                    else
                    {
                        fsRef = fsInstance.Child(fileToUpload.FileName);
                    }

                    task = fsRef.PutAsync(streamFile, cancellation.Token);

                    task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} {e} %");
                    // error during upload will be thrown when you await the task
                    messageResult = await task;
                    
                    Console.WriteLine("Download link:\n" + messageResult);
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
  
            return new OkObjectResult(new {message=messageResult}); 
            

        }

}