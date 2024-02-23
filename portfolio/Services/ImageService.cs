using System.Net;
using Firebase.Storage;
using portfolio.Interfaces;

namespace portfolio.Services;

public class StorageService(IConfiguration configuration) : IStorageService
{
    private readonly string? _bucket = configuration["Firebase:StorageBucket"];
    private readonly string? _apiKey = configuration["Firebase:ApiKey"];

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var task = new FirebaseStorage(_bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
                ThrowOnCancel = true
            })
            .Child("images")
            .Child(fileName)
            .PutAsync(fileStream);

        var downloadUrl = await task;
        return downloadUrl;
    }



    public async Task DeleteFileAsync(string fileLink)
    {
        Uri fileUri = new Uri(fileLink);
        string filePath = WebUtility.UrlDecode(fileUri.AbsolutePath);

        const string prefixToRemove = "/images/";
        int prefixPosition = filePath.IndexOf(prefixToRemove);
        if (prefixPosition >= 0)
        {
            filePath = filePath.Substring(prefixPosition + prefixToRemove.Length);
        }

        filePath = filePath.Trim('/');

        var storage = new FirebaseStorage(_bucket, new FirebaseStorageOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
            ThrowOnCancel = true
        });

        await storage.Child("images").Child(filePath).DeleteAsync();
    }
}
