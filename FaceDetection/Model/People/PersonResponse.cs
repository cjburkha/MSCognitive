using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection.Model.People
{
    class PersonResponse
    {
        public Guid PersonID { get; set; }
        public String name { get; set; }
        public String UserData { get; set; }
        public Guid[] FaceIDs { get; set; }

    }
    /*
     *  {
        "personId":"25985303-c537-4467-b41d-bdb45cd95ca1",
        "name":"Ryan",
        "userData":"User-provided data attached to the person",
        "persistedFaceIds":[
          "015839fb-fbd9-4f79-ace9-7675fc2f1dd9",
          "fce92aed-d578-4d2e-8114-068f8af4492e",
          "b64d5e15-8257-4af2-b20a-5a750f8940e7"
        ]
    },*/
}
