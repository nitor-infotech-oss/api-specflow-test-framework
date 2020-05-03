using ApiFramework.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace ApiFramework
{
    public class JsonHelper
    {
        public string ReadJsonFile(Table table)
        {
            string inputParameters = null;
            foreach (var row in table.Rows)
            {
                inputParameters = ReadJsonFile(row["FileName"]);  // reading json file
            }
            return inputParameters;
        }

        public string ReadJsonFile(string fileName)
        {
            string assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPathofFile = Path.Combine(assemblyPath, @"" + fileName).Replace("Environment", ConfigurationManager.AppSettings["Environment"]);
            string inputData = File.ReadAllText(fullPathofFile);
            return inputData;
        }

        public string BuildRequestURL(string baseURL, string jsonInputParams)
        {
            if (jsonInputParams == null)
            {
                return baseURL;
            }
            else
            {
                IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonInputParams);
                List<string> stringValues = new List<string>();
                foreach (var item in jsonInputCSharp)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        stringValues.Add(item.Key + "=" + item.Value);
                    }
                }
                return string.Format("{0}?{1}", baseURL, string.Join("&", stringValues));
            }
        }

        public string GetDataByEnvironment(string parameter)
        {
            string environment = ConfigurationManager.AppSettings["Environment"];
            string environmentData = ReadJsonFile(@"Config\API_Data_Config.json");
            var inputJsonObject = JsonConvert.DeserializeObject<API_Data_Config>(environmentData);

            var envConfig = inputJsonObject.GetDataByEnvironment
                .Where(x => x.environment == environment).Select(x => x.environmentData)
                .ToList();
            foreach (var config in envConfig)
            {                
                foreach (var item in config)
                {
                    if (item.key == parameter)
                        parameter = item.value;
                }
                    
            }
            return parameter;

        }
    }
}

