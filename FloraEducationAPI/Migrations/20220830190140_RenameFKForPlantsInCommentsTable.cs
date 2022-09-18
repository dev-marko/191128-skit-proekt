using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraEducationAPI.Migrations
{
    public partial class RenameFKForPlantsInCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Plants_PlantId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantId",
                table: "Comments",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Plants_PlantId",
                table: "Comments",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
