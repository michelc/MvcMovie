using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Genre_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Genre_ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Movie_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Genre_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Movie_ID);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_Genre_ID",
                        column: x => x.Genre_ID,
                        principalTable: "Genres",
                        principalColumn: "Genre_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Genre_ID",
                table: "Movies",
                column: "Genre_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
