namespace Vendedor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        SiglaUF = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.EstadoID);
            
            CreateTable(
                "dbo.Municipio",
                c => new
                    {
                        MunicipioID = c.Long(nullable: false, identity: true),
                        EstadoID = c.Int(nullable: false),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MunicipioID)
                .ForeignKey("dbo.Estado", t => t.EstadoID, cascadeDelete: true)
                .Index(t => t.EstadoID);
            
            CreateTable(
                "dbo.DadosPessoaFisica",
                c => new
                    {
                        DadosPessoaFisicaID = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        CPF = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DadosPessoaFisicaID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        UsuarioID = c.Long(nullable: false, identity: true),
                        Login = c.String(),
                        Senha = c.String(nullable: false, maxLength: 18),
                        Email = c.String(nullable: false),
                        DadosPessoaFisicaID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioID)
                .ForeignKey("dbo.DadosPessoaFisica", t => t.DadosPessoaFisicaID, cascadeDelete: true)
                .Index(t => t.DadosPessoaFisicaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "DadosPessoaFisicaID", "dbo.DadosPessoaFisica");
            DropForeignKey("dbo.Municipio", "EstadoID", "dbo.Estado");
            DropIndex("dbo.Usuario", new[] { "DadosPessoaFisicaID" });
            DropIndex("dbo.Municipio", new[] { "EstadoID" });
            DropTable("dbo.Usuario");
            DropTable("dbo.DadosPessoaFisica");
            DropTable("dbo.Municipio");
            DropTable("dbo.Estado");
        }
    }
}
