using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection.Model.Detect
{
    class HeadPose
    {
        public decimal roll { get; set; }
        public int yaw { get; set; }
        public int pitch { get; set; }
    }
}
