using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanNow.Infrastructured.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "detailsDomiciles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Addresses = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Apt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypeClean = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Time = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageDomicile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailsDomiciles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detailsDomiciles");
        }
    }
}
