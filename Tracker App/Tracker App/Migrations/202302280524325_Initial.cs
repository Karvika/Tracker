namespace Tracker_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddExpenses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        categId = c.Int(nullable: false),
                        expenseAmount = c.Single(nullable: false),
                        date = c.DateTime(nullable: false),
                        userEmail = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ExpenseCategories", t => t.categId, cascadeDelete: true)
                .Index(t => t.categId);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        categoryName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SetBudgets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        budgetAmount = c.Single(nullable: false),
                        categId = c.Int(nullable: false),
                        userEmail = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ExpenseCategories", t => t.categId, cascadeDelete: true)
                .Index(t => t.categId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SetBudgets", "categId", "dbo.ExpenseCategories");
            DropForeignKey("dbo.AddExpenses", "categId", "dbo.ExpenseCategories");
            DropIndex("dbo.SetBudgets", new[] { "categId" });
            DropIndex("dbo.AddExpenses", new[] { "categId" });
            DropTable("dbo.SetBudgets");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.AddExpenses");
        }
    }
}
