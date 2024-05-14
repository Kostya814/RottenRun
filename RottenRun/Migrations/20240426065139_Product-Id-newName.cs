using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RottenRun.Migrations
{
    /// <inheritdoc />
    public partial class ProductIdnewName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductID",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_Products_ProductID",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductID",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Ratings",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProductID",
                table: "Ratings",
                newName: "IX_Ratings_ProductId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "FavoriteProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_ProductID",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Baskets",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductID",
                table: "Baskets",
                newName: "IX_Baskets_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Products_ProductId",
                table: "FavoriteProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_Products_ProductId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Ratings",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                newName: "IX_Ratings_ProductID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "FavoriteProducts",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteProducts_ProductId",
                table: "FavoriteProducts",
                newName: "IX_FavoriteProducts_ProductID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Baskets",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                newName: "IX_Baskets_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductID",
                table: "Baskets",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Products_ProductID",
                table: "FavoriteProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductID",
                table: "Ratings",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
