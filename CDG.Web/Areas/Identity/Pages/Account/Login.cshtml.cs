// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CDG.DAL.Data;
using CDG.BLL;
using Ardalis.GuardClauses;
using CDG.BLL.Interfaces;

namespace CDG.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IBasketService basketService;
        private readonly IBasketQueryService basketQueryService;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger,
        IBasketService basketService,
        IBasketQueryService basketQueryService)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.basketService = basketService;
            this.basketQueryService = basketQueryService;

        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
        public string BasketCount = "";

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {

            [Required (ErrorMessage = "Обязательное поле")]
            [EmailAddress]
            public string Email { get; set; }

            [Required (ErrorMessage = "Обязательное поле")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Запомнить меня?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
            var count = await basketQueryService.CountTotalBasketItemsAsync(Input?.Email);
            BasketCount = count.ToString();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    await TransferAnonymousBasketToUserAsync(Input?.Email);

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        private async Task TransferAnonymousBasketToUserAsync(string userName)
        {
            if (Request.Cookies.ContainsKey(SD.BASKET_COOKIENAME))
            {
                var anonymousId = Request.Cookies[SD.BASKET_COOKIENAME];
                if (Guid.TryParse(anonymousId, out var _))
                {
                    Guard.Against.NullOrEmpty(userName, nameof(userName));
                    await basketService.TransferBasketAsync(anonymousId, userName);
                }
                Response.Cookies.Delete(SD.BASKET_COOKIENAME);
            }
        }
    }
}
