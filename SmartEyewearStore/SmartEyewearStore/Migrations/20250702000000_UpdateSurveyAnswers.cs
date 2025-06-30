using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceShape",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "PreferredColors",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "BudgetRange",
                table: "SurveyAnswers");

            migrationBuilder.AddColumn<string>(
                name: "GlassType",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tone",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlassType",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "SurveyAnswers");

            migrationBuilder.DropColumn(
                name: "Tone",
                table: "SurveyAnswers");

            migrationBuilder.AddColumn<string>(
                name: "FaceShape",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredColors",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AgeRange",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BudgetRange",
                table: "SurveyAnswers",
                nullable: false,
                defaultValue: "");
        }
    }
}