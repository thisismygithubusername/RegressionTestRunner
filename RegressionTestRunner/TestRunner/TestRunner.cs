using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Regression.Tests.Settings;
using RegressionTestRunner.Models;

namespace RegressionTestRunner.TestRunner
{
    public class TestRunner : IRunnable
    {
        public List<TestFailure> ListOfFailedTests = new List<TestFailure>();

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public bool RunSingleTest(TestResult test)
        {
            var testClass = TestTypes.GetTestClassInstanceFromString(test.TestClass);
            OutPutTestInfo(test, testClass);
            //testClass.GdoSetup("-40250");
            testClass.GenerateSession("Business");
            //testClass.ExtraSetup();
            try
            {
                testClass.GetType().InvokeMember(test.TestName, BindingFlags.InvokeMethod, null, testClass, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.WriteLine(Environment.NewLine);
                ListOfFailedTests.Add(new TestFailure
                    {
                        Test = test,
                        exception = e
                    });
                testClass.Session.Quit();
                return false;

            }
            finally
            {
                testClass.TearDown();
            }
            return true;
        }

        public void PrintFailedTestResult(List<TestFailure> failedTests)
        {
            Console.WriteLine(failedTests);
        }

        public void OutPutTestInfo(TestResult test, object testClass)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("TestClass is : " + testClass.GetType());
            Console.WriteLine("Test Being Executed: " + System.Environment.NewLine + test);
        }

        public bool ContainsTestInClass(string test, string className)
        {
            return IsTestInClass(test, className);
        }

        public bool ContainsTestClass(string testClass)
        {
            return IsTestClass(testClass);
        }

        public void WriteTestSettings(TestSettings settings)
        {
            WriteToFile(settings);
        }

        private static bool IsTestInClass(string test, string className)
        {
            var testClass = TestTypes.GetTestClassInstanceFromString(className);
            var list = testClass.GetType().GetMethods();
            return list.Any(methodInfo => methodInfo.Name.Equals(test));
        }

        private static bool IsTestClass(string testClass)
        {
            var testClasses = typeof (TestTypes).GetMethods();
            return testClasses.Any(methodInfo => methodInfo.Name.Equals(testClass));
        }

        private static void WriteToFile(TestSettings newSettings)
        {
            var ser = new DataContractJsonSerializer(typeof(TestSettings));
            using (var fs = new FileStream(GetPath(), FileMode.Create))
            {
                ser.WriteObject(fs, newSettings);
                fs.Close();
            }
           
        }

        private static string GetPath()
        {
            return AssemblyDirectory + "\\..\\..\\TestSettings.json";
        }
        
    }
}
