using CoffeeShop.Services;
using CoffeeShop.Web.Infrastucture.Core;

using System;
using System.IO;
using System.Net;
using System.Text;

namespace CoffeeShop.Web.Infrastucture.PaymentIntegrated
{
    public class MomoPaymentRequest
    {
        public static string SendPaymentRequest(string endPoint, string postJsonString)
        {
            var _serviceFactory = ServiceFactory.Get<IErrorService>();
            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endPoint);

                var postData = postJsonString;

                var data = Encoding.UTF8.GetBytes(postData);

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";

                httpWReq.ContentLength = data.Length;
                httpWReq.ReadWriteTimeout = 30000;
                httpWReq.Timeout = 15000;
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string jsonresponse = "";

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }

                //todo parse it
                return jsonresponse;
                //return new MomoResponse(mtid, jsonresponse);
            }
            catch (WebException e)
            {
                _serviceFactory.LogError(e);
                return e.Message;
            }
            catch (Exception ex)
            {
                _serviceFactory.LogError(ex);
                return ex.Message;
            }
        }
    }
}