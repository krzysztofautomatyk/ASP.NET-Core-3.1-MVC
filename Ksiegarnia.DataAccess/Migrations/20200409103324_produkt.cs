using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiegarnia.DataAccess.Migrations
{
    public partial class produkt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    ListPrice = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Price50 = table.Column<double>(nullable: false),
                    Price100 = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    KategoriaId = table.Column<int>(nullable: false),
                    OkladkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produkty_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produkty_Okladki_OkladkaId",
                        column: x => x.OkladkaId,
                        principalTable: "Okladki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_KategoriaId",
                table: "Produkty",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_OkladkaId",
                table: "Produkty",
                column: "OkladkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produkty");
        }
    }
}
