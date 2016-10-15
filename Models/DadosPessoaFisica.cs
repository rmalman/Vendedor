using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vendedor.Models
{
    public class DadosPessoaFisica
    {
        
        public Int64 DadosPessoaFisicaID { get; set; }

        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public Int64 CPF { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public Int64 UsuarioID { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }


        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public Int64 EnderecoID { get; set; }

        [Display(Name = "Endereco")]
        public virtual Endereco Endereco { get; set; }


    }
}