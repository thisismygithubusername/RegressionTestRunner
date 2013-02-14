using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegressionTestRunner.TestRunner;

namespace RegressionTestRunner.Models
{
    public static class TestInfoFetcher
    {
        /*
        * Returns a list of TestResult Objects
        */
        public static List<TestResult> GetListOfBusinessModeTestResults()
        {
            var listOfTestResults = new List<TestResult>();
            var listofTests = GetListOfTests();

            foreach (string test in listofTests)
            {
                if (test.Contains("BusinessMode"))
                {
                    listOfTestResults.Add(ParseTestIntoTestResult(test));
                }
            }
            return listOfTestResults;
        }

        /*
         *  Accepts a string in this format
         * "Coverage.TestType.TestClass.TestName"
         * "Coverage.BusinessMode.WorkshopsPageTests.ScheduleWorkshopChangeCostume"
         *  Turn the string into a TestResult object and returns it
         */
        private static TestResult ParseTestIntoTestResult(String test)
        {
            const char delimeter = '.';
            string[] testAtrributes = test.Split(delimeter);
            var testResult = new TestResult
            {
                TestClass = testAtrributes[2],
                TestName = testAtrributes[3],
                TestType = testAtrributes[1]
            };
            return testResult;
        }

        private static IEnumerable<string> GetListOfTests()
        {
            var allRowsInDoc = GSpreadsheetDataFetcher.ImportTestColumnFromGoogleDoc();
            // Iterate through each row, printing its cell values.
            return allRowsInDoc.Where(row => row.Contains("Coverage")).ToList();
        }

    }
}
