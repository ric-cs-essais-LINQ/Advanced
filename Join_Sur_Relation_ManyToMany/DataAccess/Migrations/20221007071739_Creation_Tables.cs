using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Creation_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Numeros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valeur = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numeros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiragesLoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiragesLoto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumerosTiragesLoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroId = table.Column<int>(type: "int", nullable: false),
                    TirageLotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumerosTiragesLoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumerosTiragesLoto_Numeros_NumeroId",
                        column: x => x.NumeroId,
                        principalTable: "Numeros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NumerosTiragesLoto_TiragesLoto_TirageLotoId",
                        column: x => x.TirageLotoId,
                        principalTable: "TiragesLoto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Numeros_Valeur",
                table: "Numeros",
                column: "Valeur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumerosTiragesLoto_NumeroId_TirageLotoId",
                table: "NumerosTiragesLoto",
                columns: new[] { "NumeroId", "TirageLotoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumerosTiragesLoto_TirageLotoId",
                table: "NumerosTiragesLoto",
                column: "TirageLotoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiragesLoto_Date",
                table: "TiragesLoto",
                column: "Date",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumerosTiragesLoto");

            migrationBuilder.DropTable(
                name: "Numeros");

            migrationBuilder.DropTable(
                name: "TiragesLoto");
        }
    }
}
