using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class seed_admin_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Default", "Admin" });

            migrationBuilder.UpdateData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                column: "Login",
                value: "adam@nure.ua");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Ihor", "Tsoi" });

            migrationBuilder.UpdateData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"),
                column: "Login",
                value: "qweqwe");
        }
    }
}
