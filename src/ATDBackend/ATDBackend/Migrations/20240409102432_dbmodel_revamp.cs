using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class dbmodel_revamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "School_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Seeds");

            migrationBuilder.RenameColumn(
                name: "Role_name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Role_name",
                table: "Roles",
                newName: "IX_Roles_RoleName");

            migrationBuilder.AddColumn<decimal>(
                name: "Permissions",
                table: "Roles",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Username",
                table: "Users",
                columns: new[] { "Email", "Username" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Role_name");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_RoleName",
                table: "Roles",
                newName: "IX_Roles_Role_name");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }
    }
}
