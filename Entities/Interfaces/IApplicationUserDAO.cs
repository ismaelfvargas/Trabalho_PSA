using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IApplicationUserDAO
    {
        //recebe o username e retorna o id de usuario
        public String getUserID(String userName);

        //retorna informações de vendas de um perfil
        public ApplicationUser vendasPerfil(String userName);

        //retorna informações de compras de um perfil
        public ApplicationUser comprasPerfil(String userName);

        //recebe o username e retorna o endereco e cep
        public IQueryable<EnderecoComCep> getEnderecoCep(string userName);

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        //muda o status do produto para avaliado
        public Boolean AvaliaVendedor(String userName, int avaliacao, long idProd);

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        public Boolean AvaliaVendedorCompraNegada(String userName);

        //retorna o numero de avaliações de um usuario
        public int GetReputacao(String userName);


    }
}
