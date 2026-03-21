using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdocaoPetApi.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaAnimalId",
                table: "Animais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCategoriaAnimal",
                table: "Animais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriaAnimal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaAnimal", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animais_CategoriaAnimalId",
                table: "Animais",
                column: "CategoriaAnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animais_CategoriaAnimal_CategoriaAnimalId",
                table: "Animais",
                column: "CategoriaAnimalId",
                principalTable: "CategoriaAnimal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_CategoriaAnimal_CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.DropTable(
                name: "CategoriaAnimal");

            migrationBuilder.DropIndex(
                name: "IX_Animais_CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.DropColumn(
                name: "CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.DropColumn(
                name: "IdCategoriaAnimal",
                table: "Animais");
        }
    }
}
