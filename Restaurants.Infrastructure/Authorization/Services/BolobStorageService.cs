using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configurations;

namespace Restaurants.Infrastructure.Authorization.Services;

internal class BlobStorageService(
    IOptions<BlobStorageSettings> blobStorageSettingsOptions,
    ILogger<BlobStorageService> logger) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

    public async Task<string> UploadToBlobAsync(Stream data, string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_blobStorageSettings.ConnectionString))
            throw new InvalidOperationException("Blob storage connection string is missing.");

        if (string.IsNullOrWhiteSpace(_blobStorageSettings.LogosContainerName))
            throw new InvalidOperationException("Blob storage logos container name is missing.");

        var blobClientOptions = new BlobClientOptions
        {
            Retry =
            {
                MaxRetries = 2,
                NetworkTimeout = TimeSpan.FromSeconds(30)
            }
        };

        var blobStorageClient = new BlobServiceClient(_blobStorageSettings.ConnectionString, blobClientOptions);
        var containerClient = blobStorageClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(fileName);

        logger.LogInformation("Uploading blob {FileName} to container {ContainerName}", fileName, _blobStorageSettings.LogosContainerName);

        try
        {
            await blobClient.UploadAsync(data, overwrite: true, cancellationToken);
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogError(
                ex,
                "Failed to upload blob {FileName} to container {ContainerName}",
                fileName,
                _blobStorageSettings.LogosContainerName);

            throw new ExternalStorageException("Logo storage is currently unavailable. Please check Azure Blob Storage connectivity and configuration.", ex);
        }

        logger.LogInformation("Uploaded blob {FileName} successfully", fileName);

        return blobClient.Uri.ToString();
    }
}


