using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IImagemDAO
    {
        public void LoadFiles (long ProdutoId, List<IFormFile> files);

        public Imagem GetImagem (int ImagemId);
    }
}
