using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Barely_Cooking_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedCakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Cakes",
                newName: "ImageFile");

            migrationBuilder.InsertData(
                table: "Cakes",
                columns: new[] { "CakeId", "Category", "Description", "ImageFile", "Name", "Price", "SpecialTags" },
                values: new object[,]
                {
                    { 1, "Wedding", "Cake", "https://barelycooking.blob.core.windows.net/cakephotos/cake.jpg", "Fast Slow-Ride", 209.0, "Top Rated" },
                    { 2, "Chocolate", "Cake", "https://barelycooking.blob.core.windows.net/cakephotos/cake2.jpg", "Fast Slow-Ride", 209.0, "Top Rated" },
                    { 3, "Wedding", "Cake", "https://barelycooking.blob.core.windows.net/cakephotos/cake3.jpg", "Fast Slow-Ride", 209.0, "" },
                    { 4, "Chocolate", "Cake", "https://barelycooking.blob.core.windows.net/cakephotos/cake4.jpg", "Fast Slow-Ride", 209.0, "Top Rated" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cakes",
                keyColumn: "CakeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cakes",
                keyColumn: "CakeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cakes",
                keyColumn: "CakeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cakes",
                keyColumn: "CakeId",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "ImageFile",
                table: "Cakes",
                newName: "Image");
        }
    }
}
