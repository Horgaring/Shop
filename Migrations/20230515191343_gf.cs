using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBAPP.Migrations
{
    /// <inheritdoc />
    public partial class gf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountChatModel_Accounts_UsersId",
                table: "AccountChatModel");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountChatModel_Group_GroupsId",
                table: "AccountChatModel");

            migrationBuilder.DropIndex(
                name: "IX_AccountChatModel_UsersId",
                table: "AccountChatModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserMessage",
                table: "Group",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User2Message",
                table: "Group",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupName",
                table: "Group",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "Group",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccountChatModel1",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChatModel1", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AccountChatModel1_Accounts_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChatModel1_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_productId",
                table: "Group",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountChatModel1_UsersId",
                table: "AccountChatModel1",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_product_productId",
                table: "Group",
                column: "productId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_product_productId",
                table: "Group");

            migrationBuilder.DropTable(
                name: "AccountChatModel1");

            migrationBuilder.DropIndex(
                name: "IX_Group_productId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "Group");

            migrationBuilder.AlterColumn<string>(
                name: "UserMessage",
                table: "Group",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "User2Message",
                table: "Group",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "Group",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_AccountChatModel_UsersId",
                table: "AccountChatModel",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountChatModel_Accounts_UsersId",
                table: "AccountChatModel",
                column: "UsersId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountChatModel_Group_GroupsId",
                table: "AccountChatModel",
                column: "GroupsId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
