using DAL.Interfaces;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace DAL.Implementations;

public class CloudStorageRepository : ICloudStorageRepository
{
    private readonly string _bucketName;

    public CloudStorageRepository()
    {
        _bucketName = "phygital-public";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "service-acc-key.json");
    }
    public void UploadFile(IFormFile file, string fileName, string folderName)
    {
        var storage = StorageClient.Create();
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        memoryStream.Position = 0;

        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        var contentType = fileExtension switch
        {
            ".jpg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream" // default content type fallback
        };

        storage.UploadObject(_bucketName, $"{folderName}/{fileName}{fileExtension}", contentType, memoryStream);
    }
    public bool FileExists(string fileName)
    {
        var storage = StorageClient.Create();
        foreach (var obj in storage.ListObjects(_bucketName, ""))
        {
            if (obj.Name == fileName)
            {
                return true;
            }
        }
        return false;
    }
}