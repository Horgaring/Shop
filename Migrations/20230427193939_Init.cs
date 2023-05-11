using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WEBAPP.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Wallet = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    UserMessage = table.Column<string>(type: "text", nullable: true),
                    User2Message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Refresh",
                columns: table => new
                {
                    Refreshtoken = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refresh", x => x.Refreshtoken);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PathImage = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    accountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_Accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountChatModel",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChatModel", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AccountChatModel_Accounts_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChatModel_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategProduct",
                columns: table => new
                {
                    categId = table.Column<int>(type: "integer", nullable: false),
                    productsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategProduct", x => new { x.categId, x.productsId });
                    table.ForeignKey(
                        name: "FK_CategProduct_Categ_categId",
                        column: x => x.categId,
                        principalTable: "Categ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategProduct_product_productsId",
                        column: x => x.productsId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountChatModel_UsersId",
                table: "AccountChatModel",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name",
                table: "Accounts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategProduct_productsId",
                table: "CategProduct",
                column: "productsId");

            migrationBuilder.CreateIndex(
                name: "IX_product_accountId",
                table: "product",
                column: "accountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountChatModel");

            migrationBuilder.DropTable(
                name: "CategProduct");

            migrationBuilder.DropTable(
                name: "Refresh");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Categ");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
