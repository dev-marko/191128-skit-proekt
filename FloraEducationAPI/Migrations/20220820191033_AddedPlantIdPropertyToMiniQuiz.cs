using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraEducationAPI.Migrations
{
    public partial class AddedPlantIdPropertyToMiniQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiniQuiz_Plants_PlantId",
                table: "MiniQuiz");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlantId",
                table: "MiniQuiz",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MiniQuiz_Plants_PlantId",
                table: "MiniQuiz",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiniQuiz_Plants_PlantId",
                table: "MiniQuiz");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlantId",
                table: "MiniQuiz",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_MiniQuiz_Plants_PlantId",
                table: "MiniQuiz",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
