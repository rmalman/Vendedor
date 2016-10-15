namespace Vendedor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DadosPessoaisPF_Endereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoID = c.Long(nullable: false, identity: true),
                        Logradouro = c.String(nullable: false),
                        Numero = c.String(nullable: false),
                        Complemento = c.String(),
                        CEP = c.Int(nullable: false),
                        Bairro = c.String(nullable: false),
                        MunicipioID = c.Long(nullable: false),
                        EstadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnderecoID)
                .ForeignKey("dbo.Municipio", t => t.MunicipioID, cascadeDelete: true)
                .ForeignKey("dbo.Estado", t => t.EstadoID, cascadeDelete: false)
                .Index(t => t.MunicipioID)
                .Index(t => t.EstadoID);
            
            AddColumn("dbo.DadosPessoaFisica", "EnderecoID", c => c.Long(nullable: false));
            CreateIndex("dbo.DadosPessoaFisica", "EnderecoID");
            AddForeignKey("dbo.DadosPessoaFisica", "EnderecoID", "dbo.Endereco", "EnderecoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DadosPessoaFisica", "EnderecoID", "dbo.Endereco");
            DropForeignKey("dbo.Endereco", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "MunicipioID", "dbo.Municipio");
            DropIndex("dbo.Endereco", new[] { "EstadoID" });
            DropIndex("dbo.Endereco", new[] { "MunicipioID" });
            DropIndex("dbo.DadosPessoaFisica", new[] { "EnderecoID" });
            DropColumn("dbo.DadosPessoaFisica", "EnderecoID");
            DropTable("dbo.Endereco");
        }
    }
}
