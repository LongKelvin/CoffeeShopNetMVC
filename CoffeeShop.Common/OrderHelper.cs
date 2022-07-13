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

                case (int)CommonConstants.PaymentStatus.Canceled:

                    return CommonConstants.PaymentStatus.Canceled.ToString();

                default: return null;
            }
        }

        public static string GetOrderStatus(int status)
        {
            switch (status)
            {
                case (int)CommonConstants.OrderStatus.Pending:

                    return CommonConstants.OrderStatus.Pending.ToString();

                case (int)CommonConstants.OrderStatus.Confirmed:

                    return CommonConstants.OrderStatus.Confirmed.ToString();

                case (int)CommonConstants.OrderStatus.Processing:

                    return CommonConstants.OrderStatus.Processing.ToString();

                case (int)CommonConstants.OrderStatus.Shipping:

                    return CommonConstants.OrderStatus.Shipping.ToString();

                case (int)CommonConstants.OrderStatus.Complete:

                    return CommonConstants.OrderStatus.Complete.ToString();

                case (int)CommonConstants.OrderStatus.Canceled:

                    return CommonConstants.OrderStatus.Canceled.ToString();

                case (int)CommonConstants.OrderStatus.Refunded:

                    return CommonConstants.OrderStatus.Refunded.ToString();

                case (int)CommonConstants.OrderStatus.Falied:

                    return CommonConstants.OrderStatus.Falied.ToString();

                default: return null;
            }
        }

        public static string GetShippingStatus(int status)
        {
            switch (status)
            {
                case (int)CommonConstants.ShippingStatus.NotYetShipped:

                    return CommonConstants.ShippingStatus.NotYetShipped.ToString();

                case (int)CommonConstants.ShippingStatus.Shipping:

                    return CommonConstants.ShippingStatus.Shipping.ToString();

                case (int)CommonConstants.ShippingStatus.Delivered:

                    return CommonConstants.ShippingStatus.Delivered.ToString();

                case (int)CommonConstants.ShippingStatus.ShippingNotRequired:

                    return CommonConstants.ShippingStatus.ShippingNotRequired.ToString();

                case (int)CommonConstants.ShippingStatus.Canceled:

                    return CommonConstants.ShippingStatus.Canceled.ToString();

                default: return null;
            }
        }
    }
}