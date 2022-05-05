using AutoMapper;

using CoffeeShop.Services;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;
        public PageController(IPageService pageServices)
        {
            _pageService = pageServices;
        }

        // GET: Page
        [OutputCache(Duration = 0, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageVM = Mapper.Map<PageViewModel>(page);    
            return View(pageVM);
        }
    }
}