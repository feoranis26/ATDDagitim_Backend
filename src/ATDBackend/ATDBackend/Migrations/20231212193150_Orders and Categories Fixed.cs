using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATDBackend.Migrations
{
    /// <inheritdoc />
    public partial class OrdersandCategoriesFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeds_Orders_OrderId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds");

            migrationBuilder.DropIndex(
                name: "IX_Seeds_OrderId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Seeds");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Seeds");

            migrationBuilder.AddColumn<List<int>>(
                name: "Seeds",
                table: "Orders",
                type: "integer[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seeds",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Seeds",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Seeds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_CategoryId",
                table: "Seeds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Seeds_OrderId",
                table: "Seeds",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Categories_CategoryId",
                table: "Seeds",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeds_Orders_OrderId",
                table: "Seeds",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
