namespace WissAppEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WissApp_v101 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "E-Mail", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "E-Mail", c => c.Binary(nullable: false, maxLength: 200));
        }
    }
}
