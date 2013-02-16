using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Regression.Tests.Settings;
using RegressionTestRunner.Models;
using RegressionTestRunner.TestRunner;

namespace TestRunnerSite.Models
{
    public class TestingModels
    {

        public class RunningTestModel : RunningTest 
        {
            public String Name;
            public String Class;
            public String Mode;
            public String SiteID;
            public String Settings;
        }

        public class FailedTestModel : RunningTestModel
        {
            public String Failure;
        }
        
    }
}