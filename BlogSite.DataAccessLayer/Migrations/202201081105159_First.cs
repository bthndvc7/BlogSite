namespace BlogSite.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SYSTEM.Article", "IsActive", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("SYSTEM.Article", "IsActive", c => c.Decimal(nullable: false, precision: 1, scale: 0));
        }
    }
}
