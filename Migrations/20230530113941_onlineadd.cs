using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBAPP.Migrations
{
    /// <inheritdoc />
    public partial class onlineadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnreadMessages",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "UserMessage",
                table: "Group");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Message",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Message",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "Online",
                table: "Group",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Online",
                table: "Group");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Time",
                table: "Message",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "Message",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "UnreadMessages",
                table: "Group",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserMessage",
                table: "Group",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
