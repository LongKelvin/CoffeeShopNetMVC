using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IShopInfoService _shopInfoService;
        public ContactController(IShopInfoService shopInfoService)
        {
            _shopInfoService = shopInfoService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            var contactInfo = _shopInfoService.GetShopInfo();
            var contactInfoVm = AutoMapper.Mapper.Map<ShopInfoViewModel>(contactInfo);
            return View(contactInfoVm);
        }
    }
}