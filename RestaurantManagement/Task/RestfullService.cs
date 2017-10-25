using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
namespace RestaurantManagement.Task
{
   public class RestfullService
    {    
        const string restConnection = "http://localhost:53534/api/";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public dynamic Connection(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return content;
        }
        public T ServiceResult<T>(string obj,int? objId=null)
        {
            T result = default(T);
            var sendUrl = objId != null ? obj + "/" + objId.Value : obj + "/";
            var restService = new RestfullService();
            var resultOfService = restService.Connection(restConnection + sendUrl);
            var allMenus = (T)serializer.Deserialize<T>(resultOfService);
            result = (T)Convert.ChangeType(allMenus, typeof(T));
            return result;
        }
    }
}
