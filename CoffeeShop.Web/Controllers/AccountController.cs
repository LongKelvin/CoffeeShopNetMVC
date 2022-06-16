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

		public AccountController ( ApplicationUserManager userManager, ApplicationSignInManager signInManager,
			IErrorService errorService, IShopInfoService shopInfoService ) : base(errorService)
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
		public ActionResult Index ( )
			{
			return View();
			}

		[HttpGet]
		public ActionResult Register ( )
			{
			return View();
			}

		[HttpPost]
		[CaptchaValidationActionFilter("CaptchaCode", "RegisterCaptcha", "Captcha code is incorrect, please try again")]
		public async Task<ActionResult> Register ( RegisterViewModel registerVM )
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

		public void SendResponseEmailToCustomer ( string userName, RegisterViewModel registerVM )
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
		public ActionResult Login ( string returnUrl )
			{
			ViewBag.ReturnUrl = returnUrl;
			return View();
			}

		[HttpPost]
		public ActionResult Login ( LoginViewModel loginVM, string returnUrl )
			{
			if (!ModelState.IsValid)
				{
				return View(loginVM);
				}

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
		public ActionResult LogOut ( )
			{
			IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
			authenticationManager.SignOut();
			return RedirectToAction("Index", "Home");
			}

		[HttpPost]
		public JsonResult GetAuthenticatedUser ( )
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

		//
		// POST: /Account/ExternalLogin
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult ExternalLogin ( string provider, string returnUrl )
			{
			ControllerContext.HttpContext.Session.RemoveAll();
			Session["Workaround"] = 0;
			// Request a redirect to the external login provider
			return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
			}

		//Custom method to get login info
		private async Task<ExternalLoginInfo> AuthenticationManager_GetExternalLoginInfoAsync_Workaround ( )
			{
			ExternalLoginInfo loginInfo = null;

			var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);

			if (result != null && result.Identity != null)
				{
				var idClaim = result.Identity.FindFirst(ClaimTypes.NameIdentifier);
				if (idClaim != null)
					{
					loginInfo = new ExternalLoginInfo()
						{
						DefaultUserName = result.Identity.Name == null ? "" : result.Identity.Name.Replace(" ", ""),
						Login = new UserLoginInfo(idClaim.Issuer, idClaim.Value),
						ExternalIdentity = result.Identity,
						Email = result.Identity.FindFirstValue(ClaimTypes.Email)
						};
					}
				}

			return loginInfo;
			}

		//
		// GET: /Account/ExternalLoginCallback
		[AllowAnonymous]
		public async Task<ActionResult> ExternalLoginCallback ( string returnUrl )
			{
			//var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

			var loginInfo = await AuthenticationManager_GetExternalLoginInfoAsync_Workaround();

			if (loginInfo == null)
				{
				return RedirectToAction("Login");
				}

			// Sign in the user with this external login provider if the user already has a login
			var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
			switch (result)
				{
				case SignInStatus.Success:
					return RedirectToLocal(returnUrl);

				case SignInStatus.LockedOut:
					return View("Lockout");

				case SignInStatus.RequiresVerification:
					return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

				case SignInStatus.Failure:
				default:
					// If the user does not have an account, then prompt the user to create an account
					ViewBag.ReturnUrl = returnUrl;
					ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
					return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
				}

			}


		#region Another solution for external login
		/// <summary>
		/// Another solution for external login
		/// //Solution 1
		//var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);
		//if (result == null || result.Identity == null)
		//	{
		//	return RedirectToAction("Login");
		//	}

		//var idClaim = result.Identity.FindFirst(ClaimTypes.NameIdentifier);
		//if (idClaim == null)
		//	{
		//	return RedirectToAction("Login");
		//	}

		//var login = new UserLoginInfo(idClaim.Issuer, idClaim.Value);
		//var name = result.Identity.Name == null ? "" : result.Identity.Name.Replace(" ", "");
		//var email = result.Identity.FindFirstValue(ClaimTypes.Email);

		//// Sign in the user with this external login provider if the user already has a login
		//var user = await UserManager.FindAsync(login);
		//if (user != null)
		//	{
		//	await SignInManager.SignInAsync(user, isPersistent: false, false);
		//	return RedirectToLocal(returnUrl);
		//	}
		//else
		//	{
		//	// If the user does not have an account, then prompt the user to create an account
		//	ViewBag.ReturnUrl = returnUrl;
		//	ViewBag.LoginProvider = login.LoginProvider;
		//	return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
		//	}
		/// </summary>
		/// <param name="model"></param>
		/// <param name="returnUrl"></param>
		/// <returns></returns>
		/// 
		#endregion

		//
		// POST: /Account/ExternalLoginConfirmation
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ExternalLoginConfirmation ( ExternalLoginConfirmationViewModel model, string returnUrl )
			{
			if (User.Identity.IsAuthenticated)
				{
				return RedirectToAction("Index", "Home");
				}

			if (ModelState.IsValid)
				{
				// Get the information about the user from the external login provider
				var info = await AuthenticationManager.GetExternalLoginInfoAsync();
				if (info == null)
					{
					return View("ExternalLoginFailure");
					}
				var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
				var result = await UserManager.CreateAsync(user);
				if (result.Succeeded)
					{
					result = await UserManager.AddLoginAsync(user.Id, info.Login);
					if (result.Succeeded)
						{
						await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
						return RedirectToLocal(returnUrl);
						}
					}
				AddErrors(result);
				}

			ViewBag.ReturnUrl = returnUrl;
			return View(model);
			}

		//
		// POST: /Account/LogOff
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff ( )
			{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Index", "Home");
			}

		//
		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public ActionResult ExternalLoginFailure ( )
			{
			return View();
			}

		protected override void Dispose ( bool disposing )
			{
			if (disposing)
				{
				if (_userManager != null)
					{
					_userManager.Dispose();
					_userManager = null;
					}

				if (_signInManager != null)
					{
					_signInManager.Dispose();
					_signInManager = null;
					}
				}

			base.Dispose(disposing);
			}

		#region Helpers

		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager
			{
			get
				{
				return HttpContext.GetOwinContext().Authentication;
				}
			}

		private void AddErrors ( IdentityResult result )
			{
			foreach (var error in result.Errors)
				{
				ModelState.AddModelError("", error);
				}
			}

		private ActionResult RedirectToLocal ( string returnUrl )
			{
			if (Url.IsLocalUrl(returnUrl))
				{
				return Redirect(returnUrl);
				}
			return RedirectToAction("Index", "Home");
			}

		internal class ChallengeResult : HttpUnauthorizedResult
			{
			public ChallengeResult ( string provider, string redirectUri )
				: this(provider, redirectUri, null)
				{
				}

			public ChallengeResult ( string provider, string redirectUri, string userId )
				{
				LoginProvider = provider;
				RedirectUri = redirectUri;
				UserId = userId;
				}

			public string LoginProvider { get; set; }
			public string RedirectUri { get; set; }
			public string UserId { get; set; }

			public override void ExecuteResult ( ControllerContext context )
				{
				//context.RequestContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

				var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
				if (UserId != null)
					{
					properties.Dictionary[XsrfKey] = UserId;
					}
				context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
				}
			}

		#endregion Helpers
		}
	}