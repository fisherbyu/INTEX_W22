using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BYU_EGYPT_INTEX.Migrations
{
    public partial class AddRole : Migration
    {
        private string PublicRoleId = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    {PublicRoleId, "Public", "PUBLIC", null}
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
