using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Usuario : IdentityUser
    {
        [MaxLength(50)]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [MaxLength(40)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [MaxLength(9)]
        [Display(Name = "CEP")]
        public string CEP { get; set; }


        [MaxLength(255)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
