using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlightsMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Location_DestinationLocationId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Location_SourceLocationId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSchedule_Flight_FlightId",
                table: "FlightSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_SourceLocationId",
                table: "Flights",
                newName: "IX_Flights_SourceLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Flight_DestinationLocationId",
                table: "Flights",
                newName: "IX_Flights_DestinationLocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Locations_DestinationLocationId",
                table: "Flights",
                column: "DestinationLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Locations_SourceLocationId",
                table: "Flights",
                column: "SourceLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSchedule_Flights_FlightId",
                table: "FlightSchedule",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Locations_DestinationLocationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Locations_SourceLocationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSchedule_Flights_FlightId",
                table: "FlightSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_SourceLocationId",
                table: "Flight",
                newName: "IX_Flight_SourceLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_DestinationLocationId",
                table: "Flight",
                newName: "IX_Flight_DestinationLocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Location_DestinationLocationId",
                table: "Flight",
                column: "DestinationLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Location_SourceLocationId",
                table: "Flight",
                column: "SourceLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSchedule_Flight_FlightId",
                table: "FlightSchedule",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
