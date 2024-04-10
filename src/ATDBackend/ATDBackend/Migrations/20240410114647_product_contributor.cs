using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class product_contributor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Users_UserId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_UserId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seeds");

            migrationBuilder.AddColumn<List<int>>(
                name: "ContributorSchoolIds",
                table: "Seeds",
                type: "integer[]",
                nullable: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Seeds_ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ContributorSchoolIds",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "ContributorSchoolIds",
                table: "Schools");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_UserId",
                table: "Seeds",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Users_UserId",
                table: "Seeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
