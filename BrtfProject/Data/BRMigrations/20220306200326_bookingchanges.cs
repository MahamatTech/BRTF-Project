using Microsoft.EntityFrameworkCore.Migrations;

namespace BrtfProject.Data.BRMigrations
{
    public partial class bookingchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Bookings",
                nullable: true, 
                maxLength: 50);
            migrationBuilder.AddColumn<string>(
                  name: "LastName",
                  table: "Bookings",
                  nullable: true,
                  maxLength: 50);
            migrationBuilder.AddColumn<string>(
               name: "FirstName",
               table: "Bookings",
               nullable: true,
               maxLength: 50);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Bookings");

            migrationBuilder.DropColumn(
               name: "LastName",
               table: "Bookings");

            migrationBuilder.DropColumn(
               name: "FirstName",
               table: "Bookings");
        }
    }
}
