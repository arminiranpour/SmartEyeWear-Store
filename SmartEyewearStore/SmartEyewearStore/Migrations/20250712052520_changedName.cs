using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEyewearStore.Migrations
{
    /// <inheritdoc />
    public partial class changedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "DBS311_252NAA12",
                table: "GLASSESINFO",
                newName: "GLASSESINFOID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GLASSESINFOID",
                schema: "DBS311_252NAA12",
                table: "GLASSESINFO",
                newName: "ID");
        }
    }
}
