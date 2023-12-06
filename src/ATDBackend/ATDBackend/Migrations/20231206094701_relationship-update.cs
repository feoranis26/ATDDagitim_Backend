using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class relationshipupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Categories_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Schools_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Seeds_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Schools_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_Category_id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Users_Id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Categories_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Inventory_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Schools_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Users_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_Category_id",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "Category_id",
                table: "Seeds");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Seeds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Categories_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Schools_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Seeds_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Schools_Id",
                table: "Orders",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Id",
                table: "Orders",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_Id",
                table: "Seeds",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Users_Id",
                table: "Seeds",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Categories_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Inventory_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Schools_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Users_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Id",
                table: "Users",
                column: "Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_Id",
                table: "Users",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Categories_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Schools_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Seeds_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Users_Id",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Schools_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_Id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Users_Id",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Categories_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Inventory_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Schools_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_in_Users_Id",
                table: "Seeds_in");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Seeds");

            migrationBuilder.AddColumn<int>(
                name: "Category_id",
                table: "Seeds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_Category_id",
                table: "Seeds",
                column: "Category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Categories_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Schools_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Seeds_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Seeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Users_Id",
                table: "Inventory",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Schools_Id",
                table: "Orders",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Id",
                table: "Orders",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_Category_id",
                table: "Seeds",
                column: "Category_id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Users_Id",
                table: "Seeds",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Categories_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Inventory_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Schools_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_in_Users_Id",
                table: "Seeds_in",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Id",
                table: "Users",
                column: "Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Schools_Id",
                table: "Users",
                column: "Id",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
