using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdocaoPetApi.Migrations
{
    /// <inheritdoc />
    public partial class RoleMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_UserId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserRole",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                newName: "IX_UserRole_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Role",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRole_UsuarioId",
                table: "UserRole",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRole_UsuarioId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UsuarioId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
