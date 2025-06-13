using Kolokwium2.DTOs;
using System.Threading.Tasks;

namespace ArtApi.Services
{
    public interface IExhibitionService
    {
        Task<bool> AddExhibitionAsync(ExhibitionCreateDto dto);
    }
}

