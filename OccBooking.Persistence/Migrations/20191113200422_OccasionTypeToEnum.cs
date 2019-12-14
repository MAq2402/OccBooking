using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class OccasionTypeToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccasionType",
                table: "ReservationRequests");

            migrationBuilder.AddColumn<string>(
                name: "OccasionType_Name",
                table: "ReservationRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccasionType_Name",
                table: "ReservationRequests");

            migrationBuilder.AddColumn<int>(
                name: "OccasionType",
                table: "ReservationRequests",
                nullable: false,
                defaultValue: 0);
        }
    }
}
