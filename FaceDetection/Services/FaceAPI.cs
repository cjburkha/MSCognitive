using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Net;
using System.IO;
using FaceDetection.Model.Detect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FaceDetection.Services
{
    class FaceAPI
    {

        //Detect will identify a face, and return the faceID. Use faceID in future calls. Kind of the baseline
        public bool Detect(Bitmap image = null)

        {
            
            
            
            Byte[] imageArray;
            //
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.projectoxford.ai/face/v1.0/detect?returnFaceId=true&returnFaceAttributes=age,smile,gender,glasses");
            HttpWebResponse response;
            String responseText = "none";
            int resCode = 200;
            request.ContentType = "application/octet-stream";
            request.Method = "Post";
            request.Headers.Add("Ocp-Apim-Subscription-Key", "2f37a6e26c4645ae8c7d760db35a5e41");
            Stream requetStream =  request.GetRequestStream();

            imageArray = ImageToByte2(image);
            requetStream.Write(imageArray, 0, imageArray.Length);
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
            Console.WriteLine(responseText);


            DetectResponse dResponse = DetectResponseFromJson(responseText);


            //o.
            //JsonP
            //Json


            

            return true;
        }

        private DetectResponse DetectResponseFromJson(string json)
        {
            //String response = "[{\"faceId\":\"72cc02ca-aed2-4646-87db-96ba188ea43c\",\"faceRectangle\":{\"top\":100,\"left\":220,\"width\":48,\"height\":48}}]";
            JArray a = JArray.Parse(json);

            if (a.Count <= 0)
                return null;

            DetectResponse dResponse = a[0].ToObject<DetectResponse>();

            return dResponse;
        }

        private static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
