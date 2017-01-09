using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FaceDetection.Services
{
    class HttpService
    {
        public String ExecuteRequest(String url, String method, string contentType, Byte[] postData = null)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response;
            String responseText;
            request.ContentType = contentType;
            request.Method = method;
            request.Headers.Add("Ocp-Apim-Subscription-Key", "2f37a6e26c4645ae8c7d760db35a5e41");
            Stream requetStream = request.GetRequestStream();

            
            if (postData != null)
                requetStream.Write(postData, 0, postData.Length);
            StreamReader sr;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());

            }
            catch (WebException ex)
            {
                //response = (HttpWebResponse)request.GetResponse();
                //sr = ex.Get
                //sr = new StreamReader(response.GetResponseStream());    
                sr = new StreamReader(ex.Response.GetResponseStream());
                //resCode = ex.Status.

            }



            responseText = sr.ReadToEnd();
            return responseText;
            
        }
    }
}
