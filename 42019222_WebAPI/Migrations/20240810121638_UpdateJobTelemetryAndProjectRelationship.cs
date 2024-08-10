using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _42019222_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobTelemetryAndProjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Config");

            migrationBuilder.EnsureSchema(
                name: "Telemetry");

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Config",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOnboarded = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Process",
                schema: "Config",
                columns: table => new
                {
                    ProcessID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ProcessName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ProcessType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    IsFramework = table.Column<bool>(type: "bit", nullable: false),
                    RequiresDefaultConfig = table.Column<bool>(type: "bit", nullable: false),
                    Submitter = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ProcessConfigURL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ReportURL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultGeography = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: "Global"),
                    DefaultBusinessFunction = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: "Unspecified"),
                    Platform = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ProcessID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Config",
                columns: table => new
                {
                    ProjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ProjectName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ProjectDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ProjectCreationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(dateadd(hour,(2),getdate()))"),
                    ProjectStatus = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "JobTelemetry",
                schema: "Telemetry",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProccesID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    JobID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    QueueID = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StepDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    HumanTime = table.Column<int>(type: "int", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueReference = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UniqueReferenceType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    BusinessFunction = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Geography = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ExcludeFromTimeSaving = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    AdditionalInfo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTelemetry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobTelemetry_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Config",
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTelemetry_ProjectId",
                schema: "Telemetry",
                table: "JobTelemetry",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "JobTelemetry",
                schema: "Telemetry");

            migrationBuilder.DropTable(
                name: "Process",
                schema: "Config");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Config");
        }
    }
}
