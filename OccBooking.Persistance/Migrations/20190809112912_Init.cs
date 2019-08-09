using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OccBooking.Persistance.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name_FirstName = table.Column<string>(nullable: true),
                    Name_LastName = table.Column<string>(nullable: true),
                    Email_Value = table.Column<string>(nullable: true),
                    PhoneNumber_Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AvailableOccasionTypes = table.Column<string>(nullable: true),
                    AdditionalOptions = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    HasRooms = table.Column<bool>(nullable: false),
                    CostPerPerson = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    CostForPerson = table.Column<decimal>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalOptions = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Client_Name_FirstName = table.Column<string>(nullable: true),
                    Client_Name_LastName = table.Column<string>(nullable: true),
                    Client_Email_Value = table.Column<string>(nullable: true),
                    Client_PhoneNumber_Value = table.Column<string>(nullable: true),
                    AmountOfPeople = table.Column<int>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    IsRejected = table.Column<bool>(nullable: false),
                    OccasionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    ReservationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halls_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Halls_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HallJoins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstHallId = table.Column<Guid>(nullable: true),
                    SecondHallId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallJoins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HallJoins_Halls_FirstHallId",
                        column: x => x.FirstHallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HallJoins_Halls_SecondHallId",
                        column: x => x.SecondHallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HallReservations",
                columns: table => new
                {
                    HallId = table.Column<Guid>(nullable: false),
                    ReservationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallReservations", x => new { x.HallId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_HallReservations_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HallJoins_FirstHallId",
                table: "HallJoins",
                column: "FirstHallId");

            migrationBuilder.CreateIndex(
                name: "IX_HallJoins_SecondHallId",
                table: "HallJoins",
                column: "SecondHallId");

            migrationBuilder.CreateIndex(
                name: "IX_HallReservations_ReservationId",
                table: "HallReservations",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_PlaceId",
                table: "Halls",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_ReservationId",
                table: "Halls",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MenuId",
                table: "Meals",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PlaceId",
                table: "Menus",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_OwnerId",
                table: "Places",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MenuId",
                table: "Reservations",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HallJoins");

            migrationBuilder.DropTable(
                name: "HallReservations");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
