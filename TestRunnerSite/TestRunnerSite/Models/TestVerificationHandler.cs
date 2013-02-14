using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RegressionTestRunner.TestRunner;

namespace TestRunnerSite.Models
{
    public static class TestVerificationHandler
    {
        public static bool Verify(string testName, string testClass, string testType, string siteID)
        {
            return VerifyTestInput(testName, testClass, testType, siteID);
        }

        public static bool VerifyTestName(string testName, string testClass)
        {
            return VerifyTestInClass(testName, testClass);
        }

        public static bool VerifyTestClass(string testClass)
        {
            return VerifyClass(testClass);
        }

        public static bool VerifySite(string siteID)
        {
            return VerifySiteID(siteID);
        }

        private static bool VerifyTestInput(string testName, string testClass, string testType, string SiteID)
        {
            if (!VerifyTestType(testType))
            {
                return false;
            }
            if (!VerifyTestClass(testClass))
            {
                return false;
            }
            if (!VerifyTestName(testName, testClass))
            {
                return false;
            }
            return VerifySiteID(SiteID);
        }

        private static bool VerifyTestInClass(string testName, string testClass)
        {
            return new TestRunner().ContainsTestInClass(testName, testClass);
        }

        private static bool VerifyClass(string testClass)
        {
            return new TestRunner().ContainsTestClass(testClass);
        }
        private static bool VerifyTestType(string testType)
        {
            return testType.Equals("ConsumerMode") || testType.Equals("BusinessMode");
        }

        private static bool VerifySiteID(string SiteID)
        {
            //TODO
            return true;
        }
    }
}