using BotDetect.Web.Mvc;

using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.App_Start;
using CoffeeShop.Web.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IShopInfoService _shopInfoService;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IErrorService errorService, IShopInfoService shopInfoService) : base(errorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _shopInfoService = shopInfoService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "RegisterCaptcha", "Captcha code is incorrect, please try again")]
        public async Task<ActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);

            //Find dupplicate email
            var dupplicateUser = await _userManager.FindByEmailAsync(registerVM.Email);
            if (dupplicateUser != null)
            {
                ModelState.AddModelError(registerVM.Email, "Email aready existed!");
                return View(registerVM);
            }

            //Find dupplicate username
            var userByUserName = await _userManager.FindByNameAsync(registerVM.UserName);
            if (userByUserName != null)
            {
                ModelState.AddModelError(registerVM.UserName, "Account aready existed!, Please choose a different User Name!");
                return View(registerVM);
            }

            ApplicationUser newUser = new ApplicationUser
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                EmailConfirmed = true,
                BirthDay = System.DateTime.Now
            };

            //Create user
            await _userManager.CreateAsync(newUser, registerVM.Password);

            //Check user status
            var newRegisterUser = await _userManager.FindByEmailAsync(newUser.Email);

            if (newRegisterUser != null)
            {
                await _userManager.AddToRolesAsync(newRegisterUser.Id, new string[] { "User" });
            }

            ViewData["SuccessMsg"] = "Create account successfull";

            string successForm = System.IO.File.ReadAllText(
                   Server.MapPath("/Assets/Client/form/success-form/success_form.html"));

            successForm = successForm.Replace("{{DirectActionLink}}",
                Url.Action("Login", "Account"));

            //Send mail to new register account
            SendResponseEmailToCustomer(registerVM.UserName, registerVM);

            //Update view
            ViewBag.SuccessForm = successForm;
            MvcCaptcha.ResetCaptcha("ContactCaptcha");
            return View(registerVM);
        }

        public void SendResponseEmailToCustomer(string userName, RegisterViewModel registerVM)
        {
            string content = System.IO.File.ReadAllText(
                Server.MapPath("/Assets/Client/templates/register_member_template.html"));
            content = content.Replace("{{Name}}", userName);
            //Fill out shop contact info to email template
            var shopInfo = _shopInfoService.GetShopInfo();

            content = content.Replace("{{Telephone}}", shopInfo.Telephone);
            content = content.Replace("{{ShopEmail}}", shopInfo.Email);
            content = content.Replace("{{ShopAddress}}", shopInfo.Address);
            content = content.Replace("{{ShopWebsiteLink}}", shopInfo.Website);
            content = content.Replace("{{ShopName}}", shopInfo.Name);

            //User info
            content = content.Replace("{{UserName}}", userName);
            content = content.Replace("{{AccountEmail}}", registerVM.Email);
            content = content.Replace("{{AccountCreatedDate}}", DateTime.Now.ToString());

            string currentLink = ConfigHelper.GetByKey("CurrentLink");
            content = content.Replace("{{ShopWebsiteLink}}", currentLink);

            MailHelper.SendMail(registerVM.Email, "[COFFEE_WAY] - WELCOME TO COFFEE WAY ", content);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginVM, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            //var userByEmail = _userManager.FindByEmail(loginVM.Email);
            //if (userByEmail == null)
            //{
            //    ModelState.AddModelError(loginVM.Email, "The email or password is not correct, Please checkout");
            //    return View(loginVM);
            //}

            var user = _userManager.Find(loginVM.UserName, loginVM.Password);
            if (user == null)
            {
                ModelState.AddModelError(loginVM.UserName, "The user name or password is not correct, Please checkout");
                return View(loginVM);
            }

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            ClaimsIdentity claimsIdentity = _userManager.CreateIdentity(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationProperties properties = new AuthenticationProperties
            {
                IsPersistent = loginVM.RememberMe,
            };

            authenticationManager.SignIn(properties, claimsIdentity);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult GetAuthenticatedUser()
        {
            if (!Request.IsAuthenticated)
                return Json(new { status = false });

            var userId = User.Identity.GetUserId();
            var result = _userManager.FindById(userId);
            return Json(new
            {
                status = true,
                data = result
            });
        }
    }
}