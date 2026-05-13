namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    public Task<string> UploadToBlobAsync(Stream data, string fileName, CancellationToken cancellationToken = default);
}
