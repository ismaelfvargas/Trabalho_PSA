using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface ICategoriaDAO
    {
        //retorna o nome de todas as categorias de produtos cadastrados
        public IQueryable<String> categoriasNomes();

        //retorna um IEnumerable de categorias
        public IEnumerable<Categoria> categoriasIEnumerable();

        //Salva uma categoria nova no banco
        public void CadastroNovaCategoria(Categoria cat);
    }
}