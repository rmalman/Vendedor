using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vendedor.Models
{
    public class Usuario
    {
        public Int64 UsuarioID { get; set; }


        private string _Login;
        public string Login {
            get
            {
                if (string.IsNullOrEmpty(_Login))
                {
                    return _Login;
                }
                return _Login.ToUpper();
            }
            set
            {
                _Login = value;
            }
        }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "A {0} precisa de no minimo {2} caracteres.", MinimumLength = 6)]
        public string Senha { get; set; }



        private string _Email;
        [Required]
        [EmailAddress]
        public string Email {
            get
            {
                if (string.IsNullOrEmpty(_Email))
                {
                    return _Email;
                }
                return _Email.ToUpper();
            }
            set
            {
                _Email = value;
            }
        }

        public Int64 DadosPessoaFisicaID { get; set; }

        public virtual DadosPessoaFisica DadoPessoaFisica { get; set; }
    }
}