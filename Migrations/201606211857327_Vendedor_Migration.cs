namespace Vendedor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendedor_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        BairroID = c.Long(nullable: false, identity: true),
                        MunicipioID = c.Long(nullable: false),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BairroID)
                .ForeignKey("dbo.Municipio", t => t.MunicipioID, cascadeDelete: true)
                .Index(t => t.MunicipioID);
            
            AddColumn("dbo.DadosPessoaFisica", "UsuarioID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bairro", "MunicipioID", "dbo.Municipio");
            DropIndex("dbo.Bairro", new[] { "MunicipioID" });
            DropColumn("dbo.DadosPessoaFisica", "UsuarioID");
            DropTable("dbo.Bairro");
        }
    }
}
