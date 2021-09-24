using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using PL.Context;
using BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SecondHandWeb.Models;

namespace SecondHandWeb.Controllers
{
    public class MeusProdutosCompradosController : Controller
    {
        private readonly BusinesFacade _businesFacade;
        public readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _environment;

        public MeusProdutosCompradosController(BusinesFacade businesFacade,
                                   UserManager<ApplicationUser> userManager, 
                                   IWebHostEnvironment environment)
        {
            _businesFacade = businesFacade;
            _environment = environment;
            _userManager = userManager;
        }

        [Authorize]
        // GET: MeusProdutosComprados
        public async Task<IActionResult> Index(string ProdutosCategoria, string searchString,
                                               decimal ValIni, decimal ValFinal)
        {
            var usuario = await _userManager.GetUserAsync(HttpContext.User);
            var categoriaQuery = _businesFacade.categoriasNomes();
            var produtos = _businesFacade.IqueyItensDoComprador(usuario.Id);

            if (!string.IsNullOrEmpty(searchString))
            {
                produtos = _businesFacade.IqueyItensPalChav(searchString, produtos);
            }

            if (!string.IsNullOrEmpty(ProdutosCategoria))
            {
                produtos = _businesFacade.IqueryItensPorCategoria(ProdutosCategoria, produtos);
            }

            if (ValIni != 0 || ValFinal != 0)
            {
                produtos = _businesFacade.IqueryItensFaixaDeValores(ValIni, ValFinal, produtos);
            }

            var produtoCategoriaVM = new ProdutoCategoriaViewModel
            {
                Categorias = new SelectList(categoriaQuery.Distinct().ToList()),
                Produtos = produtos.ToList()
            };

            if (usuario != null)
            {
                ViewData["usuario"] = usuario;
            }

            return View(produtoCategoriaVM);
        }        

        [Authorize]
        // GET: MeusProdutosComprados/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = _businesFacade.ItemPorId((long)id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: MeusProdutosComprados/Cancel/5
        public async Task<IActionResult> Cancel(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }           

            Boolean prod = _businesFacade.CompradorCancelarVendaProduto((long)id);

            if (prod == false)
            {
                return NotFound();
            }

            return View(_businesFacade.ItemPorId((long)id));
        }

        //recebe uma pergunta e id de produto e salva a pergunta
        public async Task<IActionResult> SalvaPergunta(long id, String per)
        {

            _businesFacade.SalvaPergunta(id, per);

            return RedirectToAction("Details", "MeusProdutosComprados", new { Id = id });

        }

        //recebe uma pergunta e id de produto e salva a pergunta
        public async Task<IActionResult> Avaliar(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Status = new[]
            {
                new SelectListItem() { Value = "1", Text = "1"},
                new SelectListItem() { Value = "2", Text = "2"},
                new SelectListItem() { Value = "3", Text = "3"},
                new SelectListItem() { Value = "4", Text = "4"},
                new SelectListItem() { Value = "5", Text = "5"}
            };

            return View(_businesFacade.ItemPorId((long)id));

        }

        //salva a avaliacao da compra do produto
        public async Task<IActionResult> SalvaAvaliacao(String userName, String avaliacao, long? idProd)
        {            
            if (idProd == null)
            {
                return NotFound();
            }

            var rep = _businesFacade.AvaliaVendedor(userName, Int32.Parse(avaliacao), (long)idProd);

            if (rep == true)
            {
                return RedirectToAction("Index", "MeusProdutosComprados");
            }
            else
            {
                return NotFound();
            }

        }

        private bool ProdutoExists(long id)
        {
            return _businesFacade.existe(id);
        }

        //dados do usuario
        public async Task<IActionResult> DadosUsuario()
        {
            var usuario = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.Id = usuario.Id;
            ViewBag.UserName = usuario.UserName;

            return View();

        }

        public ActionResult GetImage(int id)
        {
            Imagem im = _businesFacade.GetImagem(id);
            if (im != null)
            {
                return File(im.ImageFile, im.ImageMimeType);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult LoadFiles(long ProdutoId, List<IFormFile> files)
        {
            _businesFacade.CadImagem(ProdutoId, files);

            return View("Details", _businesFacade.ItemPorId(ProdutoId));
        }
    }
}

