using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Migrations
{
    public partial class testRunFinalTrialFinaleee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId2",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId3",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "InterviewAndEvaluationViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId2",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId3",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_InterviewAndEvaluationViewModelId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_InterviewAndEvaluationViewModelId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId2",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId3",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId",
                table: "Evaluations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId1",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId2",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId3",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId",
                table: "Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterviewAndEvaluationViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterViewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPoints = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewAndEvaluationViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId1",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId2",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId2");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId3",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId3");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_InterviewAndEvaluationViewModelId",
                table: "Jobs",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_InterviewAndEvaluationViewModelId",
                table: "Evaluations",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Evaluations",
                column: "InterviewAndEvaluationViewModelId",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Jobs",
                column: "InterviewAndEvaluationViewModelId",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId1",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId1",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId2",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId2",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId3",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId3",
                principalTable: "InterviewAndEvaluationViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
