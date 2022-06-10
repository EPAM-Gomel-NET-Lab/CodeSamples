using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityService.Persistence.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5275a09e-8b37-478f-a5c8-67c5e3a14261", "e87ecc4b-4775-42e6-8cfc-d27a6c55c5a8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "df414647-0682-4fce-82a0-b5d600560826", "4dc8e70d-ca21-4499-9c02-2f1e59360dd5", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5275a09e-8b37-478f-a5c8-67c5e3a14261");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df414647-0682-4fce-82a0-b5d600560826");
        }
    }
}
