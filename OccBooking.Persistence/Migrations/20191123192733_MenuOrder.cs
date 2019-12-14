using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistence.Migrations
{
    public partial class MenuOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Menus_MenuId",
                table: "ReservationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_MenuId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "CostPerPerson",
                table: "Places");

            migrationBuilder.CreateTable(
                name: "MenuOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: true),
                    AmountOfPeople = table.Column<int>(nullable: false),
                    ReservationRequestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuOrders_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuOrders_ReservationRequests_ReservationRequestId",
                        column: x => x.ReservationRequestId,
                        principalTable: "ReservationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuOrders_MenuId",
                table: "MenuOrders",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuOrders_ReservationRequestId",
                table: "MenuOrders",
                column: "ReservationRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "ReservationRequests",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerPerson",
                table: "Places",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_MenuId",
                table: "ReservationRequests",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Menus_MenuId",
                table: "ReservationRequests",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
