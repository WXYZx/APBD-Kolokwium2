namespace Kolokwium2.Models
{
    public class Artwork
    {
        public int ArtworkId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public int YearCreated { get; set; }
        public Artist Artist { get; set; }
        public ICollection<ExhibitionArtwork> ExhibitionArtworks { get; set; }
    }
}

