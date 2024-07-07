using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_subscription.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubsciptionId",
                table: "Subscriptions",
                newName: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subscriptions",
                newName: "SubsciptionId");
        }
    }
}
