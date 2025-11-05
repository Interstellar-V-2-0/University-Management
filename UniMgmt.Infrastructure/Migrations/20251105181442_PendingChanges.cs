using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMgmt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "classroom",
                table: "Sections",
                newName: "Classroom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Classroom",
                table: "Sections",
                newName: "classroom");
        }
    }
}
