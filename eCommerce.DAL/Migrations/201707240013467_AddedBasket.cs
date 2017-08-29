namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        BasketItemId = c.Int(nullable: false, identity: true),
                        BasketId = c.Guid(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Basket_BasketId = c.Int(),
                    })
                .PrimaryKey(t => t.BasketItemId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Baskets", t => t.Basket_BasketId)
                .Index(t => t.ProductId)
                .Index(t => t.Basket_BasketId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "Basket_BasketId", "dbo.Baskets");
            DropForeignKey("dbo.BasketItems", "ProductId", "dbo.Products");
            DropIndex("dbo.BasketItems", new[] { "Basket_BasketId" });
            DropIndex("dbo.BasketItems", new[] { "ProductId" });
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
