using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Kolokwium2.Services;
using Microsoft.EntityFrameworkCore;

namespace ArtApi.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly AppDbContext _context;
    
        public GalleryService(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<GalleryDetailsDto?> GetGalleryWithExhibitionsAsync(int galleryId)
        {
            var gallery = await _context.Galleries
                .Include(g => g.Exhibitions)
                .ThenInclude(e => e.ExhibitionArtworks)
                .ThenInclude(ea => ea.Artwork)
                .ThenInclude(a => a.Artist)
                .FirstOrDefaultAsync(g => g.GalleryId == galleryId);

            if (gallery == null)
            {
                return null;
            }
    
            return new GalleryDetailsDto
            {
                GalleryId = gallery.GalleryId,
                Name = gallery.Name,
                EstablishedDate = gallery.EstablishedDate,
                Exhibitions = gallery.Exhibitions.Select(e => new ExhibitionInGalleryDto
                {
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    NumberOfArtworks = e.NumberOfArtworks,
                    Artworks = e.ExhibitionArtworks.Select(ea => new ArtworkInExhibitionDto
                    {
                        Title = ea.Artwork.Title,
                        YearCreated = ea.Artwork.YearCreated,
                        InsuranceValue = ea.InsuranceValue,
                        Artist = new ArtistDto
                        {
                            FirstName = ea.Artwork.Artist.FirstName,
                            LastName = ea.Artwork.Artist.LastName,
                            BirthDate = ea.Artwork.Artist.BirthDate
                        }
                    }).ToList()
                }).ToList()
            };
        }
    }
}

