using Microsoft.EntityFrameworkCore.Migrations;

namespace coffeeShop.Migrations
{
    public partial class correccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
