using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Regression.Tests.Settings;
using RegressionTestRunner.Models;
using TestRunnerSite.Models;
using RegressionTestRunner.TestRunner;
using TestRunnerSite.Models;

namespace TestRunnerSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to my awesome test Runner";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Test Runner 2.0 beta";

            return View();
        }

        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Testing(string testName, string testClass, string testType, string SiteID)
        {
            if (!TestVerificationHandler.Verify(testName, testClass, testType, SiteID))
            {
                return View("InvalidTest");
            }
            else
            {
                var testRunner = new TestRunner();
                var testSettings = TestSettings.ParseSettings();
                testSettings.SiteID = SiteID;
                //testRunner.WriteTestSettings(testSettings);
                string settings = "SiteID: " + testSettings.SiteID +
                                  ", Domain: " + testSettings.Domain;
                var model = new TestingModels.RunningTestModel
                    {
                        Name = testName,
                        Class = testClass,
                        Mode = testType,
                        SiteID = SiteID,
                        Settings = settings
                    };
                return View(model);
            }
        }
    }
}
