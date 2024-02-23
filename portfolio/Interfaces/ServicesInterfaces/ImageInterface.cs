namespace portfolio.Interfaces;

interface IStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);

    Task DeleteFileAsync(string fileLink);
}
