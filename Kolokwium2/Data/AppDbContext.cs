using Microsoft.EntityFrameworkCore;
using Kolokwium2.Models;

namespace Kolokwium2.Data
{
    public class AppDbContext : DbContext
{
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Exhibition> Exhibitions { get; set; }
    public DbSet<ExhibitionArtwork> ExhibitionArtworks { get; set; }

    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gallery>().HasKey(g => g.GalleryId);
        modelBuilder.Entity<Gallery>().Property(g => g.Name).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Gallery>().Property(g => g.EstablishedDate).IsRequired();

        modelBuilder.Entity<Artist>().HasKey(a => a.ArtistId);
        modelBuilder.Entity<Artist>().Property(a => a.FirstName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Artist>().Property(a => a.LastName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Artist>().Property(a => a.BirthDate).IsRequired();

        modelBuilder.Entity<Artwork>().HasKey(a => a.ArtworkId);
        modelBuilder.Entity<Artwork>().Property(a => a.Title).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Artwork>().Property(a => a.YearCreated).IsRequired();
        modelBuilder.Entity<Artwork>()
            .HasOne(a => a.Artist)
            .WithMany(ar => ar.Artworks)
            .HasForeignKey(a => a.ArtistId);

        modelBuilder.Entity<Exhibition>().HasKey(e => e.ExhibitionId);
        modelBuilder.Entity<Exhibition>().Property(e => e.Title).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Exhibition>().Property(e => e.StartDate).IsRequired();
        modelBuilder.Entity<Exhibition>().HasOne(e => e.Gallery).WithMany(g => g.Exhibitions).HasForeignKey(e => e.GalleryId);

        modelBuilder.Entity<ExhibitionArtwork>().HasKey(ea => new { ea.ExhibitionId, ea.ArtworkId });
        modelBuilder.Entity<ExhibitionArtwork>().Property(ea => ea.InsuranceValue).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<ExhibitionArtwork>()
            .HasOne(ea => ea.Exhibition)
            .WithMany(e => e.ExhibitionArtworks)
            .HasForeignKey(ea => ea.ExhibitionId);
        modelBuilder.Entity<ExhibitionArtwork>()
            .HasOne(ea => ea.Artwork)
            .WithMany(a => a.ExhibitionArtworks)
            .HasForeignKey(ea => ea.ArtworkId);

        modelBuilder.Entity<Gallery>().HasData(
            new Gallery { GalleryId = 1, Name = "Modern Art Space", EstablishedDate = new DateTime(2001, 9, 12) }
        );
        modelBuilder.Entity<Artist>().HasData(
            new Artist { ArtistId = 1, FirstName = "Pablo", LastName = "Picasso", BirthDate = new DateTime(1881, 10, 25) },
            new Artist { ArtistId = 2, FirstName = "Frida", LastName = "Kahlo", BirthDate = new DateTime(1907, 7, 6) }
        );
        modelBuilder.Entity<Artwork>().HasData(
            new Artwork { ArtworkId = 1, ArtistId = 1, Title = "Guernica", YearCreated = 1937 },
            new Artwork { ArtworkId = 2, ArtistId = 2, Title = "The Two Fridas", YearCreated = 1939 }
        );
        modelBuilder.Entity<Exhibition>().HasData(
            new Exhibition
            {
                ExhibitionId = 1,
                GalleryId = 1,
                Title = "20th Century Giants",
                StartDate = new DateTime(2024, 5, 1),
                EndDate = new DateTime(2024, 9, 1),
                NumberOfArtworks = 2
            }
        );
        modelBuilder.Entity<ExhibitionArtwork>().HasData(
            new ExhibitionArtwork { ExhibitionId = 1, ArtworkId = 1, InsuranceValue = 1000000.00m },
            new ExhibitionArtwork { ExhibitionId = 1, ArtworkId = 2, InsuranceValue = 800000.00m }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("DefaultConnection");
        }
    }
}

}


