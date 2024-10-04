using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewAppTasklyWebApi.Migrations
{
    public partial class TaskManagementTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskPriorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskPriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskManagements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskManagements_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskManagements_TaskPriorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "TaskPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskManagements_TaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "TaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskStateManagements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStateManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStateManagements_TaskManagements_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskStateManagements_TaskStates_StateId",
                        column: x => x.StateId,
                        principalTable: "TaskStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagements_CreationDate",
                table: "TaskManagements",
                column: "CreationDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagements_PriorityId",
                table: "TaskManagements",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagements_StateId",
                table: "TaskManagements",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagements_Title",
                table: "TaskManagements",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagements_UserId",
                table: "TaskManagements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskPriorities_Name",
                table: "TaskPriorities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskStateManagements_StateId",
                table: "TaskStateManagements",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStateManagements_TaskId",
                table: "TaskStateManagements",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStates_Name",
                table: "TaskStates",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskStateManagements");

            migrationBuilder.DropTable(
                name: "TaskManagements");

            migrationBuilder.DropTable(
                name: "TaskPriorities");

            migrationBuilder.DropTable(
                name: "TaskStates");
        }
    }
}
