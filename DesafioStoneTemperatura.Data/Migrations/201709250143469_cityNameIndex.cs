namespace DesafioStoneTemperatura.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class cityNameIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", c => c.String(maxLength: 250, unicode: false));
            CreateIndex("dbo.Cities", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cities", new[] { "Name" });
            AlterColumn("dbo.Cities", "Name", c => c.String());
        }
    }
}
