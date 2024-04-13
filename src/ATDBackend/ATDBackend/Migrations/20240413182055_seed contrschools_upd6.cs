using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class seedcontrschools_upd6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeedContributor_Seeds_SeedContributorIds",
                table: "SeedContributor");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Schools_ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_SeedContributor_SeedContributorIds",
                table: "SeedContributor");

            migrationBuilder.DropColumn(
                name: "ContributedSeedIds",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "SeedContributorIds",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "SeedContributorIds",
                table: "SeedContributor");

            migrationBuilder.DropColumn(
                name: "ContributedSeedIds",
                table: "Schools");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContributedSeedIds",
                table: "Seeds",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "SeedContributorIds",
                table: "Seeds",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "SeedContributorIds",
                table: "SeedContributor",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "ContributedSeedIds",
                table: "Schools",
                type: "integer[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_ContributedSeedIds",
                table: "Seeds",
                column: "ContributedSeedIds");

            migrationBuilder.CreateIndex(
                name: "IX_SeedContributor_SeedContributorIds",
                table: "SeedContributor",
                column: "SeedContributorIds");

            migrationBuilder.AddForeignKey(
                name: "FK_SeedContributor_Seeds_SeedContributorIds",
                table: "SeedContributor",
                column: "SeedContributorIds",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Schools_ContributedSeedIds",
                table: "Seeds",
                column: "ContributedSeedIds",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
