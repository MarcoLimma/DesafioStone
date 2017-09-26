namespace DesafioStoneTemperatura.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Temperatures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Value = c.Int(nullable: false),
                        CityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Temperatures", "CityId", "dbo.Cities");
            DropIndex("dbo.Temperatures", new[] { "CityId" });
            DropTable("dbo.Temperatures");
            DropTable("dbo.Cities");
        }
    }
}
