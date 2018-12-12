using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ImageManagement.Helper
{
    public class BaiduApiHelper
    {
        public  static readonly  string Token= "24.d5f0939a0cc1400f10c88fe3dbc26c83.2592000.1547088050.282335-15119804";
        /// <summary>
        /// 通用文字识别
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Models.ApiImgResultModel general_basic(string img)
        {
            string strbaser64 =img; // 图片的base64编码
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=" + Token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            String str = "image=" + HttpUtility.UrlEncode(strbaser64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("通用文字识别:");
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<Models.ApiImgResultModel>(result);
        }
        /// <summary>
        /// excel文字识别
        /// </summary>
        /// <param name="excel"></param>
        /// <returns></returns>
        public static Models.ApiExcelResultModel form_ocr_request(string excel)
        {
            string strbaser64 = excel; // 图片的base64编码
            string host = "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request?access_token=" + Token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            String str = "image=" + HttpUtility.UrlEncode(strbaser64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("excel提交:");
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<Models.ApiExcelResultModel>(result);
        }
        /// <summary>
        /// 获取excel的结果
        /// </summary>
        /// <param name="request_id">编号</param>
        /// <param name="type">不填为excel文件,填任意值为json格式</param>
        /// <returns></returns>
        public static Models.ApiExcelResultModel form_ocr_get_request_result(string request_id,int type)
        {
            string host = "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/get_request_result?access_token=" + Token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            String str = "request_id=" + HttpUtility.UrlEncode(request_id);
            if (type!=0)
            {
                str += ";result_type=json";
            }
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string result = reader.ReadToEnd();
            Console.WriteLine("excel提交:");
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<Models.ApiExcelResultModel>(result);
        }

       

    }
}
