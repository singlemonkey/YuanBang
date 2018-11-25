namespace YuanBang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        ParentID = c.Int(nullable: false),
                        DisPlayIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        NoticeTypeID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.NoticeTypeID, cascadeDelete: true)
                .Index(t => t.NoticeTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notices", "NoticeTypeID", "dbo.Dictionaries");
            DropIndex("dbo.Notices", new[] { "NoticeTypeID" });
            DropTable("dbo.Notices");
            DropTable("dbo.Dictionaries");
            DropTable("dbo.Admins");
        }
    }
}
