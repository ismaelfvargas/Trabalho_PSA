using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Enums
{
    public enum StatusPergunta
    {        
        [Display(Name = "Pergunta Aguardando Resposta")] PerguntaAguardando = 1,
        [Display(Name = "Pergunta Respondida")] PerguntaRespondida = 2,        
    }
    
}
