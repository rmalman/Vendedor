using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vendedor.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }

        private string _Nome;

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "Estado")]
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

        private string _SiglaUF;

        [RegularExpression(@"^.{2,}$", ErrorMessage = "UF possui duas letras")]
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Dado Inválido")]
        [Display(Name = "UF")]
        public string SiglaUF
        {
            get
            {
                if (string.IsNullOrEmpty(_SiglaUF))
                {
                    return _SiglaUF;
                }
                return _SiglaUF.ToUpper();
            }
            set
            {
                _SiglaUF = value;
            }
        }

        public virtual ICollection<Municipio> Municipio { get; set; }
    }
}