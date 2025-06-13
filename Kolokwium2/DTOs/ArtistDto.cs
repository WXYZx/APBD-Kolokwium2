namespace Kolokwium2.DTOs
{
    public class ArtistDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
    
    public class ArtworkInExhibitionDto
    {
        public string Title { get; set; }
        public int YearCreated { get; set; }
        public decimal InsuranceValue { get; set; }
        public ArtistDto Artist { get; set; }
    }
    
    public class ExhibitionCreateDto
    {
        public string Title { get; set; }
        public string Gallery { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ExhibitionCreateArtworkDto> Artworks { get; set; }
    }

    public class ExhibitionCreateArtworkDto
    {
        public int ArtworkId { get; set; }
        public decimal InsuranceValue { get; set; }
    }
    
    public class ExhibitionInGalleryDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NumberOfArtworks { get; set; }
        public List<ArtworkInExhibitionDto> Artworks { get; set; }
    }
    
    public class GalleryDetailsDto
    {
        public int GalleryId { get; set; }
        public string Name { get; set; }
        public DateTime EstablishedDate { get; set; }
        public List<ExhibitionInGalleryDto> Exhibitions { get; set; }
    }
}

