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
using FaceDetection.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FaceDetection.Services
{
    class FaceAPI
    {

        //Detect will identify a face, and return the faceID. Use faceID in future calls. Kind of the baseline
        public DetectResponse Detect(Bitmap image = null)

        {


            String responseText = "Nothing";
            Byte[] imageArray;
            //
            imageArray = ImageToByte2(image);
            String contentType = ContentType.octect;
            String httpMethod = "Post";
            String url = "https://api.projectoxford.ai/face/v1.0/detect?returnFaceId=true&returnFaceAttributes=age,smile,gender,glasses";
            responseText = new HttpService().ExecuteRequest(url, httpMethod, contentType, imageArray);
            Console.WriteLine(responseText);


            DetectResponse dResponse = DetectResponseFromJson(responseText);

            
            return dResponse;
        }

        public bool CreatePersonGroup(String groupID)
        {
            String url = "https://api.projectoxford.ai/face/v1.0/persongroups/" + groupID;
            String body = "{\"name\":\"family\"}";
            String httpMethod = "Put";
            String contentType = ContentType.json;
            byte[] bytes = Encoding.ASCII.GetBytes(body);
            String responseText;
            responseText = new HttpService().ExecuteRequest(url, httpMethod, contentType, bytes);
            if (String.IsNullOrEmpty(responseText))
                return true;
            else
            {
                return false;
            }
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
