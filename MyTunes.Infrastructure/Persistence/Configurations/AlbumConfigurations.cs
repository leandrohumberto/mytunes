using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTunes.Core.Entities;

namespace MyTunes.Infrastructure.Persistence.Configurations
{
    public class AlbumConfigurations : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(album => album.Id);
            builder.Property(album => album.Title)
                .IsRequired();
            builder.Property(album => album.Format)
                .IsRequired();
            builder.Property(album => album.Year)
                .IsRequired();
            builder.Property(album => album.IdArtist)
                .IsRequired();

            builder
                .HasMany(album => album.Tracklist)
                .WithOne(track => track.Album)
                .HasForeignKey(track => track.IdAlbum)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
