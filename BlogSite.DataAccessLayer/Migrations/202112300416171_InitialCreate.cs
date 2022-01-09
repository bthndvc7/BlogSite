namespace BlogSite.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SYSTEM.Article",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Author = c.String(nullable: false, maxLength: 50),
                        CategorName = c.String(nullable: false, maxLength: 50),
                        ArticleDate = c.String(nullable: false, maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 50),
                        ImageUrl = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Decimal(nullable: false, precision: 1, scale: 0),
                        ReadingCount = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Content = c.String(nullable: false),
                        ArticleUrl = c.String(nullable: false, maxLength: 150),
                        Tags = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SYSTEM.Author",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NameSurname = c.String(nullable: false, maxLength: 50),
                        AuthorAbout = c.String(nullable: false, maxLength: 250),
                        Image = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FacebookAdress = c.String(nullable: false, maxLength: 50),
                        TwitterAdress = c.String(nullable: false, maxLength: 50),
                        InstagramAdress = c.String(nullable: false, maxLength: 50),
                        Role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("SYSTEM.Author");
            DropTable("SYSTEM.Article");
        }
    }
}
