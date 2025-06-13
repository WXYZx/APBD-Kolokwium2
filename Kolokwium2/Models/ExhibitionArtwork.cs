namespace Kolokwium2.Models
{
    public class ExhibitionArtwork
    {
        public int ExhibitionId { get; set; }
        public int ArtworkId { get; set; }
        public decimal InsuranceValue { get; set; }
        public Exhibition Exhibition { get; set; }
        public Artwork Artwork { get; set; }
    }
}
