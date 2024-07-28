namespace Barely_Cooking_API.Services
{
    public interface ImageBlobService
    {
        Task<string> GetBlob(string blobName, string containerName);
        Task<bool> DeleteBlob(string blobName, string containerName);
        Task<string> UploadBlob(string blobName, string containerName, IFormFile formFile);
    }
}
