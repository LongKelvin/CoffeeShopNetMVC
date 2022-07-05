namespace CoffeeShop.Common
{
    public static class CommonConstants
    {
        public const string ProductTag = "Product";
        public const string PostTag = "Post";
        public const string DefaultFooterId = "default";

        public const string SessionCart = "SessionCart";
        public const string SessionCartCurrentItem = "SessionCartCurrentItem";
        public const string SessionCurrentOrderID = "SessionCurrentOrderID";

        public const string HomeTitle = "HomeTitle";
        public const string HomeMetaKeyword = "HomeMetaKeyword";
        public const string HomeMetaDescription = "HomeMetaDescription";

        public const string Administrator = "Administrator";

        public const string PermissionsType = "Permission";

        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string BasicUser = "BasicUser";

        public const string MomoPaymentInfo = "Thanh toán hóa đơn CoffeeWay qua MOMO";

        public const string API_ApplicationPermission = "api/ApplicationPermission";
        public const string API_Account = "api/Account";
        public const string API_ApplicationGroup = "api/ApplicationGroup";
        public const string API_ApplicationRole = "api/ApplicationRole";
        public const string API_ApplicationUser = "api/ApplicationUser";
        public const string API_Home = "api/Home";
        public const string API_PostCategory = "api/PostCategory";
        public const string API_ProductCategory = "api/ProductCategory";
        public const string API_Product = "api/Product";
        public const string API_Slide = "api/Slide";
        public const string API_Statistic = "api/Statistic";
        public const string API_Order = "api/Order";

        public const string FacebookAppId = "FacebookAppId";
        public const string FacebookAppSecret = "FacebookAppSecret";

        public const string GoogleClientId = "GoogleClientId";
        public const string GoogleClientSecret = "GoogleClientSecret";

        public const string EXCEL_UPLOAD_PATH = "ExcelUploadPath";
        public const string EXCEL_EXPORT_PATH = "ExcelExportPath";

        public const string PDF_EXPORT_PATH = "PdfExportPath";

        //Momo Payment Config
        public const string MOMO_PARTNER_CODE = "MOMO_PARTNER_CODE";

        public const string MOMO_ACCESS_KEY = "MOMO_ACCESS_KEY";
        public const string MOMO_SECRET_KEY = "MOMO_SECRET_KEY";
        public const string MOMO_API_ENDPOINT = "MOMO_API_ENDPOINT";
        public const string MOMO_DEFAULT_REQUEST_TYPE = "MOMO_DEFAULT_REQUEST_TYPE";
        public const string MOMO_REDIRECT_URL = "MOMO_REDIRECT_URL";
        public const string MOMO_IPN_URL = "MOMO_IPN_URL";
        public const string MOMO_PAYMENT_INFO = "MOMO_PAYMENT_INFO";

        public enum Sex
        {
            Male = 0,
            Female = 1,
            Other = 2
        }

        //PAYMENT CODE
        public const int PAYMENT_SHIPCOD = 100;

        public const int PAYMENT_MOMO = 101;
        public const int PAYMENT_ZALO_PAY = 102;
        public const int PAYMENT_INTERNET_BANKING = 103;
        public const int PAYMENT_CREDIT_CARD = 104;

        public enum PaymentMethodCode
        {
            SHIPCOD = 100,
            MOMO = 101,
            ZALO_PAY = 102,
            INTERNET_BANKING = 103,
            CREDIT_CARD = 104
        }

        //Order status
        public enum OrderStatus
        {
            Pending,
            Confirmed,
            Processing,
            Shipping,
            Complete,
            Canceled,
            Refunded,
            Falied
        }

        //Payment status
        public enum PaymentStatus
        {
            Pending,
            Paid,
            Refunded,
            Canceled
        }

        //Shipping status
        public enum ShippingStatus
        {
            NotYetShipped,
            Shipping,
            Delivered,
            ShippingNotRequired,
            Canceled
        }
    }
}