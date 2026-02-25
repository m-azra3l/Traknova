using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelematicService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telematics",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    TripId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Altitude = table.Column<int>(type: "int", nullable: false),
                    Angle = table.Column<int>(type: "int", nullable: false),
                    Satellites = table.Column<int>(type: "int", nullable: false),
                    GpsSpeed = table.Column<int>(type: "int", nullable: false),
                    CanSpeed = table.Column<int>(type: "int", nullable: false),
                    CanEngineRpm = table.Column<int>(type: "int", nullable: false),
                    EngineTemperature = table.Column<int>(type: "int", nullable: false),
                    CanTotalMileageMetres = table.Column<int>(type: "int", nullable: false),
                    CanFuelLevelLitres = table.Column<int>(type: "int", nullable: false),
                    CanFuelPercentage = table.Column<int>(type: "int", nullable: false),
                    TripOdemeter = table.Column<int>(type: "int", nullable: false),
                    TotalOdemeterMetres = table.Column<int>(type: "int", nullable: false),
                    BatteryLevel = table.Column<int>(type: "int", nullable: false),
                    BatteryVoltage = table.Column<float>(type: "real", nullable: false),
                    BatteryCurrent = table.Column<float>(type: "real", nullable: false),
                    GsmSignal = table.Column<int>(type: "int", nullable: false),
                    IgnitionActive = table.Column<bool>(type: "bit", nullable: false),
                    IsMoving = table.Column<bool>(type: "bit", nullable: false),
                    DigitalOutput1 = table.Column<bool>(type: "bit", nullable: false),
                    DigitalOutput2 = table.Column<bool>(type: "bit", nullable: false),
                    DigitalOutput3 = table.Column<bool>(type: "bit", nullable: false),
                    OilIndicatorActive = table.Column<bool>(type: "bit", nullable: false),
                    TowingAlert = table.Column<bool>(type: "bit", nullable: false),
                    IdlingAlert = table.Column<bool>(type: "bit", nullable: false),
                    OverSpeedingAlert = table.Column<bool>(type: "bit", nullable: false),
                    UnplugAlert = table.Column<bool>(type: "bit", nullable: false),
                    EcoScore = table.Column<int>(type: "int", nullable: false),
                    DataMode = table.Column<int>(type: "int", nullable: false),
                    NetworkType = table.Column<int>(type: "int", nullable: false),
                    GreenDrivingType = table.Column<int>(type: "int", nullable: false),
                    EcoDurationInMS = table.Column<int>(type: "int", nullable: false),
                    ObdEngineRpm = table.Column<int>(type: "int", nullable: false),
                    OBDOEMTotalMileageKM = table.Column<int>(type: "int", nullable: false),
                    ObdOemFuelLevelLitres = table.Column<int>(type: "int", nullable: false),
                    ObdFuelLevelPercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telematics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrashDetections",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    ImpactMagnitude = table.Column<float>(type: "real", nullable: false),
                    Axis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelematicsId = table.Column<byte[]>(type: "binary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrashDetections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrashDetections_Telematics_TelematicsId",
                        column: x => x.TelematicsId,
                        principalTable: "Telematics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrashDetections_TelematicsId",
                table: "CrashDetections",
                column: "TelematicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Telematics_TimeStamp",
                table: "Telematics",
                column: "TimeStamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrashDetections");

            migrationBuilder.DropTable(
                name: "Telematics");
        }
    }
}
