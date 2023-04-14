using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BYU_EGYPT_INTEX.Migrations
{
    public partial class Roles : Migration
    {
        private string UserRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();

        private string AdminId = Guid.NewGuid().ToString();
        private string TestId = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedUser(migrationBuilder);

            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] {"Id", "Name", "NormalizedName", "ConcurrencyStamp"},
                values: new object[,]
                {
                    {AdminRoleId, "Administrator", "ADMINISTRATOR", null},
                    {UserRoleId, "User", "USER", null}
                });
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {"Id", "UserName", "NormalizedUserName",
                    "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp",
                    "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount"},
                values: new object[,]
                {
                    {AdminId, "admin@fagelgamous.com", "ADMIN@FAGELGAMOUS.COM",
                    "admin@fagelgamous.com", "ADMIN@FAGELGAMOUS.COM", false,"AQAAAAEAACcQAAAAEDGQ5wwj6Iz0lXHIZ5IwuvgSO88jrSBT1etWcDYjJN5CBNDKvddZcEeixYBYmcdFag==",
                    "YUPAFWNGZI2UC5FOITC7PX5J7XZTAZAA", "8e150555-a20d-4610-93ff-49c5af44f749", null, false, false, null, true, 0},
                    {TestId, "test@gmail.com", "TEST@GMAIL.COM",
                    "test@gmail.com", "TEST@GMAIL.COM", false,"AQAAAAEAACcQAAAAEDGQ5wwj6Iz0lXHIZ5IwuvgSO88jrSBT1etWcDYjJN5CBNDKvddZcEeixYBYmcdFag==",
                    "YUPAFWNGZI2UC5FOITC7PX5J7XZTAZAA", "8e150555-a20d-4610-93ff-49c5af44f749", null, false, false, null, true, 0},
                });
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] {"UserId", "RoleId"},
                values: new object[,]
                {
                    {AdminId, AdminRoleId},
                    {AdminId, UserRoleId},
                    {TestId, UserRoleId}
                }); 

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
