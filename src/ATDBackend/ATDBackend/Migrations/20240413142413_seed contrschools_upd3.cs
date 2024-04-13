using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class seedcontrschools_upd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Seeds_ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.AddColumn<List<int>>(
                name: "ContributedSeedIds",
                table: "Schools",
                type: "integer[]",
                nullable: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolSeed");

            migrationBuilder.DropColumn(
                name: "ContributedSeedIds",
                table: "Schools");

            migrationBuilder.AddColumn<int>(
                name: "ContributorSchoolIds",
                table: "Schools",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ContributorSchoolIds",
                table: "Schools",
                column: "ContributorSchoolIds");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Seeds_ContributorSchoolIds",
                table: "Schools",
                column: "ContributorSchoolIds",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
