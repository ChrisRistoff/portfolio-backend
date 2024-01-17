using Firebase.Storage;

namespace portfolio.Storage;

public class StorageService
{
    private readonly string? _bucket;
    private readonly string? _apiKey;

    public StorageService(IConfiguration configuration)
    {
        _bucket = configuration["Firebase:StorageBucket"];
        _apiKey = configuration["Firebase:ApiKey"];
    }

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
