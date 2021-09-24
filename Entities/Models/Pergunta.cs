using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Pergunta
    {
        [Required]
        public long PerguntaId { get; set; }

        [Display(Name = "Pergunta")]
        [Required]
        public String Perguntas { get; set; }

        [Display(Name = "Resposta")]
        public String Respostas { get; set; }
                
        public StatusPergunta StatusPergunta { get;set;}
                
        public long ProdutoId { get; set; }

        public Produto Produto { get; set; }
    }
}
