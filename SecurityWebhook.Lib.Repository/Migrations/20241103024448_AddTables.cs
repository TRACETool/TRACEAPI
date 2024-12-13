using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SecurityWebhook.Lib.Repository.Triggers;

#nullable disable

namespace SecurityWebhook.Lib.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContributorMaster",
                columns: table => new
                {
                    ContributorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContributorName = table.Column<string>(type: "text", nullable: false),
                    ContributorEmail = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorMaster", x => x.ContributorId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMaster",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    RoleDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMaster", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RepositoryMaster",
                columns: table => new
                {
                    RepoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoName = table.Column<string>(type: "text", nullable: false),
                    RepoUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryMaster", x => x.RepoId);
                    table.ForeignKey(
                        name: "FK_RepositoryMaster_ContributorMaster_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "ContributorMaster",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anomalies",
                columns: table => new
                {
                    AnomaliesId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AnomalyType = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ActionTaken = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PreviousHash = table.Column<string>(type: "text", nullable: false),
                    CurrentHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anomalies", x => x.AnomaliesId);
                    table.ForeignKey(
                        name: "FK_Anomalies_ContributorMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "ContributorMaster",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anomalies_RepositoryMaster_RepoId",
                        column: x => x.RepoId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributorRepositories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContributorId = table.Column<long>(type: "bigint", nullable: false),
                    RepositoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorRepositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributorRepositories_ContributorMaster_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "ContributorMaster",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributorRepositories_RepositoryMaster_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScanDetails",
                columns: table => new
                {
                    ScanId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Event = table.Column<int>(type: "integer", nullable: false),
                    RedMarked = table.Column<int>(type: "integer", nullable: false),
                    YellowMarked = table.Column<int>(type: "integer", nullable: false),
                    WhiteMarked = table.Column<int>(type: "integer", nullable: false),
                    TotalVulnerabilities = table.Column<int>(type: "integer", nullable: false),
                    RawData = table.Column<string>(type: "text", nullable: false),
                    ScanTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeElapsed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousHash = table.Column<string>(type: "text", nullable: false),
                    CurrentHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanDetails", x => x.ScanId);
                    table.ForeignKey(
                        name: "FK_ScanDetails_ContributorMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "ContributorMaster",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScanDetails_RepositoryMaster_RepoId",
                        column: x => x.RepoId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScanFrequencyMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoId = table.Column<long>(type: "bigint", nullable: false),
                    Events = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanFrequencyMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScanFrequencyMaster_RepositoryMaster_RepoId",
                        column: x => x.RepoId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vulnerabilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepoId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Snapshot = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PreviousHash = table.Column<string>(type: "text", nullable: false),
                    CurrentHash = table.Column<string>(type: "text", nullable: false),
                    DetectionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vulnerabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vulnerabilities_ContributorMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "ContributorMaster",
                        principalColumn: "ContributorId");
                    table.ForeignKey(
                        name: "FK_Vulnerabilities_RepositoryMaster_RepoId",
                        column: x => x.RepoId,
                        principalTable: "RepositoryMaster",
                        principalColumn: "RepoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anomalies_RepoId",
                table: "Anomalies",
                column: "RepoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anomalies_UserId",
                table: "Anomalies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorRepositories_ContributorId",
                table: "ContributorRepositories",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorRepositories_RepositoryId",
                table: "ContributorRepositories",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryMaster_CreatedBy",
                table: "RepositoryMaster",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ScanDetails_RepoId",
                table: "ScanDetails",
                column: "RepoId");

            migrationBuilder.CreateIndex(
                name: "IX_ScanDetails_UserId",
                table: "ScanDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScanFrequencyMaster_RepoId",
                table: "ScanFrequencyMaster",
                column: "RepoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vulnerabilities_RepoId",
                table: "Vulnerabilities",
                column: "RepoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vulnerabilities_UserId",
                table: "Vulnerabilities",
                column: "UserId");

            migrationBuilder.Sql(TriggerQueries.CreateScanForbidding);
            migrationBuilder.Sql(TriggerQueries.CreateVulnerabilitiesForbidding);
            migrationBuilder.Sql(TriggerQueries.CreateAnomaliesForbidding);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anomalies");

            migrationBuilder.DropTable(
                name: "ContributorRepositories");

            migrationBuilder.DropTable(
                name: "RoleMaster");

            migrationBuilder.DropTable(
                name: "ScanDetails");

            migrationBuilder.DropTable(
                name: "ScanFrequencyMaster");

            migrationBuilder.DropTable(
                name: "Vulnerabilities");

            migrationBuilder.DropTable(
                name: "RepositoryMaster");

            migrationBuilder.DropTable(
                name: "ContributorMaster");

            migrationBuilder.Sql(TriggerQueries.DropScanForbiding);
            migrationBuilder.Sql(TriggerQueries.DropVulnerabilitiesForbiding);
            migrationBuilder.Sql(TriggerQueries.DropAnomaliesForbiding);
        }
    }
}
