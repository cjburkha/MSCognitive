using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetection.Model.People
{
    class PersonRequest
    {
        public String name { get; set; }
        public String description { get; set; }

        public PersonRequest(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
