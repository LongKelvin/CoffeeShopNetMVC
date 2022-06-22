namespace CoffeeShop.Web.Models
{
    public class ViewModelBase
    {
        public int ID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}