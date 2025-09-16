using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetricUnit",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetricUnit",
                table: "Stocks");
        }
    }
}
