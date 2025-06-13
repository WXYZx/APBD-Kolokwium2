using Kolokwium2.DTOs;

namespace Kolokwium2.Services
{
    
public interface IGalleryService
{
    Task<GalleryDetailsDto?> GetGalleryWithExhibitionsAsync(int galleryId);
}
}
