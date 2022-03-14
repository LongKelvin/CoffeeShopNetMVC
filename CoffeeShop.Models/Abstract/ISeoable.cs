namespace CoffeeShop.Models.Abstract
{
    public interface ISeoable
    {
        string MetaKeyword { get; set; } // nvarchar(250), null

        string MetaDescription { get; set; } // nvarchar(250), null
    }
}