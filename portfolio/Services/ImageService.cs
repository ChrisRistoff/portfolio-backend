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
}
