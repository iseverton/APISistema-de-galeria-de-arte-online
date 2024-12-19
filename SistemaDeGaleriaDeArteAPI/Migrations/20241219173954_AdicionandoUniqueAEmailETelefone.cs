using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeGaleriaDeArteAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoUniqueAEmailETelefone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_users_phoneNumber",
                table: "users",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_email",
                table: "users",
                column: "user_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_phoneNumber",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_user_email",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");
        }
    }
}
