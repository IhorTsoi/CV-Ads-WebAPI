using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class Advertisements_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Advertisements",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");
        }
    }
}
