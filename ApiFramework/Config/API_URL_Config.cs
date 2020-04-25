using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFramework.Config
{
    public class EnvironmentData
    {
        public string key { get; set; }
        public string value { get; set; }

    }
    public class GetUrlsByEnvironment
    {
        public string environment { get; set; }
        public IList<EnvironmentData> environmentData { get; set; }

    }
    public class API_URL_Config
    {
        public IList<GetUrlsByEnvironment> GetUrlsByEnvironment { get; set; }

    }
}
