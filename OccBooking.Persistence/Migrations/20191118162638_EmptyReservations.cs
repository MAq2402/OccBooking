using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class EmptyReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "HallReservations");

            migrationBuilder.CreateTable(
                name: "EmptyPlaceReservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmptyPlaceReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmptyPlaceReservation_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmptyPlaceReservation_PlaceId",
                table: "EmptyPlaceReservation",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmptyPlaceReservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "HallReservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
