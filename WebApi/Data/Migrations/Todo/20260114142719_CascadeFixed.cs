using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Data.Migrations.Todo
{
    /// <inheritdoc />
    public partial class CascadeFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagToTask_Tasks_TodoTaskId1",
                table: "TagToTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskPages_Tasks_TodoTaskId1",
                table: "TaskPages");

            migrationBuilder.DropIndex(
                name: "IX_TaskPages_TodoTaskId1",
                table: "TaskPages");

            migrationBuilder.DropIndex(
                name: "IX_TagToTask_TodoTaskId1",
                table: "TagToTask");

            migrationBuilder.DropColumn(
                name: "TodoTaskId1",
                table: "TaskPages");

            migrationBuilder.DropColumn(
                name: "TodoTaskId1",
                table: "TagToTask");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId1",
                table: "TaskPages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodoTaskId1",
                table: "TagToTask",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskPages_TodoTaskId1",
                table: "TaskPages",
                column: "TodoTaskId1",
                unique: true,
                filter: "[TodoTaskId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TagToTask_TodoTaskId1",
                table: "TagToTask",
                column: "TodoTaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TagToTask_Tasks_TodoTaskId1",
                table: "TagToTask",
                column: "TodoTaskId1",
                principalTable: "Tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskPages_Tasks_TodoTaskId1",
                table: "TaskPages",
                column: "TodoTaskId1",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
