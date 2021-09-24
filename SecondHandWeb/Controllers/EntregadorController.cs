using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using PL.Context;
using Microsoft.AspNetCore.Identity;
using BLL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace SecondHandWeb.Controllers
{
    public class EntregadorController : Controller
    {

        private readonly BusinesFacade _businesFacade;
        public readonly UserManager<ApplicationUser> _userManager;

        public EntregadorController(BusinesFacade businesFacade,
                                   UserManager<ApplicationUser> userManager)
        {
            _businesFacade = businesFacade;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Entregador
        [Authorize]
        public async Task<IActionResult> ProdutosDisponiveisParaEntrega()
        {
            return View(_businesFacade.ItensParaEntrega());
        }

        // GET: Entregador/Details/5
        public IActionResult Details(long id)
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

            return View(produto);
        }
        public async Task<IActionResult> ProdutosEmRotaDeEntrega()
        {
            return View(_businesFacade.ItensEmRotaDeEntrega());
        }

        public async Task<IActionResult> ProdutoEntregue(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Boolean prod = _businesFacade.ProdutoEntregue((long)id);

            if (prod == true)
            {
                return View(_businesFacade.ItemPorId((long)id));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Entrega(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Pegando o usuário logado:
            var usuario = await _userManager.GetUserAsync(HttpContext.User);

            Boolean prod = _businesFacade.EntregaProduto((long)id, usuario.UserName);

            if (prod == false)
            {
                return NotFound();
            }

            return View(_businesFacade.ItemPorId((long)id));
        }
    }
}
