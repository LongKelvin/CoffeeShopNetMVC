﻿using CoffeeShop.Common;
using CoffeeShop.Models.Models;
using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Extensions;
using CoffeeShop.Web.Models;

using System;
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
        public ActionResult SendFeedbackToAdmin(ShopContactViewModel shopContactVM)
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

                //Fill out content to email
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/templates/feedback_template.html"));
                content = content.Replace("{{EmailSubject}}", shopContactVM.Feedback.EmailSubject);
                content = content.Replace("{{Name}}", shopContactVM.Feedback.Name);
                content = content.Replace("{{Email}}", shopContactVM.Feedback.Email);
                content = content.Replace("{{Message}}", shopContactVM.Feedback.Message);
                content = content.Replace("{{CreatedDate}}", DateTime.Now.ToString());

                //Get admin mail data
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "[COFFEE_WAY] - THƯ PHẢN HỒI TỪ KHÁCH HÀNG", content);

                SendResponseEmailToCustomer(shopContactVM.Feedback.Email, shopContactVM.Feedback.Name);

                //feedbackVm.Name = "";
                //feedbackVm.Message = "";
                //feedbackVm.Email = "";
                //MvcCaptcha.ResetCaptcha("ContactCaptcha");
                return View(nameof(Index), GetContactInfo());
            }

            return View(nameof(Index), shopContactVM);
        }

        public void SendResponseEmailToCustomer(string email, string name)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/templates/response_template.html"));
            content = content.Replace("{{Name}}", name);
            //Fill out shop contact info to email template
            var shopInfo = GetContactInfo();
            content = content.Replace("{{Telephone}}", shopInfo.ShopInfo.Telephone);
            content = content.Replace("{{ShopEmail}}", shopInfo.ShopInfo.Email);
            content = content.Replace("{{ShopAddress}}", shopInfo.ShopInfo.Address);
            content = content.Replace("{{ShopWebsiteLink}}", shopInfo.ShopInfo.Website);
            content = content.Replace("{{ShopName}}", shopInfo.ShopInfo.Name);

            MailHelper.SendMail(email, "[COFFEE_WAY] - THANKS FOR FEEDBACK", content);
        }
    }
}