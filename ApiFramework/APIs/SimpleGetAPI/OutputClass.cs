using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework.APIs.SimpleGET
{
    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }

    }
    public class Ad
    {
        public string company { get; set; }
        public string url { get; set; }
        public string text { get; set; }

    }
    public class OutputClass
    {
        public Data data { get; set; }
        public Ad ad { get; set; }

    }
}
