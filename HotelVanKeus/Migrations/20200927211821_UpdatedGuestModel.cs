using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelVanKeus.Migrations
{
    public partial class UpdatedGuestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Guests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Guests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Guests");
        }
    }
}
