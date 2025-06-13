using ArtApi.Services;
using Kolokwium2.Data;
using Kolokwium2.Models;
using Kolokwium2.DTOs;
using Kolokwium2.Services;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly AppDbContext _context;
    
        public ExhibitionService(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<bool> AddExhibitionAsync(ExhibitionCreateDto dto)
        {
            var gallery = await _context.Galleries.FirstOrDefaultAsync(g => g.Name == dto.Gallery);
            if (gallery == null)
            {
                throw new Exception("Gallery not found");
            }

            foreach (var aw in dto.Artworks)
            {
                var artworkExists = await _context.Artworks.AnyAsync(a => a.ArtworkId == aw.ArtworkId);
                if (!artworkExists)
                {
                    throw new Exception("Dzieło nie istnieje");
                }
            }

            if (dto.Artworks == null || dto.Artworks.Count == 0)
            {
                throw new Exception("Lista dzieł nie może być pusta");
            }

            var exhibition = new Exhibition
            {
                Title = dto.Title,
                GalleryId = gallery.GalleryId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                NumberOfArtworks = dto.Artworks.Count,
                ExhibitionArtworks = dto.Artworks.Select(a => new ExhibitionArtwork
                {
                    ArtworkId = a.ArtworkId,
                    InsuranceValue = a.InsuranceValue
                }).ToList()
            };
    
            _context.Exhibitions.Add(exhibition);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

