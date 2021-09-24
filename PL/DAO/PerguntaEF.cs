using Entities.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using PL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.DAO
{
    public class PerguntaEF : IPerguntaDAO
    {
        private readonly SecondHandContext _context;

        //construtor produtos entity framework        

        public PerguntaEF (SecondHandContext context)
        {
            _context = context;
        }


        public Pergunta GetPergunta(long PerId)
        {
            var per = _context.Perguntas.Find(PerId);
            if (per != null)
            {
                return per;
            }
            else
            {
                return null;
            }
        }        

        //recebe uma pergunta e salva
        public void SalvaPergunta(long ProdId, string Per)
        {
            Pergunta pergu = new Pergunta();
            pergu.ProdutoId = ProdId;
            pergu.StatusPergunta = Entities.Models.Enums.StatusPergunta.PerguntaAguardando;
            pergu.Perguntas = Per;
            pergu.Respostas = "Nenhuma Resposta!";
            _context.Perguntas.Add(pergu);
            _context.SaveChanges();
        }

        public void SalvaResposta(long PerId, string Resp)
        {
            var per = GetPergunta(PerId);
            per.Respostas = Resp;
            per.StatusPergunta = Entities.Models.Enums.StatusPergunta.PerguntaRespondida;
            _context.Update(per);
            _context.SaveChanges();
        }
    }
}
