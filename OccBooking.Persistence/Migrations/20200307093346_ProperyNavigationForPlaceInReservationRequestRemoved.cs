using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class ProperyNavigationForPlaceInReservationRequestRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Owners_OwnerId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Places_PlaceId",
                table: "ReservationRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaceId",
                table: "ReservationRequests",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Places",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Owners_OwnerId",
                table: "Places",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Places_PlaceId",
                table: "ReservationRequests",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Owners_OwnerId",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Places_PlaceId",
                table: "ReservationRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaceId",
                table: "ReservationRequests",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Places",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Owners_OwnerId",
                table: "Places",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Places_PlaceId",
                table: "ReservationRequests",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
