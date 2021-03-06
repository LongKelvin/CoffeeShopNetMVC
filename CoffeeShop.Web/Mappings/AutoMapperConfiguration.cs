using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Web.Models;

using Microsoft.Ajax.Utilities;

namespace CoffeeShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {

        public static void Configure()
        {


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
                cfg.CreateMap<Order, OrderViewModel>();
                cfg.CreateMap<ShopInformation, ShopInfoViewModel>();
                //cfg.CreateMap<ProductTag, ProductTagViewModel>();
                //cfg.CreateMap<Footer, FooterViewModel>();
                cfg.CreateMap<Slide, SlideViewModel>();
                cfg.CreateMap<Page, PageViewModel>();
                cfg.CreateMap<PaymentMethod, PaymentMethodViewModel>();
                //cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>();
                cfg.CreateMap<ApplicationPermission, ApplicationPermissionViewModel>();
                cfg.CreateMap<ApplicationNotification, ApplicationNotificationViewModel>();
            });


        }
    }
}