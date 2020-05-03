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
    public class GetDataByEnvironment
    {
        public string environment { get; set; }
        public IList<EnvironmentData> environmentData { get; set; }

    }
    public class API_Data_Config
    {
        public IList<GetDataByEnvironment> GetDataByEnvironment { get; set; }

    }
}
