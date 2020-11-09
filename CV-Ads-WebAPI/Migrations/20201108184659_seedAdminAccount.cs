using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class seedAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"), "Ihor", "Tsoi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"));
        }
    }
}
