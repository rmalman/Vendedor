namespace Vendedor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correcao_Bairro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bairro", "MunicipioID", "dbo.Municipio");
            DropIndex("dbo.Bairro", new[] { "MunicipioID" });
            DropTable("dbo.Bairro");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        BairroID = c.Long(nullable: false, identity: true),
                        MunicipioID = c.Long(nullable: false),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BairroID);
            
            CreateIndex("dbo.Bairro", "MunicipioID");
            AddForeignKey("dbo.Bairro", "MunicipioID", "dbo.Municipio", "MunicipioID", cascadeDelete: true);
        }
    }
}
