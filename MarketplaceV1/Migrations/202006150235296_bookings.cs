namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        BloggerNickname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bookings");
        }
    }
}
