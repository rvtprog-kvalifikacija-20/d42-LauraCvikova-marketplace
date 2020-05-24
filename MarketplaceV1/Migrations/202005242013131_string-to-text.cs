namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringtotext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Data", c => c.String(unicode: true, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Data", c => c.String());
        }
    }
}
