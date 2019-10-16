using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistance.Migrations
{
    public partial class AddressAddedToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Places",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Province",
                table: "Places",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Places",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_ZipCode",
                table: "Places",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Address_Province",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Places");
        }
    }
}
