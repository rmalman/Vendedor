using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vendedor.Models
{
    public class Endereco
    {
        public Int64 EnderecoID { get; set; }


        private string _Logradouro;
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string Logradouro {
            get
            {
                if (string.IsNullOrEmpty(_Logradouro))
                {
                    return _Logradouro;
                }
                return _Logradouro.ToUpper();
            }
            set
            {
                _Logradouro = value;
            }
        }

        private string _Numero;
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string Numero {
            get
            {
                if (string.IsNullOrEmpty(_Numero))
                {
                    return _Numero;
                }
                return _Numero.ToUpper();
            }
            set
            {
                _Numero = value;
            }
        }

        private string _Complemento;
        public string Complemento {
            get
            {
                if (string.IsNullOrEmpty(_Complemento))
                {
                    return _Complemento;
                }
                return _Complemento.ToUpper();
            }
            set
            {
                _Complemento = value;
            }
        }

        [Range(0, 99999999)]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public Int32 CEP { get; set; }



        private string _Bairro;
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public string Bairro {
            get
            {
                if (string.IsNullOrEmpty(_Bairro))
                {
                    return _Bairro;
                }
                return _Bairro.ToUpper();
            }
            set
            {
                _Bairro = value;
            }
        }
        


        [Required(ErrorMessage = "Preenchimento obrigatório")]
        public Int64 MunicipioID { get; set; }
        [Display(Name = "Cidade")]
        public virtual Municipio Cidade { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "UF")]
        public int EstadoID { get; set; }

        [Display(Name = "UF")]
        public virtual Estado UF { get; set; }

        

    }
    
}