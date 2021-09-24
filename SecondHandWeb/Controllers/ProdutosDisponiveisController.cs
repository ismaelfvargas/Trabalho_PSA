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
using System.IO;
using SecondHandWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SecondHandWeb.Controllers
{
    public class ProdutosDisponiveisController : Controller
    {
        private readonly BusinesFacade _businesFacade;
        public readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _environment;

        public ProdutosDisponiveisController(BusinesFacade businesFacade,
                                   UserManager<ApplicationUser> userManager, 
                                   IWebHostEnvironment environment)
        {
            _businesFacade = businesFacade;
            _environment = environment;
            _userManager = userManager;
        }
                

        // GET: ProdutosDisponiveis
        [AllowAnonymous]
        public async Task<IActionResult> Index(string ProdutosCategoria, string searchString,
                                               decimal ValIni, decimal ValFinal)
        {
            var categoriaQuery = _businesFacade.categoriasNomes();
            var produtos = _businesFacade.IQuerDeProdutosDisponiveis();

            if (!string.IsNullOrEmpty(searchString))
            {
                produtos = _businesFacade.IqueyItensPalChav(searchString, produtos);
            }

            if (!string.IsNullOrEmpty(ProdutosCategoria))
            {
                produtos = _businesFacade.IqueryItensPorCategoria(ProdutosCategoria, produtos);
            }

            if(ValIni != 0 || ValFinal != 0)
            {
                produtos = _businesFacade.IqueryItensFaixaDeValores(ValIni, ValFinal, produtos);
            }

            var produtoCategoriaVM = new ProdutoCategoriaViewModel
            {
                Categorias = new SelectList(categoriaQuery.Distinct().ToList()),
                Produtos = produtos.ToList()
            };

            var usuario = await _userManager.GetUserAsync(HttpContext.User);

            if (usuario != null)
            {
                ViewData["user"] = usuario;
            }

            return View(produtoCategoriaVM);
        }

        // GET: ProdutosDisponiveis/Details/
        [AllowAnonymous]
        public async Task<IActionResult> Details(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var produto = _businesFacade.ItemPorId((long)id);
            if (produto == null)
            {
                return NotFound();
            }

            var usuario = await _userManager.GetUserAsync(HttpContext.User);
            if (usuario != null)
            {
                ViewData["usuario"] = usuario;
            }              

            return View(produto);
        }

        // GET: ProdutosDisponiveis/Compra/
        [Authorize]
        public async Task<IActionResult> Compra(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Pegando o usuário logado:
            var usuario = await _userManager.GetUserAsync(HttpContext.User);

            Boolean prod = _businesFacade.VendaProduto((long)id, usuario.UserName);

            if (prod == false)
            {
                return NotFound();
            }

            return View(_businesFacade.ItemPorId((long)id));
        }

        public async Task<IActionResult> PerfilVendedor(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userName = _businesFacade.ItemPorId((long)id).NomeVendedor;

            var perfilVendedor = _businesFacade.vendasPerfil(userName);

            ViewData["prodID"] = (long)id;
            return View(perfilVendedor);
        }

        //recebe uma pergunta e id de produto e salva a pergunta
        public async Task<IActionResult> SalvaPergunta(long id, String per)
        {            

            _businesFacade.SalvaPergunta(id, per);

            return RedirectToAction("Details", "ProdutosDisponiveis", new { Id = id });

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

        public Pergunta GetPergunta(long id)
        {
            Pergunta per = _businesFacade.GetPergunta(id);
            if (per != null)
            {
                return per;
            }
            else
            {
                return null;
            }
        }

    }
}
