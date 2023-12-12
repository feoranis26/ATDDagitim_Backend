using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class F_Keyfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role_Id",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "School_Id",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Seeds_in",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "School_Id",
                table: "Seeds_in",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "School_Seed_Id",
                table: "Seeds_in",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Seeds_in",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "School_Id",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Inventory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "School_Id",
                table: "Inventory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seed_Id",
                table: "Inventory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Inventory",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "School_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Seeds_in");

            migrationBuilder.DropColumn(
                name: "School_Id",
                table: "Seeds_in");

            migrationBuilder.DropColumn(
                name: "School_Seed_Id",
                table: "Seeds_in");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Seeds_in");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "School_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "School_Id",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Seed_Id",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Inventory");
        }
    }
}
