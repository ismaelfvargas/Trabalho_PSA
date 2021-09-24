using BLL;
using Entities.Interfaces;
using Entities.Models;
using Entities.Models.Enums;
using Entities.ViewModels;
using Ninject;
using PL.Context;
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

            SecondHandContext context = new();

            //injetando dependencia com ninject
            Ninject.IKernel inject = new StandardKernel();
            inject.Bind<IProdutoDAO>().To<ProdutoEF>();
            var obj = inject.Get<BusinesFacade>();

            BusinesFacade _bll = obj;

            #region Criação

            Produto produtoNovo = new Produto()
            {
                Name = "prod teste",
                Descricao = "produto para testar a criação no bando de dados",
                CategoriaID = 1,
                DataEntrada = new DateTime(2020, 04, 01),
                Estado = StatusProduto.Disponivel,
                Valor = 200.0m,
                UsuarioIDVendedor = "c0f2a95a-d441-423f-b369-746d098c18a9"
            };

            _bll.CadNovoProduto(produtoNovo);

            #endregion


            #region testando conteudo de cada "tabela"

            List<Categoria> cate = context.Categorias.ToList();

            foreach (Categoria c in cate)
            {
                Console.WriteLine("{0}  {1}",
                                    c.CategoriaId, c.Name);
            }
            Console.WriteLine("\n\n");

            foreach (Produto p in _bll.ListaDeProdutos())
            {
                Console.WriteLine("{0}  {1}  {2}  {3}  {4}  {5}  {6}",
                                    p.ProdutoId, p.Name, p.Descricao, p.Estado,
                                    p.Valor, p.UsuarioIDVendedor, p.Categoria);
            }
            Console.WriteLine("\n\n");

            #endregion


            #region Testando todas as consultas da entrega 1

            Console.WriteLine("1 - Itens a venda de uma determinada categoria:\n");
            String cat = "TV";
            Console.WriteLine("Categoria pesquisada: '{0}'\n", cat);

            foreach (Produto p in _bll.ItensPorCategoria(cat))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");


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


            Console.WriteLine("3 - Itens a venda dentro de uma faixa de valores\n");
            decimal valIni = 290.0m;
            decimal valFin = 500.0m;
            Console.WriteLine("Faixa de valores\nDe: [{0}] a: [{1}]\n", valIni, valFin);

            /*foreach (Produto p in _bll.ItensFaixaDeValores(valIni, valFin))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");*/


            Console.WriteLine("4 - Itens anunciados por um determinado vendedor agrupados pelo status da venda\n");
            String vend = "c0f2a95a-d441-423f-b369-746d098c18a9";
            Console.WriteLine("ID do vendedor: {0}\n", vend);

            foreach (Produto p in _bll.ItensPorStatusUsu(vend))
            {
                Console.WriteLine("Produto: {0}\nDescrição: {1}\nStatus: {2}\nValor: {3}\n" +
                                    "Categoria: {4}\n",
                                    p.Name, p.Descricao, p.Estado, p.Valor, p.Categoria);
            }
            Console.WriteLine("\n\n");


            Console.WriteLine("5 - Número total de itens vendidos num período e o valor total destas vendas\n");
            DateTime dtIni = new DateTime(2020, 04, 01);
            DateTime dtFin = new DateTime(2020, 05, 01);
            Console.WriteLine("Entre as datas de '{0}' e '{1}'\n", dtIni, dtFin);

            foreach (TotalVendaPorPeriodo i in _bll.TotalVendaPeriodo(dtIni, dtFin))
            {
                Console.WriteLine("Número total de itens vendidos: {0}\nValor total destas vendas: {1}\n",
                                    i.numVendasPeriodo, i.valorVendasPeriodo);
            }
            Console.WriteLine("\n\n");

            #endregion

        }
    }
}
