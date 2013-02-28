using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using MbUnit.Framework;
using Regression.Library.Infrastructure;
using Regression.Library.Utilities;
using Regression.Tests.Coverage.BusinessMode;
using Regression.Tests.Fixtures;
using RegressionTestRunner.Models;

namespace RegressionTestRunner.TestRunner
{
    public class TestRuntime : TestRunner
    {
        public TestRuntime() { }

        private const int tesNameColumn = 1;
        private const string SpreadSheetName = "Updated Broken Tests Spreadsheet";

        public void RunBusinessModeTestsFromSpreadsheet()
        {
            Console.WriteLine("List of Tests: ");

            foreach (var testResult in TestInfoFetcher.GetListOfBusinessModeTestResults())
            {
                try

                {
                    RunSingleTest(testResult);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Some shit fucked up with test:" + testResult.TestName + ": {0}", e);
                    Console.WriteLine(Environment.NewLine);      
                }
            }
            PrintFailedTestResult(ListOfFailedTests);
        }

        public List<MemberInfo> GetTestListFromClass(string className)
        {
            object testClass;
            try
            {
                testClass = new APITestTypes().GetTestClassInstanceFromString(className);
            }
            catch (Exception)
            {
                try
                {
                    testClass = new APITestTypes().GetTestClassInstanceFromString(className);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            var list = testClass.GetType().GetMethods();

            return (from methodInfo in list
                    let attr = methodInfo.CustomAttributes.ToList()
                    where attr.Select(x => x.AttributeType.Name.Contains("Test")).Any()
                    select methodInfo).Cast<MemberInfo>().ToList();
        } 
        public void RunFirstThreeTests()
        {
            //var list = TestInfoFetcher.GetListOfBusinessModeTestResults();
            var list = GetSmallListOfTests();
            var dictOfTests = new Dictionary<String, TestToRun>();
            for (int i = 1; i < 4; i++)
            {
                String test = "test" + i;
                Console.WriteLine(test);
                dictOfTests[test] = list[i-1];
            }

            try
            {
                ThreadInvoker.RunMultipleThreads(3, this, dictOfTests);
            }
            catch (Exception e)
            {
                Console.WriteLine("Some shit fucked up with test");
                Console.WriteLine(Environment.NewLine);
            }
        }
        // Just for shit and gigs 
        private List<TestToRun> GetSmallListOfTests()
        {
            var tests = new List<TestToRun>();
            tests.Add(new TestToRun
            {
                TestClass = "SetupTests",
                TestName = "TestRoomNumbersTable",
                TestType = "BusinessMode"
            });
            tests.Add(new TestToRun
            {
                TestClass = "SetupTests",
                TestName = "TestSetupIPListLink",
                TestType = "BusinessMode"
            });
            tests.Add(new TestToRun
            {
                TestClass = "SetupTests",
                TestName = "ToggleOptions",
                TestType = "BusinessMode"
            });

            return tests;
        } 

    }
}
