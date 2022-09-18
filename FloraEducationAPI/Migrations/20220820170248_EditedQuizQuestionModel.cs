using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloraEducationAPI.Migrations
{
    public partial class EditedQuizQuestionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "QuizQuestion");

            migrationBuilder.AddColumn<List<string>>(
                name: "Answers",
                table: "QuizQuestion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerIndex",
                table: "QuizQuestion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answers",
                table: "QuizQuestion");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerIndex",
                table: "QuizQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "QuizQuestion",
                type: "text",
                nullable: true);
        }
    }
}
