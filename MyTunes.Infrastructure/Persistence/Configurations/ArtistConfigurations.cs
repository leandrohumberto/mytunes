using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTunes.Core.Entities;

namespace MyTunes.Infrastructure.Persistence.Configurations
{
    public class ArtistConfigurations : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(artist => artist.Id);
            builder.Property(artist => artist.Name)
                .IsRequired();

            builder
                .HasMany(artist => artist.Albums)
                .WithOne(album => album.Artist)
                .HasForeignKey(album => album.IdArtist)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
