using BotDetect.Web.Mvc;

using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IShopInfoService _shopInfoService;
        private readonly IFeedbackService _feedbackService;

        public ContactController(IShopInfoService shopInfoService,
            IFeedbackService feedbackService,
            IErrorService errorService) : base(errorService)
        {
            _shopInfoService = shopInfoService;
            _feedbackService = feedbackService;
        }

        // GET: Contact
        public ActionResult Index()
        {
            var shopContactVm = GetContactInfo();
            return View(shopContactVm);
        }

        public ShopContactViewModel GetContactInfo()
        {
            var shopInfo = _shopInfoService.GetShopInfo();
            var contactInfoVm = AutoMapper.Mapper.Map<ShopInfoViewModel>(shopInfo);

            return new ShopContactViewModel
            {
                ShopInfo = contactInfoVm
            };
        }


        [HttpPost]
        //[CaptchaValidationActionFilter("CaptchaCode", "ContactCaptcha", "Captcha code invalid")]
        public ActionResult SendFeedback(ShopContactViewModel shopContactVM)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(shopContactVM.Feedback);
                newFeedback.CreatedDate = System.DateTime.Now;
                newFeedback.Status = true;


                _feedbackService.Add(newFeedback);
                _feedbackService.SaveChanges();

                ViewData["SuccessMsg"] = Resources.Resources.SendFeedbackStatus_OK;

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/templates/contact_template.html"));
                content = content.Replace("{{EmailSubject}}", shopContactVM.Feedback.EmailSubject);
                content = content.Replace("{{Name}}", shopContactVM.Feedback.Name);
                content = content.Replace("{{Email}}", shopContactVM.Feedback.Email);
                content = content.Replace("{{Message}}", shopContactVM.Feedback.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "[COFFEE_SHOP] - Thông tin liên hệ từ website", content);

                //feedbackVm.Name = "";
                //feedbackVm.Message = "";
                //feedbackVm.Email = "";
                //MvcCaptcha.ResetCaptcha("ContactCaptcha");
                return View(nameof(Index), GetContactInfo());
            }

           

            return View(nameof(Index),shopContactVM);
        }
    }
}