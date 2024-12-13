using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecurityWebhook.Lib.Repository.Migrations
{
    /// <inheritdoc />
    public partial class metadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepoScanMetadata",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectKey = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepoScanMetadata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepoScanMetadata_RepositoryMaster_RepoId",
                        column: x => x.RepoId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepoScanMetadata_RepoId",
                table: "RepoScanMetadata",
                column: "RepoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepoScanMetadata");
        }
    }
}
