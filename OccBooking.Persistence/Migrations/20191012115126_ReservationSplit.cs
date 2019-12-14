using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class ReservationSplit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Reservations_ReservationId",
                table: "HallReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Menus_MenuId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations");

            migrationBuilder.DropIndex(
                name: "IX_HallReservations_ReservationId",
                table: "HallReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "HallReservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "ReservationRequests");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PlaceId",
                table: "ReservationRequests",
                newName: "IX_ReservationRequests_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_MenuId",
                table: "ReservationRequests",
                newName: "IX_ReservationRequests_MenuId");

            migrationBuilder.AlterColumn<Guid>(
                name: "HallId",
                table: "HallReservations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HallReservations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "HallReservations",
                nullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationRequests",
                table: "ReservationRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HallReservations_HallId",
                table: "HallReservations",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_HallReservations_MenuId",
                table: "HallReservations",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Menus_MenuId",
                table: "HallReservations",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Menus_MenuId",
                table: "ReservationRequests",
                column: "MenuId",
                principalTable: "Menus",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HallReservations_Menus_MenuId",
                table: "HallReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Menus_MenuId",
                table: "ReservationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Places_PlaceId",
                table: "ReservationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations");

            migrationBuilder.DropIndex(
                name: "IX_HallReservations_HallId",
                table: "HallReservations");

            migrationBuilder.DropIndex(
                name: "IX_HallReservations_MenuId",
                table: "HallReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationRequests",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "Id",
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
                name: "MenuId",
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

            migrationBuilder.RenameTable(
                name: "ReservationRequests",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationRequests_PlaceId",
                table: "Reservations",
                newName: "IX_Reservations_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationRequests_MenuId",
                table: "Reservations",
                newName: "IX_Reservations_MenuId");

            migrationBuilder.AlterColumn<Guid>(
                name: "HallId",
                table: "HallReservations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "HallReservations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HallReservations",
                table: "HallReservations",
                columns: new[] { "HallId", "ReservationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HallReservations_ReservationId",
                table: "HallReservations",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Halls_HallId",
                table: "HallReservations",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HallReservations_Reservations_ReservationId",
                table: "HallReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Menus_MenuId",
                table: "Reservations",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
