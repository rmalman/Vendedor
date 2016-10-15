using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vendedor.Models;

namespace Vendedor.DAL
{
    public class VendedorInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VendedorContext>
    {
        protected override void Seed(VendedorContext context)
        {
            var estados = new List<Estado>
            {
                //Sudeste
                new Estado{Nome="ESPIRITO SANTO",SiglaUF="ES"},
                new Estado{Nome="MINAS GERAIS",SiglaUF="MG"},
                new Estado{Nome="RIO DE JANEIRO",SiglaUF="RJ"},
                new Estado{Nome="SÃO PAULO",SiglaUF="SP"},
                //Sul
                new Estado{Nome="PARANÁ",SiglaUF="PR"},
                new Estado{Nome="SANTA CATARINA",SiglaUF="SC"},
                new Estado{Nome="RIO GRANDE DO SUL",SiglaUF="RS"},
                //CENTRO-OESTE
                new Estado{Nome="DISTRITO FEDERAL",SiglaUF="DF"},
                new Estado{Nome="GOIÁS",SiglaUF="GO"},
                new Estado{Nome="MATO GROSSO",SiglaUF="MT"},
                new Estado{Nome="MATO GROSSO DO SUL",SiglaUF="MS"},
                //NORTE
                new Estado{Nome="ACRE",SiglaUF="AC"},
                new Estado{Nome="AMAPÁ",SiglaUF="AP"},
                new Estado{Nome="AMAZONAS",SiglaUF="AM"},
                new Estado{Nome="PARÁ",SiglaUF="PA"},
                new Estado{Nome="RONDONIA",SiglaUF="RO"},
                new Estado{Nome="RORAIMA",SiglaUF="RR"},
                new Estado{Nome="TOCANTIS",SiglaUF="TO"},
                //NORDESTE
                new Estado{Nome="ALAGOAS",SiglaUF="AL"},
                new Estado{Nome="BAHIA",SiglaUF="BA"},
                new Estado{Nome="CEARÁ",SiglaUF="CE"},
                new Estado{Nome="MARANHÃO",SiglaUF="MA"},
                new Estado{Nome="PARAÍBA",SiglaUF="PB"},
                new Estado{Nome="PERNANBUCO",SiglaUF="PE"},
                new Estado{Nome="PIAUÍ",SiglaUF="PI"},
                new Estado{Nome="RIO GRANDE DO NORTE",SiglaUF="RN"},
                new Estado{Nome="SERGIPE",SiglaUF="SE"}                
            };
            estados.ForEach(s => context.Estados.Add(s));
            context.SaveChanges();
        }
    }
}