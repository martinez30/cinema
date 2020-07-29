using Cine.Backoffice.Controllers;
using Cine.Backoffice.Models.AccountViewModels;
using Infra;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cine.Backoffice.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CineContext _context;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            CineContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }


        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var list = new List<ListViewModel>();
        //    foreach (var user in _userManager.Users.Include(x => x.Person))
        //    {
        //        list.Add(new ListViewModel
        //        {
        //            Id = user.Id.ToString(),
        //            Nome = $"{user.Person.FirstName} {user.Person.LastName}",
        //            Email = user.Email
        //        });
        //    }
        //    return View(list);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByNameAsync(model.Email);
                var result = Microsoft.AspNetCore.Identity.SignInResult.Failed;

                if (user != null)
                    result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {

                    if (returnUrl != null)
                        return RedirectToLocal(returnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário ou Senha inválido.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser(model.Email);
            var createresult = await _userManager.CreateAsync(user);
            if(createresult.Succeeded)
            {
                var createpass =  await _userManager.AddPasswordAsync(user, model.Password);
                if(createpass.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", createpass.Errors.First().Description);
                }
            }
            else
            {
                ModelState.AddModelError("", createresult.Errors.First().Description);
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            //if (ModelState.IsValid)
            //{
            //    // Get the information about the user from the external login provider
            //    var info = await _signInManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        throw new ApplicationException("Error loading external login information during confirmation.");
            //    }
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //    var result = await _userManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        result = await _userManager.AddLoginAsync(user, info);
            //        if (result.Succeeded)
            //        {
            //            await _signInManager.SignInAsync(user, isPersistent: false);
            //            _logger.LogDebug("User created an account using {Name} provider.", info.LoginProvider);
            //            return RedirectToLocal(returnUrl);
            //        }
            //    }
            //    AddErrors(result);
            //}

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return View(nameof(ForgotPasswordConfirmation), model);

            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.ResetPasswordCallbackLink(code, Request.Scheme);
            //await _emailSender.LostPasswordAsync(user.Email, user.Person.FirstName, $"{user.Person.FirstName} {user.Person.LastName}", callbackUrl);

            return View(nameof(ForgotPasswordConfirmation), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation(ForgotPasswordViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dbuser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            var user = await _userManager.FindByIdAsync(dbuser?.Id);

            if (dbuser == null || user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterPassword(string email = null, string code = null)
        {
            if (email == null)
                throw new ApplicationException("An e-mail must be supplied for password registration.");

            if (code == null)
                throw new ApplicationException("A code must be supplied for password registration.");

            var model = new RegisterPasswordViewModel
            {
                Email = email,
                Code = code,
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPassword(RegisterPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(Login));
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                _context.SaveChanges();

                var resultLogin = Microsoft.AspNetCore.Identity.SignInResult.Failed;

                if (resultLogin.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction(nameof(Login));
            }

            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(string code = null)
        {
            return View(new ChangePasswordViewModel() { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                ViewBag.Message = Messages.PasswordChanged;
                ViewBag.Success = true;
            }
            AddErrors(result);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser(string message = "", bool? success = null)
        {
            var model = new EditUserViewModel();

            var applicationsUser = await _userManager.GetUserAsync(this.User);

            if (applicationsUser != null)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == applicationsUser.Id);

                model.Email = user.Email;
            }

            if (success.HasValue)
            {
                ViewBag.Message = message;
                ViewBag.Success = success.Value;
            }

            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(10_485_760)]
        [Authorize]
        public async Task<IActionResult> EditUser(IFormCollection data)
        {
            var applicationsUser = await _userManager.GetUserAsync(this.User);
            var user = _userManager.Users.FirstOrDefault(x => x.Id == applicationsUser.Id);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { message = Messages.DataChanged, success = true });
            }
            return Json(new { message = result.Errors.First().Description, success = false });

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            if (result.Errors.Any())
                ModelState.AddModelError(string.Empty, result.Errors.First().Description);

            //foreach (var error in result.Errors)
            //{
            //}
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
