using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanNow.Infrastructured.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Timbre",
                table: "detailsDomiciles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timbre",
                table: "detailsDomiciles");
        }
    }
}
