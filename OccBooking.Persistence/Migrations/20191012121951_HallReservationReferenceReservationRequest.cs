using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class HallReservationReferenceReservationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Menus_MenuId",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "AdditionalOptions",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "AmountOfPeople",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "OccasionType",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "Client_Email_Value",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "Client_Name_FirstName",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "Client_Name_LastName",
                table: "HallReservations");

            migrationBuilder.DropColumn(
                name: "Client_PhoneNumber_Value",
                table: "HallReservations");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "HallReservations",
                newName: "ReservationRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_HallReservations_MenuId",
                table: "HallReservations",
                newName: "IX_HallReservations_ReservationRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_ReservationRequests_ReservationRequestId",
                table: "HallReservations",
                column: "ReservationRequestId",
                principalTable: "ReservationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_ReservationRequests_ReservationRequestId",
                table: "HallReservations");

            migrationBuilder.RenameColumn(
                name: "ReservationRequestId",
                table: "HallReservations",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_HallReservations_ReservationRequestId",
                table: "HallReservations",
                newName: "IX_HallReservations_MenuId");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalOptions",
                table: "HallReservations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmountOfPeople",
                table: "HallReservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "HallReservations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "HallReservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OccasionType",
                table: "HallReservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Client_Email_Value",
                table: "HallReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client_Name_FirstName",
                table: "HallReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client_Name_LastName",
                table: "HallReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client_PhoneNumber_Value",
                table: "HallReservations",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Menus_MenuId",
                table: "HallReservations",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
