namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReciverId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chats");
        }
    }
}
