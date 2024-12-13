using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityWebhook.Lib.Repository.Migrations
{
    /// <inheritdoc />
    public partial class vcsmetadatachanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VCS",
                table: "RepoScanMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VCS",
                table: "RepoScanMetadata");
        }
    }
}
