using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassesStudentsAPI.Migrations
{
    public partial class Init01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tridy",
                columns: table => new
                {
                    TridaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KodoveOznaceni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumVzniku = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumUkonceni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrovenVzdelani = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tridy", x => x.TridaId);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonniCislo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pohlavi = table.Column<int>(type: "int", nullable: false),
                    DatumNarozeni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TridaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Studenti_Tridy_TridaId",
                        column: x => x.TridaId,
                        principalTable: "Tridy",
                        principalColumn: "TridaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_TridaId",
                table: "Studenti",
                column: "TridaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Tridy");
        }
    }
}
