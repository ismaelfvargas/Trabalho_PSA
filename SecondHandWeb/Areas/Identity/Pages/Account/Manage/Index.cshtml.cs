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
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BusinesFacade _businesFacade;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            BusinesFacade _bf)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _businesFacade = _bf;
        }

        public string Username { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Reputacao { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Cep")]
            public string CEP { get; set; }

            [Display(Name = "Endereço")]
            public string Endereco { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var Usuario = await _userManager.GetUserAsync(User);

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var cep = Usuario.CEP;
            var endereco = Usuario.Endereco;
            var reputacao = Usuario.ReputacaoFinal;

            Username = userName;
            Cep = cep;
            Endereco = endereco;
            Reputacao = reputacao.ToString();

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                CEP = cep,
                Endereco = endereco
            };
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

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var cep = user.CEP;
            var endereco = user.Endereco;

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.CEP != cep)
            {
                user.CEP = Input.CEP;
                await _userManager.UpdateAsync(user);
                if (user.CEP == null)
                {
                    StatusMessage = "Unexpected error when trying to set a cep number.";
                    return RedirectToPage();
                }
            }

            if (Input.Endereco != endereco)
            {
                user.Endereco = Input.Endereco;
                await _userManager.UpdateAsync(user);
                if (user.Endereco == null)
                {
                    StatusMessage = "Unexpected error when trying to set a endereço.";
                    return RedirectToPage();
                }
                _businesFacade.AlteraEndProdutoAvend(user.UserName, user.Endereco);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
