using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection.Model.Detect
{
    class FaceAttributes
    {
        public int age { get; set; }
        public string gender { get; set; }
        public decimal smile { get; set; }
        public FacialHair facialHair { get; set; }
        public string glasses { get; set; }
        public HeadPose headPose { get; set; }
    }
}
