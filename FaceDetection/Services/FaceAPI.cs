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
using FaceDetection.Model.People;
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

        public bool CreatePersonGroup(String groupID, String description)
        {
            String url = "https://api.projectoxford.ai/face/v1.0/persongroups/" + groupID;
            String body = String.Format("{{\"name\":\"{0}\", \"userData\": \"{1}\"}}", groupID, description);
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

        

        public String CreatePerson(String groupID, String personName, String personData)
        {
            String url = String.Format("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/{0}/persons", groupID);
            //String body = String.Format("{\"name\":\"{0}\", \"userData\":\"{1}\"}", personName);
            String body = @"{{""name"":""{0}"", ""userData"":""{1}""}}";
            body = String.Format(body, personName, personData);
            String httpMethod = "Post";
            String contentType = ContentType.json;
            byte[] bytes = Encoding.ASCII.GetBytes(body);
            String responseText;
            responseText = new HttpService().ExecuteRequest(url, httpMethod, contentType, bytes);

            return responseText;
        }

        public Dictionary<String, PersonResponse> GetPersons(String groupID)
        {
            String url = String.Format("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/{0}/persons", groupID);
            String httpMethod = "Get";
            String contentType = ContentType.json;
            
            String responseText;
            responseText = new HttpService().ExecuteRequest(url, httpMethod, contentType);

            return PersonRepsonseFromJSON(responseText);
        }

        public bool DeletePerson(String groupID, String personID)
        {
            String url =
                String.Format("https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/{0}/persons/{1}",groupID, personID);
            String httpMethod = "Delete";
            String contentType = ContentType.json;

            String responseText;
            responseText = new HttpService().ExecuteRequest(url, httpMethod, contentType);

            return true;
        }

        //These can split out
        private Dictionary<String, PersonResponse> PersonRepsonseFromJSON(string json)
        {
            JArray a = JArray.Parse(json);
            Dictionary<String, PersonResponse> allPersons = new Dictionary<string, PersonResponse>();
            if (a.Count <= 0)
                return null;

            foreach (var personResponse in a)
            {
               PersonResponse pResponse =  personResponse.ToObject<PersonResponse>();
               allPersons.Add(pResponse.name, pResponse);
            }

            return allPersons;

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
