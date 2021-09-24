using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{

    public interface IProdutoDAO
    {
        //Todos os produtos do banco:
        public List<Produto> ListaDeProdutos();

        //retorna uma lista IQuerable com todos os produtos disponiveis para venda no banco de dados
        public IQueryable<Produto> IQuerDeProdutosDisponiveis();

        //Salva um produto novo no banco
        public void CadastroNovoProduto(Produto prod);

        //realiza a venda de um produto
        public Boolean VendaProduto(long id, String userName);

        //realiza o cancelamento da venda de um produto
        public Boolean CompradorCancelarVendaProduto(long id);

        //Comprador aceitou realizar a venda do produto, recebe um id e muda o status do produto
        public Boolean CompradoAceitouVendaProduto(long id);

        //Comprador negou realizar a venda do produto, recebe um id e muda o status do produto
        public Boolean CompradoNegouVendaProduto(long id);

        //Recebe um ID de produto e retorna o mesmo
        public Produto ItemPorId(long ProdutoID);

        //Recebe um id e deleta o produto
        public void deletaProduto(long ProdutoID);

        //Recebe um id e informa se o produto existe ou nao
        public Boolean existe(long ProdutoID);

        //relatorio de itens disponiveis para venda
        public List<Produto> ItensDisponiveis();

        //relatorio de itens por uma determinada categoria
        public List<Produto> ItensPorCategoria(String cat);

        //recebe uma categoria e uma lista de produtos
        //retorna todos os produtos dessa categoria
        public IQueryable<Produto> IqueryItensPorCategoria(String cat, IQueryable<Produto> prods);

        //recebe uma palavra chave e retorna um produto
        public List<Produto> ItensPalChav(string palChave);

        //recebe uma palavra chave e um iquery de produtos retorna um iquery de produtos
        //que contenham em seu nome a palavra fornecida
        public IQueryable<Produto> IqueyItensPalChav(string palChave, IQueryable<Produto> prods);

        //relatorio de itens por uma determinada categoria e palavra
        public List<Produto> ItensPalChavCat(String palChave, String cat);

        //recebe dois valores e um iquery de produto retorna
        //uma lista de produtos dentro desses valores
        public IQueryable<Produto> IqueryItensFaixaDeValores(decimal valIni, decimal valFin,
                                                                IQueryable<Produto> prods);

        //relatorio de itens por Status do produto de um determinado usuario
        public List<Produto> ItensPorStatusUsu(String usu);

        //recebe um id de usuario e retorna um iquery de todos os produtos
        //de um usuario ordenados pelo status
        public IQueryable<Produto> IqueryItensPorStatusUsu(String usu);

        ////relatorio do total de vendas em um determinado periodo de tempo
        public IQueryable<TotalVendaPorPeriodo> NroTotalVendaPeriodo(DateTime dtIni, DateTime dtFin);

        //recebe um produto e salva as modificacoes
        public void editProduto(Produto prod);

        //recebe um id do comprador e retorna uma lista de todos os produtos
        //comprados por ele ordenados pelo status
        public IQueryable<Produto> IqueyItensDoComprador(String usu);

        //retorna uma lista de produtos prontos para serem entregados
        public List<Produto> ItensParaEntrega();

        //retorna uma lista de produtos em rota de entrega
        public List<Produto> ItensEmRotaDeEntrega();

        //recebe o id de um produto e o email de um entregador e coloca o produto
        //para rota de entregada
        public Boolean EntregaProduto(long id, String entregador);

        //recebe o id de um produto e confirma sua entrega
        public Boolean ProdutoEntregue(long id);

        //altera o endereco dos produtos a venda de um usuario
        public void AlteraEndProdutoAvend(String userName, String endereco);

        //recebe o id de um produto e muda o status dele para avaliado
        public void ProdutoAvaliado(long id);

        //retorna o total de produtos bloqueados no banco de dados
        public long totalProdBloqueados(DateTime dtIni, DateTime dtFin);

        //retorna o total de produtos entregues no banco de dados
        public long totalProdEntregues(DateTime dtIni, DateTime dtFin);

        //retorna o total de produtos anunciados no site
        public long totalProdAnunciados(DateTime dtIni, DateTime dtFin);

    }
}
