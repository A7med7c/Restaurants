using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UploadLogo;

public class UploadRestaurantLogoCommand : IRequest
{
    public int RestaurantId { get; set; }
    public string FileName { get; set; } = default!;
    public Stream FileContent { get; set; } = default!;
}
