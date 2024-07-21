using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleChat.DAL.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessageConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_UserId",
                table: "messages");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_UserId",
                table: "messages",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_UserId",
                table: "messages");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_UserId",
                table: "messages",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
