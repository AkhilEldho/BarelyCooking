using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barely_Cooking_API.Migrations
{
    /// <inheritdoc />
    public partial class checkChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartTotal",
                table: "Orders",
                newName: "OrderTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTotal",
                table: "Orders",
                newName: "CartTotal");
        }
    }
}
