using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Regression.Tests.Fixtures;
using Regression.Tests.Settings;
using RegressionTestRunner.Models;

namespace RegressionTestRunner.TestRunnerModules
{

    public class TestRunner : IRunnable
    {
        public TestRunner() {}
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


        public bool RunSingleTest(TestToRun test)
        {
            return SingleTest(test);
        }

        public bool RunSingleTest(RunningTest test)
        {
            return SingleTest(ConvertRunningTest(test), test.Settings);
        }

        public bool RunAPITest(string testName, string classfile)
        {
            var testClass = new APITestTypes().GetTestClassInstanceFromString(classfile);
            var currentTestsInClass = testClass.GetType().GetMethods().ToList();

            testName = currentTestsInClass.Find(x => x.Name.Contains(testName)).Name;
            testClass.TestSetup();

            var testFailure = false;
            var invokeFailure = false;
            Console.WriteLine("****************************************************************************");
            Console.WriteLine(Environment.NewLine + "Attempting to run test : " + testName);
            try
            {
                try
                {
                    testClass.GetType().InvokeMember(testName, BindingFlags.InvokeMethod, null, testClass, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Test "+ testName + " falied: " );
                    invokeFailure = true;
                }
                finally
                {
                    testClass.TearDown();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Test actually failed ");
                testFailure = true;
            }
            
            return !testFailure && !invokeFailure;
        }

        public List<MethodInfo> GetTestListFromClass(string className)
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
            var list = testClass.GetType().GetMethods().ToList();

            return (from methodInfo in list
                    let attributes = methodInfo.GetCustomAttributes().ToList()
                    where
                        attributes.Select(attribute => attribute.GetType().Name)
                                  .Any(attName => attName.Equals("TestAttribute"))
                    select methodInfo).ToList();
        } 

        private bool SingleTest(TestToRun test, TestSettings settings = null)
        {
            var testClass =  new RegressionTestTypes().GetTestClassInstanceFromString(test.TestClass);
            OutPutTestInfo(test, testClass);
            //Run setup with or without custom settings 
            
            if (settings == null)
                testClass.Setup();
            else 
                testClass.Setup(settings);
            //Generate Session Based which fixture the test use
            GenerateSessionForTestClass(testClass, test.TestType);

            try
            {
                testClass.GetType().InvokeMember(test.TestName, BindingFlags.InvokeMethod, null, testClass, null);
            }
            catch (Exception e)
            {
                LogExceptionAndAddTestFailure(e, test);
                testClass.Session.Quit();
                return false;

            }
            finally
            {
                testClass.TearDown();
            }
            return true;
        }
        
        private void LogExceptionAndAddTestFailure(Exception e, TestToRun test)
        {
            Console.WriteLine("{0} Exception caught.", e);
                Console.WriteLine(Environment.NewLine);
                ListOfFailedTests.Add(new TestFailure
                    {
                        Test = test,
                        exception = e
                    });
        }

        private void GenerateSessionForTestClass(MBTestFixture testFixture, string testType)
        {
            if (testType.Contains("Business"))
            {
                testFixture.GenerateSession("Business");
            }
            else if (testType.Contains("Consumer"))
            {
                testFixture.GenerateSession("Consumer");
                Console.WriteLine("Consumer Mode Login: " + testFixture.Session.UserPrefix + "@mbo.com");
                Console.WriteLine("Consumer Mode Password: client1234");
            }
            else
            {
                testFixture.GenerateSession("None");
            }
        }
        public void PrintFailedTestResult(List<TestFailure> failedTests)
        {
            Console.WriteLine(failedTests);
        }

        private TestToRun ConvertRunningTest(RunningTest m)
        {
            return new TestToRun
                {
                    TestName = m.Name,
                    TestClass = m.Class,
                    TestType = m.Mode
                };
        }
        public void OutPutTestInfo(TestToRun test, object testClass)
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
            var testClass = new RegressionTestTypes().GetTestClassInstanceFromString(className);
            var list = testClass.GetType().GetMethods();
            return list.Any(methodInfo => methodInfo.Name.Equals(test));
        }

        private static bool IsTestClass(string testClass)
        {
            var testClasses = typeof (RegressionTestTypes).GetMethods();
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
