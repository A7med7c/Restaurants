using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configurations;

namespace Restaurants.Infrastructure.Authorization.Services;

internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

    public async Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        var blobStorageClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobStorageClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(data, overwrite: true);

        var blobUrl = blobClient.Uri.ToString();
        return blobUrl;
    }
}
