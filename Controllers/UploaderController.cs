using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;

namespace ProjectBackend.Models;


public class UploaderController : Controller
{
    private static string ApiKey = "AIzaSyCZBeMCx5PYzlkZepR74hOHj-FbXZlgVbQ";
    private static string Bucket = "arenkid-projectbe.appspot.com";
    private static string AuthEmail = "kingsoulz4d@gmail.com";
    private static string AuthPassword = "55444664";


    // GET: Uploader
    public ActionResult Index()
    {
        
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Index(IFormFile  fileToUpload)
    {
        if (fileToUpload == null || fileToUpload.Length == 0) 
        {

              return Problem("No file selected."); 
        }
        string fileName = Path.GetFileName(fileToUpload.FileName); 

        string contentType = fileToUpload.ContentType; 

        var fpath = Path.Combine("./something", Path.GetRandomFileName());
        

        try 
        { 

            // using(var streamFile = System.IO.File.Create(fpath))
            // {
            //     await fileToUpload.CopyToAsync(streamFile);
            // }
            
            // using(MemoryStream memoryStream = new MemoryStream()) 
            // { 

            //     fileToUpload.CopyToAsync(memoryStream); 
            //     using(PgSqlConnection pgSqlConnection = 

            //     new PgSqlConnection("User Id = postgres; Password = sa123#;" + 

            //             "host=localhost;database=demo;")) 

            //     { 

            //     using(PgSqlCommand pgSqlCommand = new PgSqlCommand()) 

            //     { 

            //     pgSqlCommand.CommandText = 

            //         "INSERT INTO filestore " + 

            //             "(created_on, file_data) " + 

            //             "VALUES (:createdon, :filedata)"; 

            //     pgSqlCommand.Parameters.Add("createdon", DateTime.Now); 

            //     pgSqlCommand.Parameters.Add("filedata", PgSqlType.ByteA).Value = 

            //                     memoryStream.ToArray(); 



            //                 pgSqlCommand.Connection = pgSqlConnection; 

            //                 if (pgSqlConnection.State != 

            //                     System.Data.ConnectionState.Open) 

            //                     pgSqlConnection.Open(); 

            //                 pgSqlCommand.ExecuteNonQuery(); 

            //             } 

            //     } 
            // } 

        } 

        catch (Exception e)
        { 
            
            return Problem(e.StackTrace); 

        } 


        
        //var streamFs = System.IO.File.Open("./something/iztfn4jw.txt", FileMode.Open);

        // of course you can login using other method, not just email+password

        var config = new FirebaseAuthConfig
        {
            ApiKey = ApiKey,
            AuthDomain = "arenkid-projectbe.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                // Add and configure individual providers
                new GoogleProvider().AddScopes("email"),
                //new EmailProvider
                new EmailProvider()
                // ...
            },
            // // WPF:
            // UserRepository = new FileUserRepository("FirebaseSample") // persist data into %AppData%\FirebaseSample
            // // // UWP:
            // //UserRepository = new StorageRepository() // persist data into ApplicationDataContainer
        };

        // ...and create your FirebaseAuthClient
        var client = new FirebaseAuthClient(config);

        var a = await client.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

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

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory =  () => Task.FromResult(idToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child("receipts")
                    .Child("test")
                    .Child(fileToUpload.FileName)
                    .PutAsync(streamFile, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                // cancel the upload
                // cancellation.Cancel();

            
                // error during upload will be thrown when you await the task
                Console.WriteLine("Download link:\n" + await task);
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
        }

        
        return View(); 
    }

    //gs://arenkid-projectbe.appspot.com

    // private async void UploadFileToFirebaseStorage()
    // {

    // }


}