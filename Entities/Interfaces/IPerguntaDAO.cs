using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IPerguntaDAO
    {
        //recebe uma pergunta e salva
        public void SalvaPergunta(long ProdId, String Per);

        //recebe uma resposta para uma pergunta e salva
        public void SalvaResposta(long PerId, String Resp);

        //recebe um id de uma pergunta e retorna o resultado
        public Pergunta GetPergunta(long PerId);
    }
}
