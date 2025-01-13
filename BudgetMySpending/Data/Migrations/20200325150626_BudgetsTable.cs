using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetMySpending.Data.Migrations
{
    public partial class BudgetsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
