using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecondHandWeb.Areas.Identity.Pages.Account.Manage
{
    public partial class InfoProfileComprasModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BusinesFacade _bussinesFacade;

        public InfoProfileComprasModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            BusinesFacade bf)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bussinesFacade = bf;
        }

        [Display(Name = "Produtos Comprados")]
        public string ProdutosComprados { get; set; }

        [Display(Name = "Produtos em rota de entrega")]
        public string ProdutosCompradosEmRotaDeEntrega { get; set; }

        [Display(Name = "Produtos entregue")]
        public string ProdutosCompradosEntregue { get; set; }

        [Display(Name = "Produtos com venda negada")]
        public string ProdutosComVendaNegada { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
                        

        private async Task LoadAsync(ApplicationUser user)
        {
            var Usuario = _bussinesFacade.comprasPerfil(user.UserName);

            ProdutosComprados = Usuario.ProdutosComprados.ToString();
            ProdutosCompradosEmRotaDeEntrega = Usuario.ProdutosCompradosEmRotaDeEntrega.ToString();
            ProdutosCompradosEntregue = Usuario.ProdutosCompradosEntregue.ToString();
            ProdutosComVendaNegada = Usuario.ProdutosComVendaNegada.ToString();

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
