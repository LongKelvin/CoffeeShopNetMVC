using BotDetect.Web.Mvc;

using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.App_Start;
using CoffeeShop.Web.Models;

using Microsoft.AspNet.Identity.Owin;

using System;
using System.Net;
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

            successForm = successForm.Replace("{{DirectActionLink}}", Url.Action("Login", "Account"));

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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var userByEmail = _userManager.FindByEmailAsync(loginVM.Email);
            if (userByEmail.Result == null)
            {
                ModelState.AddModelError(loginVM.Email, "This email is not correct, Please checkout");
                return View(loginVM);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(userByEmail.Result.UserName, loginVM.Password,
                false, shouldLockout: false);
            return View();
        }
    }
}