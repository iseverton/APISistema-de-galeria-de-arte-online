using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeGaleriaDeArteAPI.Migrations
{
    /// <inheritdoc />
    public partial class modificacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "work_name",
                table: "category",
                newName: "category_name");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "category",
                newName: "work_name");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
