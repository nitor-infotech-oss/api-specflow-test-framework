using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            IDictionary<string, string> jsonInputCSharp = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonInputParams);

            if (jsonInputCSharp.Count == 0)
            {
                return baseURL;
            }
            else
            {
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
    }
}

