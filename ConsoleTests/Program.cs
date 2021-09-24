using BLL;
using Entities.Models;
using Entities.ViewModels;
using PL;
using PL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //SecondHandContext context = new SecondHandContext();
            BusinesFacade _bll = new BusinesFacade();

            #region Todos os produtos da tabela
            
            foreach (Produto p in _bll.listaDeProdutos())
            {
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}  {5}  {6}",
                                    p.ProdutoId, p.Name, p.Descricao, p.Estado, p.Valor, p.UsuarioId, p.CategoriaID);
            }
            #endregion

            #region Para criar um produto basta tirar os comentários
            /*Produto produtoNovo = new Produto()
            {
                Name = "Placa de video RTX 3070",
                Descricao = "Placa nvidia serie RTX",
                Categoria = "Informatica",
                DataEntrada = new DateTime(2020, 10, 10),
                Estado = StatusProduto.Status.Disponivel,
                Valor = 6300.0m,
                UsuarioId = 1
            };
            _bll.NovoProduto(produtoNovo);*/
            Console.WriteLine("\n\n");
            #endregion

            #region Consulta 1 - Itens a venda de uma determinada categoria
            Console.WriteLine("1 - Itens a venda de uma determinada categoria:\n");
            String cat = "Celular";
            Console.WriteLine("Categoria pesquisada: '{0}'\n", cat);

            foreach (Produto p in _bll.ItensPorCategoria(cat))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");
            #endregion

            #region Consulta 2 - Itens a venda dada uma palavra chave e uma categoria
            Console.WriteLine("2 - Itens a venda dada uma palavra chave e uma categoria:\n");
            cat = "TV";
            String palChave = "tv";
            Console.WriteLine("Categoria pesquisada: '{0}', Palavra chave: {1}\n", cat, palChave);

            foreach (Produto p in _bll.ItensPalChavCat(palChave, cat))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");
            #endregion

            #region Consulta 3 - Itens a venda dentro de uma faixa de valores
            Console.WriteLine("3 - Itens a venda dentro de uma faixa de valores\n");
            decimal valIni = 290.0m;
            decimal valFin = 500.0m;
            Console.WriteLine("Faixa de valores\nDe: [{0}] a: [{1}]\n", valIni, valFin);

            foreach (Produto p in _bll.FaixaDeValores(valIni, valFin))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");
            #endregion

            #region Consulta 4 - Itens anunciados por um determinado vendedor agrupados pelo status da venda
            Console.WriteLine("4 - Itens anunciados por um determinado vendedor agrupados pelo status da venda\n");
            int vend = 2;
            Console.WriteLine("ID do vendedor: {0}\n", vend);

            foreach (Produto p in _bll.ItensPorVendedor(vend))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");
            #endregion

            #region Consulta 5 - Número total de itens vendidos num período e o valor total destas vendas);
            Console.WriteLine("5 - Número total de itens vendidos num período e o valor total destas vendas\n");
            DateTime dtIni = new DateTime(2020, 04, 01);
            DateTime dtFin = new DateTime(2020, 05, 01);
            Console.WriteLine("Itens vendidos entre '{0}' e '{1}'\n", dtIni, dtFin);

            foreach (ItensPorIntervaloDeTempo i in _bll.ItensPorIntervaloDeTempo(dtIni, dtFin))
            {
                Console.WriteLine("Número total de itens vendidos: {0}\nValor total destas vendas: {1}\n",
                                    i.numTotalItensVendidos, i.valorTotalVendas);
            }
            Console.WriteLine("\n\n");
            #endregion
        }
    }
}