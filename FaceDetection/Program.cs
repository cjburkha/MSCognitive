using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceDetection.Services;
namespace FaceDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            FaceAPI f = new FaceAPI();
            Bitmap bitmap = new Bitmap(@"c:\temp\images\reference2.jpg");
            f.Detect(bitmap);
            Console.ReadLine();
        }
    }
}
