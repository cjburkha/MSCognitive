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
            


            if (postData != null)
            {
                Stream requetStream = request.GetRequestStream();
                requetStream.Write(postData, 0, postData.Length);
            }
                
            StreamReader sr;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());

            }
                
            catch (WebException ex)
            {
                //We want some of them to bubble up?
                if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.Conflict)
                {
                    //The problem here is we don't have the context for the error. Which group? Which person?
                    //This is going to happen and is ok.
                    String err = String.Format("Received 409 for {0}. This is ok", url);
                    Console.WriteLine(err);
                    return "";
                }
                  
                throw ex;

                //sr = new StreamReader(ex.Response.GetResponseStream());
                //resCode = ex.Status.

            }



            responseText = sr.ReadToEnd();
            return responseText;
            
        }
    }
}
