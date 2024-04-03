using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class basketmorefixy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketSeed");

            migrationBuilder.AddColumn<string>(
                name: "BasketJson",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketJson",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "BasketSeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketSeed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketSeed_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketSeed_UserId",
                table: "BasketSeed",
                column: "UserId");
        }
    }
}
