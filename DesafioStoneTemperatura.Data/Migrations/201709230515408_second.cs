namespace DesafioStoneTemperatura.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Temperatures");
            AlterColumn("dbo.Temperatures", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Temperatures", new[] { "Id", "CityId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Temperatures");
            AlterColumn("dbo.Temperatures", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Temperatures", "Id");
        }
    }
}
