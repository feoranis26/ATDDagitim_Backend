using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class seedimg_deltemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Seeds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Seeds",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
