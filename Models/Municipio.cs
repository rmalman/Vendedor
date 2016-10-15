using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vendedor.Models
{
    public class Municipio
    {
        public Int64 MunicipioID { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "UF")]
        public int EstadoID { get; set; }

        [Display(Name = "UF")]
        public virtual Estado UF { get; set; }

        private string _Nome;
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "Municipio")]
        public String Nome
        {
            get
            {
                if (string.IsNullOrEmpty(_Nome))
                {
                    return _Nome;
                }
                return _Nome.ToUpper();
            }
            set
            {
                _Nome = value;
            }
        }
    }
}