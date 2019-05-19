namespace MVCEClothing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBM1EClothing1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchase", "Pid_Pid", "dbo.Product");
            DropIndex("dbo.Purchase", new[] { "Pid_Pid" });
            AddColumn("dbo.Purchase", "Pid", c => c.Int(nullable: false));
            DropColumn("dbo.Purchase", "Pid_Pid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchase", "Pid_Pid", c => c.Int());
            DropColumn("dbo.Purchase", "Pid");
            CreateIndex("dbo.Purchase", "Pid_Pid");
            AddForeignKey("dbo.Purchase", "Pid_Pid", "dbo.Product", "Pid");
        }
    }
}
