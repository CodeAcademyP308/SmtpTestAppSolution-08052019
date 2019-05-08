namespace SmtpTestApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscribers", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscribers", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
