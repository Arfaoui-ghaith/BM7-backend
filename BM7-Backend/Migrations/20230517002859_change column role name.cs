using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BM7_Backend.Migrations
{
    /// <inheritdoc />
    public partial class changecolumnrolename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");
        }
    }
}
