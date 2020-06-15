namespace MarketplaceV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chatintidtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chats", "SenderId", c => c.String());
            AlterColumn("dbo.Chats", "ReciverId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Chats", "ReciverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Chats", "SenderId", c => c.Int(nullable: false));
        }
    }
}
