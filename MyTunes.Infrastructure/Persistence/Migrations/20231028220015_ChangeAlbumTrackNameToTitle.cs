using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTunes.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAlbumTrackNameToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tracks",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Albums",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tracks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Albums",
                newName: "Name");
        }
    }
}
