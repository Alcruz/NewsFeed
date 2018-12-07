using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModusCreate.NewsFeed.Web.Migrations
{
    public partial class AddFeedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FeedUrl = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_FeedSubscriptions_Id",
                        column: x => x.Id,
                        principalTable: "FeedSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FeedId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedEntries", x => new { x.FeedId, x.Id });
                    table.ForeignKey(
                        name: "FK_FeedEntries_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedEntries");

            migrationBuilder.DropTable(
                name: "Feeds");
        }
    }
}
