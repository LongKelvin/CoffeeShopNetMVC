namespace CoffeeShop.Common
{
    public class OrderHelper
    {
        public static string GetPaymentStatus(int status)
        {
            switch (status)
            {
                case (int)CommonConstants.PaymentStatus.Pending:

                    return CommonConstants.PaymentStatus.Pending.ToString();

                case (int)CommonConstants.PaymentStatus.Paid:

                    return CommonConstants.PaymentStatus.Paid.ToString();

                case (int)CommonConstants.PaymentStatus.Refunded:

                    return CommonConstants.PaymentStatus.Refunded.ToString();

                default: return null;
            }
        }

        public static string GetOrderStatus(int status)
        {
            switch (status)
            {
                case (int)CommonConstants.OrderStatus.Pending:

                    return CommonConstants.OrderStatus.Pending.ToString();

                case (int)CommonConstants.OrderStatus.Processing:

                    return CommonConstants.OrderStatus.Processing.ToString();

                case (int)CommonConstants.OrderStatus.Complete:

                    return CommonConstants.OrderStatus.Complete.ToString();

                case (int)CommonConstants.OrderStatus.Cancel:

                    return CommonConstants.OrderStatus.Cancel.ToString();

                case (int)CommonConstants.OrderStatus.Refunded:

                    return CommonConstants.OrderStatus.Refunded.ToString();

                default: return null;
            }
        }

        public static string GetShippingStatus(int status)
        {
            switch (status)
            {
                case (int)CommonConstants.ShippingStatus.NotYetShipped:

                    return CommonConstants.ShippingStatus.NotYetShipped.ToString();

                case (int)CommonConstants.ShippingStatus.Shipped:

                    return CommonConstants.ShippingStatus.Shipped.ToString();

                case (int)CommonConstants.ShippingStatus.Delivered:

                    return CommonConstants.ShippingStatus.Delivered.ToString();

                case (int)CommonConstants.ShippingStatus.ShippingNotRequired:

                    return CommonConstants.ShippingStatus.ShippingNotRequired.ToString();

                default: return null;
            }
        }
    }
}