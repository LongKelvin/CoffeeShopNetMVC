using CoffeeShop.Models.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public static class ModelsEntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory source, PostCategory target)
        {
            target.ID = source.ID;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Alias = source.Alias;
            target.ParentID = source.ParentID;
            target.DisplayOrder = source.DisplayOrder;
            target.Images = source.Images;
            target.HomeFlag = source.HomeFlag;

            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.UpdatedDate = source.UpdatedDate;
            target.UpdatedBy = source.UpdatedBy;
            target.MetaKeyword = source.MetaKeyword;
            target.MetaDescription = source.MetaDescription;
            target.Status = source.Status;
            target.RowVersion = source.RowVersion;
        }

        public static void UpdateProductCategory(this ProductCategory source, ProductCategory target)
        {
            target.ID = source.ID;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Alias = source.Alias;
            target.ParentID = source.ParentID;
            target.DisplayOrder = source.DisplayOrder;
            target.Images = source.Images;
            target.HomeFlag = source.HomeFlag;

            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.UpdatedDate = source.UpdatedDate;
            target.UpdatedBy = source.UpdatedBy;
            target.MetaKeyword = source.MetaKeyword;
            target.MetaDescription = source.MetaDescription;
            target.Status = source.Status;
            target.RowVersion = source.RowVersion;
        }

        public static void UpdatePost(this Post source, Post target)
        {
            target.ID = source.ID;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Alias = source.Alias;
            target.CategoryID = source.CategoryID;
            target.Content = source.Content;
            target.Images = source.Images;
            target.HomeFlag = source.HomeFlag;
            target.ViewCount = source.ViewCount;

            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.UpdatedDate = source.UpdatedDate;
            target.UpdatedBy = source.UpdatedBy;
            target.MetaKeyword = source.MetaKeyword;
            target.MetaDescription = source.MetaDescription;
            target.Status = source.Status;
            target.RowVersion = source.RowVersion;
        }

        public static void UpdateProduct(this Product source, Product target)
        {
            //Only update the property of Product
            //not contain it relative collection
            target.ID = source.ID;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Alias = source.Alias;
            target.CategoryID = source.CategoryID;
            target.Content = source.Content;
            target.Images = source.Images;
            target.MoreImages = source.MoreImages;
            target.Price = source.Price;
            target.PromotionPrice = source.PromotionPrice;
            target.Warranty = source.Warranty;
            target.HomeFlag = source.HomeFlag;
            target.ViewCount = source.ViewCount;
            target.HotFlag = source.HotFlag;
            target.ManufacturingDate = source.ManufacturingDate;
            target.ExpireDate = source.ExpireDate;
            target.Quantity = source.Quantity;
            target.CreatedDate = source.CreatedDate;
            target.CreatedBy = source.CreatedBy;
            target.UpdatedDate = source.UpdatedDate;
            target.UpdatedBy = source.UpdatedBy;
            target.MetaKeyword = source.MetaKeyword;
            target.MetaDescription = source.MetaDescription;
            target.Status = source.Status;
            target.RowVersion = source.RowVersion;
            target.OriginalPrice = source.OriginalPrice;

        }

        public static void UpdateTag(this Tag source, Tag target)
        {
            target.ID = source.ID;
            target.Name = source.Name;
            target.Type = source.Type;
        }

    }
}
