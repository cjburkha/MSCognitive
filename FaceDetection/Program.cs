using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceDetection.Model.Detect;
using FaceDetection.Services;
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
            groupAddResult = f.CreatePersonGroup("family");
            detectResponse = f.Detect(bitmap);
            
            Console.ReadLine();
        }
    }
}
