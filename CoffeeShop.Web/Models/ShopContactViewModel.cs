namespace CoffeeShop.Web.Models
{
    public class ShopContactViewModel : ViewModelBase
    {
        public ShopContactViewModel()
        {
            ShopInfo = new ShopInfoViewModel();
            Feedback = new FeedbackViewModel();
        }

        public ShopInfoViewModel ShopInfo { get; set; }
        public FeedbackViewModel Feedback { get; set; }
    }
}