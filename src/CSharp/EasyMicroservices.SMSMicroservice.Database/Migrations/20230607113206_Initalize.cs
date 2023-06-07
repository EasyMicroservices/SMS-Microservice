using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyMicroservices.SMSMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class Initalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageSenders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUserId = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSenders_ApiUsers_ApiUserId",
                        column: x => x.ApiUserId,
                        principalTable: "ApiUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUserId = table.Column<long>(type: "bigint", nullable: true),
                    ToPhoneNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "VARCHAR(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextMessages_ApiUsers_ApiUserId",
                        column: x => x.ApiUserId,
                        principalTable: "ApiUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageSenderTextMessages",
                columns: table => new
                {
                    MessageSenderId = table.Column<long>(type: "bigint", nullable: false),
                    TextMessageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSenderTextMessages", x => new { x.TextMessageId, x.MessageSenderId });
                    table.ForeignKey(
                        name: "FK_MessageSenderTextMessages_MessageSenders_MessageSenderId",
                        column: x => x.MessageSenderId,
                        principalTable: "MessageSenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSenderTextMessages_TextMessages_TextMessageId",
                        column: x => x.TextMessageId,
                        principalTable: "TextMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenders_ApiUserId",
                table: "MessageSenders",
                column: "ApiUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSenderTextMessages_MessageSenderId",
                table: "MessageSenderTextMessages",
                column: "MessageSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TextMessages_ApiUserId",
                table: "TextMessages",
                column: "ApiUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageSenderTextMessages");

            migrationBuilder.DropTable(
                name: "MessageSenders");

            migrationBuilder.DropTable(
                name: "TextMessages");

            migrationBuilder.DropTable(
                name: "ApiUsers");
        }
    }
}
