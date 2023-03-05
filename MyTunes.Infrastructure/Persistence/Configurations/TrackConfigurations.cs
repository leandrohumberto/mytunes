using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTunes.Core.Entities;

namespace MyTunes.Infrastructure.Persistence.Configurations
{
    public class TrackConfigurations : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(track => track.Id);
            builder.Property(track => track.Name)
                .IsRequired();
            builder.Property(track => track.Number)
                .IsRequired();
            builder.Property(track => track.Length)
                .IsRequired();
            builder.Property(track => track.IdAlbum)
                .IsRequired();
        }
    }
}
