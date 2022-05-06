using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IShopInfoService _shopInfoService;

        public ContactController(IShopInfoService shopInfoService, IErrorService errorService) : base(errorService)
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