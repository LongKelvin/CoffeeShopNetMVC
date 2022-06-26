using CoffeeShop.Common;
using CoffeeShop.Web.Infrastucture.PaymentIntegrated;

using Newtonsoft.Json.Linq;

namespace CoffeeShop.Web.Models.MomoPayment
{
    public class MomoPaymentRequestModel
    {
        public MomoPaymentRequestModel()
        {
            partnerCode = ConfigHelper.GetByKey(CommonConstants.MOMO_PARTNER_CODE);
            accessKey = ConfigHelper.GetByKey(CommonConstants.MOMO_ACCESS_KEY);
            redirectUrl = ConfigHelper.GetByKey(CommonConstants.MOMO_REDIRECT_URL);
            ipnUrl = ConfigHelper.GetByKey(CommonConstants.MOMO_IPN_URL);
            requestType = ConfigHelper.GetByKey(CommonConstants.MOMO_DEFAULT_REQUEST_TYPE);
            secretKey = ConfigHelper.GetByKey(CommonConstants.MOMO_SECRET_KEY);
        }

        public string partnerCode { get; set; }
        public string partnerName { get; set; }
        public string accessKey { get; set; }
        public string secretKey { get; set; }
        public string requestId { get; set; }
        public string amount { get; set; }
        public string orderInfo { get; set; }
        public string orderId { get; set; }
        public string redirectUrl { get; set; }
        public string ipnUrl { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }
        public string requestType { get; set; }
        public string lang { get; set; }
        public string storeId { get; set; }

        public string ToRawHashString()
        {
            return "accessKey=" + accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;
        }

        public JObject ToJObject()
        {
            return new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }
            };
        }
    }

    public class MomoPaymentConfigModel
    {
        public MomoPaymentConfigModel()
        {
            partnerCode = ConfigHelper.GetByKey(CommonConstants.MOMO_PARTNER_CODE);
            accessKey = ConfigHelper.GetByKey(CommonConstants.MOMO_ACCESS_KEY);
            secretKey = ConfigHelper.GetByKey(CommonConstants.MOMO_SECRET_KEY);
            endpoint = ConfigHelper.GetByKey(CommonConstants.MOMO_API_ENDPOINT);
        }

        public string partnerCode { get; set; }
        public string accessKey { get; set; }
        public string secretKey { get; set; }
        public string endpoint { get; set; }
    }
}