using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdocaoPetApi.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoMapeamentoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_CategoriaAnimal_CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.DropIndex(
                name: "IX_Animais_CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaAnimal",
                table: "CategoriaAnimal");

            migrationBuilder.DropColumn(
                name: "CategoriaAnimalId",
                table: "Animais");

            migrationBuilder.RenameTable(
                name: "CategoriaAnimal",
                newName: "CategoriaAnimals");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "CategoriaAnimals",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaAnimals",
                table: "CategoriaAnimals",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Animais_IdCategoriaAnimal",
                table: "Animais",
                column: "IdCategoriaAnimal");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_CategoriaAnimal",
                table: "Animais",
                column: "IdCategoriaAnimal",
                principalTable: "CategoriaAnimals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_CategoriaAnimal",
                table: "Animais");

            migrationBuilder.DropIndex(
                name: "IX_Animais_IdCategoriaAnimal",
                table: "Animais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaAnimals",
                table: "CategoriaAnimals");

            migrationBuilder.RenameTable(
                name: "CategoriaAnimals",
                newName: "CategoriaAnimal");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaAnimalId",
                table: "Animais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "CategoriaAnimal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaAnimal",
                table: "CategoriaAnimal",
                column: "Id");

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
    }
}
