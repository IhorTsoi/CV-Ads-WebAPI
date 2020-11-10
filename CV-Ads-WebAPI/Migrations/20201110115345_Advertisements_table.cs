using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Ads_WebAPI.Migrations
{
    public partial class Advertisements_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_Customers_CustomerId",
                table: "Advertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementView_Advertisement_AdvertisementId",
                table: "AdvertisementView");

            migrationBuilder.DropForeignKey(
                name: "FK_HumanLimit_Advertisement_AdvertisementId",
                table: "HumanLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriodLimit_Advertisement_AdvertisementId",
                table: "TimePeriodLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisement",
                table: "Advertisement");

            migrationBuilder.RenameTable(
                name: "Advertisement",
                newName: "Advertisements");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisement_CustomerId",
                table: "Advertisements",
                newName: "IX_Advertisements_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Customers_CustomerId",
                table: "Advertisements",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementView_Advertisements_AdvertisementId",
                table: "AdvertisementView",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HumanLimit_Advertisements_AdvertisementId",
                table: "HumanLimit",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimePeriodLimit_Advertisements_AdvertisementId",
                table: "TimePeriodLimit",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Customers_CustomerId",
                table: "Advertisements");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementView_Advertisements_AdvertisementId",
                table: "AdvertisementView");

            migrationBuilder.DropForeignKey(
                name: "FK_HumanLimit_Advertisements_AdvertisementId",
                table: "HumanLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_TimePeriodLimit_Advertisements_AdvertisementId",
                table: "TimePeriodLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "Advertisement");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisements_CustomerId",
                table: "Advertisement",
                newName: "IX_Advertisement_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisement",
                table: "Advertisement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_Customers_CustomerId",
                table: "Advertisement",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementView_Advertisement_AdvertisementId",
                table: "AdvertisementView",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_HumanLimit_Advertisement_AdvertisementId",
                table: "HumanLimit",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimePeriodLimit_Advertisement_AdvertisementId",
                table: "TimePeriodLimit",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
