using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceDetection.Model.Detect;
using FaceDetection.Services;
using FaceDetection.Model.People;
namespace FaceDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            FaceAPI f = new FaceAPI();
            DetectResponse detectResponse;
            bool groupAddResult;
            Bitmap bitmap = new Bitmap(@"c:\temp\images\reference_drew.jpg");
            PersonRequest[] bothNames =
            {
                new PersonRequest("Andrew", "Base Andrew"),
                new PersonRequest("Addison", "Base Addy")
            };


            String groupName = "kids";
            //get group, if not there add
            //create group
            var res = f.DeletePerson(groupName, "5f38e186-ce87-4a39-9aa1-fc559bc8d33e");
            groupAddResult = f.CreatePersonGroup(groupName, "Andrew and Addison");
            Dictionary<String, PersonResponse> allPersons = f.GetPersons(groupName);

            //create people if they are not there
            foreach (var personRequest in bothNames)
            {
                if (!allPersons.ContainsKey(personRequest.name))
                    f.CreatePerson(groupName, personRequest.name, personRequest.description);
            }
            
            //
            //Next is add faces for each person
            //https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f3039523b
        //https://westus.api.cognitive.microsoft.com/face/v1.0/persongroups/{personGroupId}/persons/{personId}/persistedFaces[?userData][&targetFace]
            detectResponse = f.Detect(bitmap);
            
            Console.ReadLine();
        }
    }
}
