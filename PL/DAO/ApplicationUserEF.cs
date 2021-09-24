using Entities.Interfaces;
using LogicLayer;
using Entities.Models;
using Entities.Models.Enums;
using PL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace PL.DAO
{
    public class ApplicationUserEF : IApplicationUserDAO
    {
        private readonly SecondHandContext _context;
        private readonly Reputacao _rep;
        private readonly IProdutoDAO _prod;

        //construtor produtos entity framework        

        public ApplicationUserEF(SecondHandContext context,
                                 Reputacao reputacao,
                                 IProdutoDAO prodEF)
        {
            _context = context;
            _rep = reputacao;
            _prod = prodEF;
        }

        //recebe o username e retorna o id de usuario
        public string getUserID(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(userName));
            return user.Id;
        }

        //recebe o username e retorna o endereco e cep
        public IQueryable<EnderecoComCep> getEnderecoCep(string userName)
        {
            var endCep = from p in _context.Users
                         where p.UserName == userName
                         select new EnderecoComCep
                         {
                            Cep = p.CEP,
                            Endereco = p.Endereco
                         };

            return endCep;
        }

        //retorna informações de vendas de um perfil
        public ApplicationUser vendasPerfil(String userName)
        {
            var vendedor = _context.ApplicationUser
                .Where(u => u.UserName.Equals(userName))
                .FirstOrDefault();

            var produtos = from p in _context.Produtos
                           where p.NomeVendedor.Equals(vendedor.UserName)
                           select p;

            AtualizaVendas(vendedor, produtos);

            return vendedor;
        }

        //retorna informações de compras de um perfil
        public ApplicationUser comprasPerfil(String userName)
        {
            var comprador = _context.ApplicationUser
                .Where(u => u.UserName.Equals(userName))
                .FirstOrDefault();

            var produtos = from p in _context.Produtos
                           where p.NomeComprador.Equals(comprador.UserName)
                           select p;

            AtualizaCompras(comprador, produtos);           

            return comprador;
        }

        //conta a quantidade de produtos a venda e atualiza no bando de dados
        public void AtualizaVendas(ApplicationUser vendedor, IQueryable<Produto> prod)
        {
            //conta a quantidade de produtos a venda
            var QtdProdutosAVenda = from p in prod
                            where p.Estado == StatusProduto.Disponivel
                            select new
                            {
                                p.Name
                            };
            vendedor.ProdutosAVenda = QtdProdutosAVenda.Count();

            //conta a quantidade de produtos aguardando aprovacao
            var QtdProdutosAguardandoAprovacao = from p in prod
                           where p.Estado == StatusProduto.Aguardando_Aprovacao
                           select new
                           {
                               p.Name
                           };
            vendedor.ProdutosAguardandoApVenda = QtdProdutosAguardandoAprovacao.Count();

            //conta a quantidade de produtos vendidos
            var QtdProdutosVendidos = from p in prod
                                      where p.Estado == StatusProduto.Vendido
                                      select new
                                      {
                                          p.Name
                                      };
            vendedor.ProdutosVendido = QtdProdutosVendidos.Count();

            //conta a quantidade de produtos em rota de entrega
            var QtdProdutosEmRotaDeEntrega = from p in prod
                                             where p.Estado == StatusProduto.Em_Rota_De_Entrega
                                             select new
                                             {
                                                 p.Name
                                             };
            vendedor.ProdutosEmRotaDeEntrega = QtdProdutosEmRotaDeEntrega.Count();

            //conta a quantidade de produtos entregues
            var QtdProdutosEntregues = from p in prod
                                       where p.Estado == StatusProduto.Entregue
                                       select new
                                       {
                                           p.Name
                                       };
            vendedor.ProdutosEntregue = QtdProdutosEntregues.Count();

            //conta a quantidade de produtos bloqueados
            var QtdProdutosBloqueados = from p in prod
                                        where p.Estado == StatusProduto.Bloqueado
                                        select new
                                        {
                                            p.Name
                                        };
            vendedor.ProdutosBloqueado = QtdProdutosBloqueados.Count();

            //atualiza o banco de dados
            _context.Update(vendedor);
            _context.SaveChanges();

        }

        public void AtualizaCompras(ApplicationUser vendedor, IQueryable<Produto> prod)
        {
            //conta a quantidade de produtos comprados
            var QtdProdutosComprados = from p in prod
                                    where p.Estado == StatusProduto.Vendido
                                    select new
                                    {
                                        p.Name
                                    };

            //conta a quantidade de produtos comprados
            var QtdProdutosAguardandoAprovacao = from p in prod
                                       where p.Estado == StatusProduto.Aguardando_Aprovacao
                                       select new
                                       {
                                           p.Name
                                       };
            vendedor.ProdutosComprados = QtdProdutosComprados.Count() + QtdProdutosAguardandoAprovacao.Count();

            //conta a quantidade de produtos em rota de entrega
            var QtdProdutosCompradosEmRotaDeEntrega = from p in prod
                                             where p.Estado == StatusProduto.Em_Rota_De_Entrega
                                             select new
                                             {
                                                 p.Name
                                             };
            vendedor.ProdutosCompradosEmRotaDeEntrega = QtdProdutosCompradosEmRotaDeEntrega.Count();

            //conta a quantidade de produtos entregues
            var QtdProdutosCompradosEntregues = from p in prod
                                       where p.Estado == StatusProduto.Entregue
                                       select new
                                       {
                                           p.Name
                                       };
            vendedor.ProdutosCompradosEntregue = QtdProdutosCompradosEntregues.Count();

            //conta a quantidade de produtos com a venda negada
            var QtdProdutosComVendaNegada = from p in prod
                                        where p.Estado == StatusProduto.Bloqueado
                                        select new
                                        {
                                            p.Name
                                        };
            vendedor.ProdutosComVendaNegada = QtdProdutosComVendaNegada.Count();

            //atualiza o banco de dados
            _context.Update(vendedor);
            _context.SaveChanges();

        }

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        public Boolean AvaliaVendedor(String userName, int avaliacao, long idProd)
        {
            var vendedor = _context.ApplicationUser
                .Where(u => u.UserName.Equals(userName))
                .FirstOrDefault();

            if (vendedor != null)
            {
                vendedor.NroAvaliacoes = vendedor.NroAvaliacoes + 1;
                vendedor.ReputacaoTotal = vendedor.ReputacaoTotal + avaliacao;
                _context.Update(vendedor);

                vendedor.ReputacaoFinal = _rep.CalculaReputacao(vendedor);
                _context.Update(vendedor);
                _context.SaveChanges();

                _prod.ProdutoAvaliado(idProd);

                return true;
            }
            else
            {
                return false;
            }
        }

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        public Boolean AvaliaVendedorCompraNegada(String userName)
        {
            var vendedor = _context.ApplicationUser
                .Where(u => u.UserName.Equals(userName))
                .FirstOrDefault();

            if (vendedor != null)
            {
                vendedor.NroAvaliacoes = vendedor.NroAvaliacoes + 1;
                _context.Update(vendedor);

                vendedor.ReputacaoFinal = _rep.CalculaReputacao(vendedor);
                _context.Update(vendedor);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        //retorna o numero de avaliações de um usuario
        public int GetReputacao(String userName)
        {
            var vendedor = _context.ApplicationUser
                        .Where(u => u.UserName.Equals(userName))
                        .FirstOrDefault();

            return vendedor.ReputacaoFinal;
        }



    }
}
