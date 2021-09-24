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
    public partial class InfoProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BusinesFacade _bussinesFacade;

        public InfoProfileModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            BusinesFacade bf)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bussinesFacade = bf;
        }

        [Display(Name = "Produtos vendidos")]
        public string vendidos { get; set; }

        [Display(Name = "Produtos a venda")]
        public string aVenda { get; set; }

        [Display(Name = "Produtos bloqueados")]
        public string bloqueados { get; set; }

        [Display(Name = "Produtos aguardando aprovação de venda")]
        public string aguardandoAprov { get; set; }

        [Display(Name = "Produtos em rota de entrega")]
        public string emRotaEntrega { get; set; }

        [Display(Name = "Produtos entregue")]
        public string entregue { get; set; }

        [Display(Name = "Reputação")]
        public string reputacao { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
                        

        private async Task LoadAsync(ApplicationUser user)
        {
            var Usuario = _bussinesFacade.vendasPerfil(user.UserName);

            vendidos = Usuario.ProdutosVendido.ToString();
            aVenda = Usuario.ProdutosAVenda.ToString();
            bloqueados = Usuario.ProdutosBloqueado.ToString();
            aguardandoAprov = Usuario.ProdutosAguardandoApVenda.ToString();
            emRotaEntrega = Usuario.ProdutosEmRotaDeEntrega.ToString();
            entregue = Usuario.ProdutosEntregue.ToString();
            reputacao = Usuario.ReputacaoFinal.ToString();

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
