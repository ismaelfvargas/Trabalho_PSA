using Entities.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using PL.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.DAO
{
    public class ImagemEF : IImagemDAO
    {
        private readonly SecondHandContext _context;

        //construtor produtos entity framework        

        public ImagemEF (SecondHandContext context)
        {
            _context = context;
        }

        //recebe um id de imagem e retorna o resultado
        public Imagem GetImagem(int ImagemId)
        {
            var im = _context.Imagem.Find(ImagemId);
            if (im != null)
            {
                return im;
            }
            else
            {
                return null;
            }
        }

        //salvar imagen
        public void LoadFiles (long ProdutoId, List<IFormFile> files)
        {
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    Imagem im = new Imagem();
                    im.ProdutoId = ProdutoId;
                    im.ImageMimeType = formFile.ContentType;
                    im.ImageFile = new byte[formFile.Length];

                    using (var stream = new MemoryStream())
                    {
                        formFile.CopyToAsync(stream);
                        im.ImageFile = stream.ToArray();

                    }
                    _context.Imagem.Add(im);
                }

                _context.SaveChanges();
            }

        }
    }
}
