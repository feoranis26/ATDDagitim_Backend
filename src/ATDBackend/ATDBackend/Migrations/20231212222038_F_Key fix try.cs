using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class F_Keyfixtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Categories_Category_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Schools_School_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Seeds_Seed_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_User_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Schools_School_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_User_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_Category_Id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Users_User_Id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Categories_Category_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Inventory_School_Seed_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Schools_School_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Users_User_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_School_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Role_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_School_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_Category_Id",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_User_Id",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_Category_Id",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Seeds_in",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "School_Seed_Id",
                table: "Seeds_in",
                newName: "SchoolSeedId");

            migrationBuilder.RenameColumn(
                name: "School_Id",
                table: "Seeds_in",
                newName: "SchoolId");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Seeds_in",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_User_Id",
                table: "Seeds_in",
                newName: "IX_Seeds_in_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_School_Seed_Id",
                table: "Seeds_in",
                newName: "IX_Seeds_in_SchoolSeedId");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_School_Id",
                table: "Seeds_in",
                newName: "IX_Seeds_in_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_Category_Id",
                table: "Seeds_in",
                newName: "IX_Seeds_in_CategoryId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "School_Id",
                table: "Orders",
                newName: "SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_User_Id",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_School_Id",
                table: "Orders",
                newName: "IX_Orders_SchoolId");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Inventory",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Seed_Id",
                table: "Inventory",
                newName: "SeedId");

            migrationBuilder.RenameColumn(
                name: "School_Id",
                table: "Inventory",
                newName: "SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_User_Id",
                table: "Inventory",
                newName: "IX_Inventory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_Seed_Id",
                table: "Inventory",
                newName: "IX_Inventory_SeedId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_School_Id",
                table: "Inventory",
                newName: "IX_Inventory_SchoolId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Inventory",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolId",
                table: "Users",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_Name",
                table: "Seeds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_UserId",
                table: "Seeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CategoryId",
                table: "Inventory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Categories_CategoryId",
                table: "Inventory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Schools_SchoolId",
                table: "Inventory",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Seeds_SeedId",
                table: "Inventory",
                column: "SeedId",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_UserId",
                table: "Inventory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Schools_SchoolId",
                table: "Orders",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Users_UserId",
                table: "Seeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Categories_CategoryId",
                table: "Seeds_in",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Inventory_SchoolSeedId",
                table: "Seeds_in",
                column: "SchoolSeedId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Schools_SchoolId",
                table: "Seeds_in",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Users_UserId",
                table: "Seeds_in",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Categories_CategoryId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Schools_SchoolId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Seeds_SeedId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_UserId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Schools_SchoolId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Users_UserId",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Categories_CategoryId",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Inventory_SchoolSeedId",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Schools_SchoolId",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Users_UserId",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SchoolId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_Name",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_UserId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_CategoryId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Seeds_in",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "SchoolSeedId",
                table: "Seeds_in",
                newName: "School_Seed_Id");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "Seeds_in",
                newName: "School_Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Seeds_in",
                newName: "Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_UserId",
                table: "Seeds_in",
                newName: "IX_Seeds_in_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_SchoolSeedId",
                table: "Seeds_in",
                newName: "IX_Seeds_in_School_Seed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_SchoolId",
                table: "Seeds_in",
                newName: "IX_Seeds_in_School_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Seeds_in_CategoryId",
                table: "Seeds_in",
                newName: "IX_Seeds_in_Category_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "Orders",
                newName: "School_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SchoolId",
                table: "Orders",
                newName: "IX_Orders_School_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Inventory",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "SeedId",
                table: "Inventory",
                newName: "Seed_Id");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "Inventory",
                newName: "School_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_UserId",
                table: "Inventory",
                newName: "IX_Inventory_User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_SeedId",
                table: "Inventory",
                newName: "IX_Inventory_Seed_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_SchoolId",
                table: "Inventory",
                newName: "IX_Inventory_School_Id");

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Inventory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_Id",
                table: "Users",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_School_Id",
                table: "Users",
                column: "School_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_Category_Id",
                table: "Seeds",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_User_Id",
                table: "Seeds",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Category_Id",
                table: "Inventory",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Categories_Category_Id",
                table: "Inventory",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Schools_School_Id",
                table: "Inventory",
                column: "School_Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Seeds_Seed_Id",
                table: "Inventory",
                column: "Seed_Id",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_User_Id",
                table: "Inventory",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Schools_School_Id",
                table: "Orders",
                column: "School_Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_User_Id",
                table: "Orders",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_Category_Id",
                table: "Seeds",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Users_User_Id",
                table: "Seeds",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Categories_Category_Id",
                table: "Seeds_in",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Inventory_School_Seed_Id",
                table: "Seeds_in",
                column: "School_Seed_Id",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Schools_School_Id",
                table: "Seeds_in",
                column: "School_Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Users_User_Id",
                table: "Seeds_in",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_School_Id",
                table: "Users",
                column: "School_Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
