using Entities.Models;
using System;

namespace LogicLayer
{
    public class Reputacao
    {
        public int CalculaReputacao(ApplicationUser vendedor)
        {
            //Lógica para aumento de reputação:
            if(vendedor.ReputacaoTotal <= 0)
            {
                return 0;
            }
            else
            {
                return vendedor.ReputacaoTotal / vendedor.NroAvaliacoes;
            }
        }
        
    }
}
