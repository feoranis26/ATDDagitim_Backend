using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class Justtestingsomething : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Seeds_in",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Seeds",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Inventory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_Id",
                table: "Users",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_School_Id",
                table: "Users",
                column: "School_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_in_Category_Id",
                table: "Seeds_in",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_in_School_Id",
                table: "Seeds_in",
                column: "School_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_in_School_Seed_Id",
                table: "Seeds_in",
                column: "School_Seed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_in_User_Id",
                table: "Seeds_in",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_Category_Id",
                table: "Seeds",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_User_Id",
                table: "Seeds",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_School_Id",
                table: "Orders",
                column: "School_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User_Id",
                table: "Orders",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Category_Id",
                table: "Inventory",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_School_Id",
                table: "Inventory",
                column: "School_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Seed_Id",
                table: "Inventory",
                column: "Seed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_User_Id",
                table: "Inventory",
                column: "User_Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Seeds_in_Category_Id",
                table: "Seeds_in");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_in_School_Id",
                table: "Seeds_in");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_in_School_Seed_Id",
                table: "Seeds_in");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_in_User_Id",
                table: "Seeds_in");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_Category_Id",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_User_Id",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Orders_School_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_User_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_Category_Id",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_School_Id",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_Seed_Id",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_User_Id",
                table: "Inventory");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Seeds_in",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Seeds",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Inventory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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
    }
}
