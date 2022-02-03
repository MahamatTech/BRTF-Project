using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BrtfProject.Data.BRMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepeatRooms",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(nullable: false),
                    Duration = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatRooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRoomDetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationId = table.Column<int>(nullable: false),
                    NumberOfRoomsBooked = table.Column<int>(nullable: false),
                    ReservationStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRoomDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoomRules",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<int>(nullable: false),
                    RuleName = table.Column<string>(maxLength: 100, nullable: false),
                    RuleDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Term = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Purge = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    description = table.Column<string>(nullable: true),
                    IsEnable = table.Column<bool>(nullable: false),
                    capacity = table.Column<string>(maxLength: 100, nullable: false),
                    EMail = table.Column<string>(maxLength: 255, nullable: false),
                    RepeatEndDate = table.Column<DateTime>(nullable: false),
                    BookingID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    room_ID = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false),
                    StartdateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    BookerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_room_ID",
                        column: x => x.room_ID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_room_ID",
                        column: x => x.room_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ReservedBy = table.Column<string>(maxLength: 50, nullable: false),
                    room_ID = table.Column<int>(nullable: false),
                    User_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_room_ID",
                        column: x => x.room_ID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_room_ID",
                        column: x => x.room_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_room_ID",
                table: "Bookings",
                column: "room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_room_ID",
                table: "Reservations",
                column: "room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BookingID",
                table: "Rooms",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentID",
                table: "Users",
                column: "StudentID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_BookingID",
                table: "Rooms",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_room_ID",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "RepeatRooms");

            migrationBuilder.DropTable(
                name: "ReservationRoomDetails");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RoomRules");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
