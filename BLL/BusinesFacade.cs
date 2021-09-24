using Entities.Interfaces;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using PL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BusinesFacade
    {
        private readonly IProdutoDAO _ProdutoDAO;
        private readonly IImagemDAO _ImagemDAO;
        private readonly IApplicationUserDAO _ApplicationUserDAO;
        private readonly ICategoriaDAO _CategoriaDAO;
        private readonly IPerguntaDAO _PerguntaDAO;

        //construtor busines facade

        public BusinesFacade(IProdutoDAO Pdao, IImagemDAO Idao, 
                             IApplicationUserDAO Adao, ICategoriaDAO Cdao,
                             IPerguntaDAO Perdao)
        {
            _ProdutoDAO = Pdao;
            _ImagemDAO = Idao;
            _ApplicationUserDAO = Adao;
            _CategoriaDAO = Cdao;
            _PerguntaDAO = Perdao;
        }        

        #region consultas em produtos

        //Todos os produtos do banco:
        public List<Produto> ListaDeProdutos()
        {
            return _ProdutoDAO.ListaDeProdutos();
        }

        //retorna uma lista IQuerable com todos os produtos disponiveis para venda no banco de dados
        public IQueryable<Produto> IQuerDeProdutosDisponiveis()
        {
            return _ProdutoDAO.IQuerDeProdutosDisponiveis();
        }

        //Salva um produto novo no banco
        public void CadNovoProduto(Produto prod)
        {
            _ProdutoDAO.CadastroNovoProduto(prod);
        }

        //altera o endereco dos produtos a venda de um usuario
        public void AlteraEndProdutoAvend(String userName, String endereco)
        {
            _ProdutoDAO.AlteraEndProdutoAvend(userName, endereco);
        }

        //Recebe um id e deleta o produto
        public void deletaProduto(long ProdutoID)
        {
            _ProdutoDAO.deletaProduto(ProdutoID);
        }

        //Recebe um id e informa se o produto existe ou nao
        public Boolean existe(long ProdutoID)
        {
            return _ProdutoDAO.existe(ProdutoID);
        }

        //Recebe um ID de produto e retorna o mesmo
        public Produto ItemPorId(long ProdutoID)
        {
            return _ProdutoDAO.ItemPorId(ProdutoID);
        }

        //relatorio de itens disponiveis para venda
        public List<Produto> ItensDisponiveis()
        {
            return _ProdutoDAO.ItensDisponiveis();
        }

        //relatorio de itens por uma determinada categoria
        public List<Produto> ItensPorCategoria(String cat)
        {
            return _ProdutoDAO.ItensPorCategoria(cat);
        }

        //recebe uma categoria e uma lista de produtos
        //retorna todos os produtos dessa categoria
        public IQueryable<Produto> IqueryItensPorCategoria(String cat, IQueryable<Produto> prods)
        {
            return _ProdutoDAO.IqueryItensPorCategoria(cat, prods);
        }

        //relatorio de itens por uma determinada categoria e palavra
        public List<Produto> ItensPalChavCat(String palChave, String cat)
        {
            return _ProdutoDAO.ItensPalChavCat(palChave, cat);
        }

        //recebe uma palavra chave e retorna um produto
        public List<Produto> ItensPalChav(string palChave)
        {
            return _ProdutoDAO.ItensPalChav(palChave);
        }

        //recebe uma palavra chave e um iquery de produtos retorna um iquery de produtos
        //que contenham em seu nome a palavra fornecida
        public IQueryable<Produto> IqueyItensPalChav(string palChave, IQueryable<Produto> prods)
        {
            return _ProdutoDAO.IqueyItensPalChav(palChave, prods);
        }

        //recebe dois valores e um iquery de produto retorna
        //uma lista de produtos dentro desses valores
        public IQueryable<Produto> IqueryItensFaixaDeValores(decimal valIni, decimal valFin,
                                                                IQueryable<Produto> prods)
        {
            return _ProdutoDAO.IqueryItensFaixaDeValores(valIni, valFin, prods);
        }

        //relatorio de itens por um determinado usuario
        public List<Produto> ItensPorStatusUsu(String usu)
        {
            return _ProdutoDAO.ItensPorStatusUsu(usu);
        }

        //recebe um id de usuario e retorna um iquery de todos os produtos
        //de um usuario ordenados pelo status
        public IQueryable<Produto> IqueryItensPorStatusUsu(String usu)
        {
            return _ProdutoDAO.IqueryItensPorStatusUsu(usu);
        }

        //relatorio do total de vendas em um determinado periodo de tempo
        public IQueryable<TotalVendaPorPeriodo> TotalVendaPeriodo(DateTime dtIni, DateTime dtFin)
        {
            return _ProdutoDAO.NroTotalVendaPeriodo(dtIni, dtFin);
        }

        //recebe um produto e salva as modificacoes
        public void editProduto(Produto prod)
        {
            _ProdutoDAO.editProduto(prod);
        }

        //realiza a venda de um produto
        public Boolean VendaProduto(long id, String userName)
        {
            return _ProdutoDAO.VendaProduto(id, userName);
        }

        //realiza o cancelamento da venda de um produto
        public Boolean CompradorCancelarVendaProduto(long id)
        {
            return _ProdutoDAO.CompradorCancelarVendaProduto(id);
        }

        //Comprador aceitou realizar a venda do produto, recebe um id e muda o status do produto
        public Boolean CompradoAceitouVendaProduto(long id)
        {
            return _ProdutoDAO.CompradoAceitouVendaProduto(id);
        }

        //Comprador negou realizar a venda do produto, recebe um id e muda o status do produto
        public Boolean CompradoNegouVendaProduto(long id)
        {
            return _ProdutoDAO.CompradoNegouVendaProduto(id);
        }

        //recebe um id do comprador e retorna uma lista de todos os produtos
        //comprados por ele ordenados pelo status
        public IQueryable<Produto> IqueyItensDoComprador(String usu)
        {
            return _ProdutoDAO.IqueyItensDoComprador(usu);
        }        

        //retorna uma lista de produtos prontos para serem entregados
        public List<Produto> ItensParaEntrega()
        {
            return _ProdutoDAO.ItensParaEntrega();
        }

        //retorna uma lista de produtos em rota de entrega
        public List<Produto> ItensEmRotaDeEntrega()
        {
            return _ProdutoDAO.ItensEmRotaDeEntrega();
        }

        //recebe o id de um produto e o email de um entregador e coloca o produto
        //para rota de entregada
        public Boolean EntregaProduto(long id, String entregador)
        {
            return _ProdutoDAO.EntregaProduto(id, entregador);
        }

        //recebe o id de um produto e confirma sua entrega
        public Boolean ProdutoEntregue(long id)
        {
            return _ProdutoDAO.ProdutoEntregue(id);
        }

        //retorna o total de produtos bloqueados no banco de dados
        public long totalProdBloqueados(DateTime dtIni, DateTime dtFin)
        {
            return _ProdutoDAO.totalProdBloqueados(dtIni, dtFin);
        }

        //retorna o total de produtos entregues no banco de dados
        public long totalProdEntregues(DateTime dtIni, DateTime dtFin)
        {
            return _ProdutoDAO.totalProdEntregues(dtIni, dtFin);
        }

        //retorna o total de produtos anunciados no site
        public long totalProdAnunciados(DateTime dtIni, DateTime dtFin)
        {
            return _ProdutoDAO.totalProdAnunciados(dtIni, dtFin);
        }

        #endregion

        #region consultas em imagem

        //Salva uma imagens novo no banco
        public void CadImagem(long ProdutoId, List<IFormFile> files)
        {
            _ImagemDAO.LoadFiles(ProdutoId,files);
        }

        //recebe um id de imagem e retorna o resultado
        public Imagem GetImagem(int ImagemId)
        {
            return _ImagemDAO.GetImagem(ImagemId);
        }              
        

            #endregion

        #region consultas em application user

        //retorna o id de um usuario
        public String getUserID(String userName)
        {
            return _ApplicationUserDAO.getUserID(userName);
        }

        //recebe o username e retorna o endereco e cep
        public IQueryable<EnderecoComCep> getEnderecoCep(string userName)
        {
            return _ApplicationUserDAO.getEnderecoCep(userName);
        }

        //retorna informações de vendas de um perfil
        public ApplicationUser vendasPerfil(String userName)
        {
            return _ApplicationUserDAO.vendasPerfil(userName);
        }

        //retorna informações de compras de um perfil
        public ApplicationUser comprasPerfil(String userName)
        {
            return _ApplicationUserDAO.comprasPerfil(userName);
        }        

        //retorna o numero de avaliações de um usuario
        public int GetReputacao(String userName)
        {
            return _ApplicationUserDAO.GetReputacao(userName);
        }

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        public Boolean AvaliaVendedor(String userName, int avaliacao, long idProd)
        {
            return _ApplicationUserDAO.AvaliaVendedor(userName, avaliacao, idProd);
        }

        //recebe uma nota de um usuario calcula e salva a reputacao final do usuario
        public Boolean AvaliaVendedorCompraNegada(String userName)
        {
            return _ApplicationUserDAO.AvaliaVendedorCompraNegada(userName);
        }

        #endregion

        #region consultas em categorias

        //retorna o nome de todas as categorias de produtos cadastrados
        public IQueryable<String> categoriasNomes()
        {
            return _CategoriaDAO.categoriasNomes();
        }

        //retorna um IEnumerable de categorias
        public IEnumerable<Categoria> categoriasIEnumerable()
        {
            return _CategoriaDAO.categoriasIEnumerable();
        }

        //Salva uma categoria nova no banco
        public void CadastroNovaCategoria(Categoria cat)
        {
            _CategoriaDAO.CadastroNovaCategoria(cat);
        }

        #endregion

        #region

        //recebe uma pergunta e salva
        public void SalvaPergunta(long ProdId, String Per)
        {
            _PerguntaDAO.SalvaPergunta(ProdId, Per);
        }

        //recebe uma resposta para uma pergunta e salva
        public void SalvaResposta(long PerId, String Resp)
        {
            _PerguntaDAO.SalvaResposta(PerId, Resp);
        }

        //recebe um id de uma pergunta e retorna o resultado
        public Pergunta GetPergunta(long PerId)
        {
            return _PerguntaDAO.GetPergunta(PerId);
        }

        #endregion

    }
}
