using Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHandWeb.Models
{
    public class ProdutoCategoriaViewModel
    {
        public List<Produto> Produtos { get; set; }

        public SelectList Categorias { get; set; }

        public string ProdutosCategoria { get; set; }

        public string SearchString { get; set; }

        public Decimal ValIni { get; set; }

        public Decimal ValFinal { get; set; }

    }
}
