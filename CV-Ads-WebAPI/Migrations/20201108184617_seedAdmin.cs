using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class seedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserIdentities",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[] { new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"), "qweqwe", "YVFjQHffGy3JitvNiD7sfE+NwgUesCXVH3zzpJ1HqVNUi2soi7DFh5T8PRu1dtXJ", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserIdentities",
                keyColumn: "Id",
                keyValue: new Guid("1ec7309f-c97d-412c-b8b8-31c1459cbd41"));
        }
    }
}
