using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class AddDirectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Director_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Director_ID);
                });

            migrationBuilder.CreateTable(
                name: "Directors_Movies",
                columns: table => new
                {
                    Director_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Movie_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors_Movies", x => new { x.Director_ID, x.Movie_ID });
                    table.ForeignKey(
                        name: "FK_Directors_Movies_Directors_Director_ID",
                        column: x => x.Director_ID,
                        principalTable: "Directors",
                        principalColumn: "Director_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Directors_Movies_Movies_Movie_ID",
                        column: x => x.Movie_ID,
                        principalTable: "Movies",
                        principalColumn: "Movie_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directors_Movies_Movie_ID",
                table: "Directors_Movies",
                column: "Movie_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directors_Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
