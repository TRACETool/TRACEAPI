using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityWebhook.Lib.Repository.Migrations
{
    /// <inheritdoc />
    public partial class metadatachanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "RepoScanMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "RepoScanMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "RepoScanMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "RepoScanMetadata");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "RepoScanMetadata");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "RepoScanMetadata");
        }
    }
}
