namespace YuanBang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addErrorLimit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "ErrorTimes", c => c.Int(nullable: false));
            AddColumn("dbo.Admins", "FirstDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "FirstDateTime");
            DropColumn("dbo.Admins", "ErrorTimes");
        }
    }
}
