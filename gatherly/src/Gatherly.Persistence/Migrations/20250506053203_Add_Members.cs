using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gatherly.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Members : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "jack.daniels@mail.com", "Jack", "Daniels" });

            migrationBuilder.CreateIndex(
                name: "IX_Member_Email",
                table: "Member",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
