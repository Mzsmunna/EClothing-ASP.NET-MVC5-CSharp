namespace MVCEClothing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBM1EClothing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Pid = c.Int(nullable: false, identity: true),
                        Itemname = c.String(),
                        Category = c.String(),
                        Itemfor = c.String(),
                        Sizes = c.String(),
                        Available = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Currency = c.String(),
                        Cost = c.Single(nullable: false),
                        Offer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pid);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Prid = c.Int(nullable: false, identity: true),
                        Itemname = c.String(),
                        Category = c.String(),
                        Itemfor = c.String(),
                        Sizes = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        TotalPrice = c.Single(nullable: false),
                        Currency = c.String(),
                        Cost = c.Single(nullable: false),
                        Offer = c.Int(nullable: false),
                        Purchasedby = c.String(),
                        Phonenumber = c.String(),
                        address = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        Pid_Pid = c.Int(),
                    })
                .PrimaryKey(t => t.Prid)
                .ForeignKey("dbo.Product", t => t.Pid_Pid)
                .Index(t => t.Pid_Pid);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Gender = c.String(),
                        Email = c.String(),
                        Phonenumber = c.String(),
                        address = c.String(),
                        Password = c.String(),
                        Usertype = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "Pid_Pid", "dbo.Product");
            DropIndex("dbo.Purchase", new[] { "Pid_Pid" });
            DropTable("dbo.User");
            DropTable("dbo.Purchase");
            DropTable("dbo.Product");
        }
    }
}
