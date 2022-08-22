using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace chatApplication.Migrations
{
    public partial class individual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndividualRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(nullable: true),
                    RecievedId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualChats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    IndividualRoomId = table.Column<int>(nullable: false),
                    myUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualChats_IndividualRooms_IndividualRoomId",
                        column: x => x.IndividualRoomId,
                        principalTable: "IndividualRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividualChats_AspNetUsers_myUserId",
                        column: x => x.myUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualChats_IndividualRoomId",
                table: "IndividualChats",
                column: "IndividualRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualChats_myUserId",
                table: "IndividualChats",
                column: "myUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualChats");

            migrationBuilder.DropTable(
                name: "IndividualRooms");
        }
    }
}
