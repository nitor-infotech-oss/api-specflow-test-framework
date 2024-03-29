﻿using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ApiFramework
{
    [Binding]
    public sealed class Hooks
    {
        public static ExtentReports extent;
        public static ExtentTest test;
  

        [BeforeFeature()] 
        //[BeforeTestRun]
        //[BeforeScenario()]
        public static void BasicSetUp()
        {
            string currentDate = DateTime.Now.ToString("dddd, dd MMMM yyyy ");
            string currentTime = DateTime.Now.ToShortTimeString().ToString().Replace(":", ".");
            string currentDateTime = currentDate + currentTime;

            string pth = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\TestResultReport_" + currentDateTime + ".html"; // using date time for generating new report each time

            extent = new ExtentReports();
            var extentHtml = new AventStack.ExtentReports.Reporter.ExtentHtmlReporter(reportPath);
            extent.AttachReporter(extentHtml);
            extentHtml.LoadConfig(projectPath + "extent-config.xml");

            extent.AddSystemInfo("Environment", ConfigurationManager.AppSettings["Environment"]);

        }


        [BeforeScenario()]
        public static void BeforeScenarioSetUp()
        {
            // string testName = TestContext.CurrentContext.Test.Name;
            string testName = ScenarioContext.Current.ScenarioInfo.Title;
          //  testName = FeatureContext.Current.FeatureInfo.Title;
            test = extent.CreateTest(testName);
        }

        //[AfterScenario()]
        //public static void AfterScnario()
        //{
            
        //}

        [AfterFeature()]
        //[AfterTestRun]
        //[AfterScenario()]
        public static void EndReport()
        {          
            extent.Flush();

            string workingDirectory = Environment.CurrentDirectory;
            System.IO.DirectoryInfo di = new DirectoryInfo(workingDirectory);

            if (di.GetFiles().Count() > Convert.ToInt32(ConfigurationManager.AppSettings["FileCount"]))
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        //Existing log will not get deleted as its been used by other process. 
                    }
                }
                //foreach (DirectoryInfo dir in di.GetDirectories()) // not needed as there will be no directory in this folder.
                //{
                //    dir.Delete(true);
                //}
            }
        }


    }
}
