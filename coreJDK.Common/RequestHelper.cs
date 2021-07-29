using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace coreJDK.Common
{
    public class RequestHelper
    {
        #region HttpPost请求
        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string postString)
        {
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(postString);
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = webClient.UploadData(url, "POST", postData);
                string srcString = Encoding.UTF8.GetString(responseData);
                return srcString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region HttpGet请求
        public static string HttpGet(string Url, string postDataStr="")
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion

        #region HttpGet请求
        private static CookieContainer cookie1 = new CookieContainer();
        public static string OAuthHttpPost(string url, string postDataStr, List<KeyValuePair<string, string>> headers)
        {

            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(postDataStr);
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                headers.ForEach(head =>
                {
                    webClient.Headers.Add(head.Key, head.Value);
                });

                byte[] responseData = webClient.UploadData(url, "POST", postData);
                string srcString = Encoding.UTF8.GetString(responseData);
                return srcString;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 客户端post请求
        /// <summary>
        /// 客户端post请求
        /// </summary>
        private static CookieContainer cookie = new CookieContainer();
        public static string ClientPost(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                request.CookieContainer = cookie;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion

    }
}
