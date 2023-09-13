using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Migrations
{
    public partial class testRunFinalTrialFinale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId1",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId2",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InterviewAndEvaluationViewModelId3",
                table: "Questions",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId1",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId2",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId3",
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

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId2",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewAndEvaluationViewModelId3",
                table: "Questions");
        }
    }
}
