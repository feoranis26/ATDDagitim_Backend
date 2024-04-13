using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class seedcontrschools_upd5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolSeed");

            migrationBuilder.RenameColumn(
                name: "ContributorSchoolIds",
                table: "Seeds",
                newName: "SeedContributorIds");

            migrationBuilder.AddColumn<int>(
                name: "ContributedSeedIds",
                table: "Seeds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeedContributor",
                columns: table => new
                {
                    SeedId = table.Column<int>(type: "integer", nullable: false),
                    SchoolId = table.Column<int>(type: "integer", nullable: false),
                    SeedContributorIds = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedContributor", x => new { x.SeedId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_SeedContributor_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeedContributor_Seeds_SeedContributorIds",
                        column: x => x.SeedContributorIds,
                        principalTable: "Seeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeedContributor_Seeds_SeedId",
                        column: x => x.SeedId,
                        principalTable: "Seeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_ContributedSeedIds",
                table: "Seeds",
                column: "ContributedSeedIds");

            migrationBuilder.CreateIndex(
                name: "IX_SeedContributor_SchoolId",
                table: "SeedContributor",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedContributor_SeedContributorIds",
                table: "SeedContributor",
                column: "SeedContributorIds");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Schools_ContributedSeedIds",
                table: "Seeds",
                column: "ContributedSeedIds",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Schools_ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.DropTable(
                name: "SeedContributor");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.RenameColumn(
                name: "SeedContributorIds",
                table: "Seeds",
                newName: "ContributorSchoolIds");

            migrationBuilder.CreateTable(
                name: "SchoolSeed",
                columns: table => new
                {
                    ContributedSeedIds = table.Column<int>(type: "integer", nullable: false),
                    ContributorSchoolIds = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSeed", x => new { x.ContributedSeedIds, x.ContributorSchoolIds });
                    table.ForeignKey(
                        name: "FK_SchoolSeed_Schools_ContributedSeedIds",
                        column: x => x.ContributedSeedIds,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolSeed_Seeds_ContributorSchoolIds",
                        column: x => x.ContributorSchoolIds,
                        principalTable: "Seeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSeed_ContributorSchoolIds",
                table: "SchoolSeed",
                column: "ContributorSchoolIds");
        }
    }
}
