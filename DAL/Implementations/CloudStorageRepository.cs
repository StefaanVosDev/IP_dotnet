using System.Collections.Specialized;
using DAL.Interfaces;
using Google.Cloud.Storage.V1;
using Google.Cloud.SecretManager.V1;
using Microsoft.AspNetCore.Http;

namespace DAL.Implementations;

public class CloudStorageRepository : ICloudStorageRepository
{
    private readonly string _bucketName;

    public CloudStorageRepository()
    {
        _bucketName = "phygital-public";
        //set the environment variable to the path of the service account key
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "service-acc-key.json");
    }
    public void UploadFile(IFormFile file, string fileName, string folderName)
    {
        var storage = StorageClient.Create();
        // Create a memory stream to store the uploaded file
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        // Reset the position of the memory stream to 0
        memoryStream.Position = 0;

        // Get the file extension and content type
        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        var contentType = fileExtension switch
        {
            ".jpg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream" // default content type fallback
        };

        // Upload the file to the specified folder in the bucket
        storage.UploadObject(_bucketName, $"{folderName}/{fileName}{fileExtension}", contentType, memoryStream);
    }
    public bool FileExists(string fileName)
    {
        // List all objects in the bucket
        var storage = StorageClient.Create();
        // Check if the file exists in the bucket
        foreach (var obj in storage.ListObjects(_bucketName, ""))
        {
            if (obj.Name.Contains(fileName))
            {
                return true;
            }
        }
        return false;
    }
    public string GetFileExtension(string fileName)
    {
        var storage = StorageClient.Create();
        foreach (var obj in storage.ListObjects(_bucketName, ""))
        {
            if (obj.Name.Contains(fileName))
            {
                // Return the file extension of the object
                return Path.GetExtension(obj.Name);
            }
        }
        return null;
    }
    private string AccessSecret(string secretId)
    {
        // Create the Secret Manager client.
        SecretManagerServiceClient client = SecretManagerServiceClient.Create();

        // Build the resource name of the secret version.
        SecretVersionName secretVersionName = new SecretVersionName("269636205630", secretId, "latest");

        // Access the secret version.
        AccessSecretVersionResponse result = client.AccessSecretVersion(secretVersionName);

        // Get the secret payload and decode it.
        string payload = result.Payload.Data.ToStringUtf8();

        return payload;
    }
    public void DeleteFile(string fileName)
    {
        var storage = StorageClient.Create();
        string fullFileName = storage.ListObjects(_bucketName, "").FirstOrDefault(obj => obj.Name.Contains(fileName))?.Name;
        // Delete the file from the bucket
        storage.DeleteObject(_bucketName, fullFileName);
    }
}