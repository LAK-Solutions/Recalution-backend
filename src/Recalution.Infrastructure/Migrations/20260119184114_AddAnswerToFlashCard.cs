using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recalution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerToFlashCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCard_Decks_DeckId",
                table: "FlashCard");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeckId",
                table: "FlashCard",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "FlashCard",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "FlashCard",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCard_Decks_DeckId",
                table: "FlashCard",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCard_Decks_DeckId",
                table: "FlashCard");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "FlashCard");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "FlashCard");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeckId",
                table: "FlashCard",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCard_Decks_DeckId",
                table: "FlashCard",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id");
        }
    }
}
