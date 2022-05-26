using AutoMapper;

using CoffeeShop.Models.Models;
using CoffeeShop.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeShop.Web.Infrastucture.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Images = postCategoryVm.Images;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;
            postCategory.RowVersion = postCategoryVm.RowVersion;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Description = productCategoryVm.Description;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.Images = productCategoryVm.Images;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;

            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.Status = productCategoryVm.Status;
            productCategory.RowVersion = productCategoryVm.RowVersion;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Images = postVm.Images;
            post.HomeFlag = postVm.HomeFlag;
            post.ViewCount = postVm.ViewCount;

            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
            post.RowVersion = postVm.RowVersion;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Description = productVm.Description;
            product.Alias = productVm.Alias;
            product.CategoryID = productVm.CategoryID;
            product.Content = productVm.Content;
            product.Images = productVm.Images;
            product.MoreImages = productVm.MoreImages;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Warranty = productVm.Warranty;
            product.HomeFlag = productVm.HomeFlag;
            product.ViewCount = productVm.ViewCount;
            product.HotFlag = productVm.HotFlag;
            product.ManufacturingDate = productVm.ManufacturingDate;
            product.ExpireDate = productVm.ExpireDate;
            product.Quantity = productVm.Quantity;
            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;
            product.RowVersion = productVm.RowVersion;

            if (string.IsNullOrEmpty(productVm.TagsString))
                return;
            //Extract tag from List<string>
            GetProductTagsFromTagsString(productVm.TagsString, productVm);

            //Assign tag to Product
            foreach (var tag in productVm.Tags)
            {
                Tag t = new Tag();
                t.UpdateTag(tag);
                product.Tags.Add(t);
            }
        }

        public static void UpdateTag(this Tag tag, TagViewModel tagVm)
        {
            tag.ID = tagVm.ID;
            tag.Name = tagVm.Name;
            tag.Type = tagVm.Type;
        }

        public static void GetProductTagsFromTagsString(string tags, ProductViewModel productVm)
        {
            if (string.IsNullOrEmpty(tags))
                return;

            var listTag = new List<TagViewModel>();
            char[] separators = new char[] { ' ', '.', ',', ';' };
            List<string> tagStr = tags.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim()).ToList();
            foreach (string t in tagStr)
            {
                string uTag = Common.StringHelper.ToUnsignString(t);

                listTag.Add(new TagViewModel
                {
                    Name = uTag,
                    ID = uTag,
                    Type = Common.CommonConstants.ProductTag
                });
            }
            productVm.Tags = listTag;
        }

        public static string GetTagStringFromProductTags(Product product)
        {
            if (product == null || product.Tags == null || product.Tags.Count <= 0)
                return null;

            var lastItem = product.Tags.Last();
            StringBuilder tags = new StringBuilder();
            foreach (var tag in product.Tags)
            {
                if (tag.Equals(lastItem))
                {
                    tags.Append(tag.Name);
                }
                else
                {
                    tags.Append(tag.Name);
                    tags.Append(';');
                }
            }
            return tags.ToString();
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.ID = slideVm.ID;
            slide.Title = slideVm.Title;
            slide.Description = slideVm.Description;
            slide.Images = slideVm.Images;
            slide.URL = slideVm.URL;
            slide.ActionName = slideVm.ActionName;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.Status = slideVm.Status;
            slide.RowVersion = slideVm.RowVersion;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVM)
        {
            feedback.ID = feedbackVM.ID;
            feedback.Email = feedbackVM.Email;
            feedback.Name = feedbackVM.Name;
            feedback.Status = feedbackVM.Status;
            feedback.EmailSubject = feedbackVM.EmailSubject;
            feedback.RowVersion = feedbackVM.RowVersion;
            feedback.Message = feedbackVM.Message;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerName;
            order.CustomerEmail = orderVm.CustomerName;
            order.CustomerMobile = orderVm.CustomerName;
            order.CustomerMessage = orderVm.CustomerName;
            order.PaymentMethodID= orderVm.PaymentMethodCode;
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.Status = orderVm.Status;
            order.PaymentStatus = orderVm.PaymentStatus;
            //order.OrderDetails = Mapper.Map<List<OrderDetail>>(orderVm);
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
            appGroup.Description = appGroupViewModel.Description;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "Add")
        {
            if (action == "Update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
            appRole.IsSystemProtected = false;
        }
        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "Add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }
    }
}