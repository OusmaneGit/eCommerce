namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasketItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "Basket_BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "Basket_BasketId" });
            DropColumn("dbo.BasketItems", "BasketId");
            RenameColumn(table: "dbo.BasketItems", name: "Basket_BasketId", newName: "BasketId");
            AlterColumn("dbo.BasketItems", "BasketId", c => c.Int(nullable: false));
            AlterColumn("dbo.BasketItems", "BasketId", c => c.Int(nullable: false));
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "BasketId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            AlterColumn("dbo.BasketItems", "BasketId", c => c.Int());
            AlterColumn("dbo.BasketItems", "BasketId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.BasketItems", name: "BasketId", newName: "Basket_BasketId");
            AddColumn("dbo.BasketItems", "BasketId", c => c.Guid(nullable: false));
            CreateIndex("dbo.BasketItems", "Basket_BasketId");
            AddForeignKey("dbo.BasketItems", "Basket_BasketId", "dbo.Baskets", "BasketId");
        }
    }
}
