using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiFramework
{
    public class JsonHelper
    {
        public string ReadJsonFile(string fileName)
        {
            string assemblyPath;
            assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            string UploadFileName = fileName;

            var fullPathofFile = Path.Combine(assemblyPath, @"" + UploadFileName);
            string inputData = File.ReadAllText(fullPathofFile);
            return inputData;
        }

        public string BuildRequestURL(string baseURL, IDictionary<string, string> jsonInputParams)
        {
            if (jsonInputParams.Count == 0)
            {
                return baseURL;
            }
            else
            {               
                List<string> stringValues = new List<string>();
                foreach (var item in jsonInputParams)
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

