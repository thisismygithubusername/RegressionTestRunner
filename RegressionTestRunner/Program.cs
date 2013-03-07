using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Regression.Tests.Fixtures;
using RegressionTestRunner.Models;
using RegressionTestRunner.TestRunnerModules;

namespace RegressionTestRunner
{
    class Program
    {
        private static void Main(string[] args)
        {
            var testRunner = new TestRunner();
            const string testClass = "AppointmentTests";

            //testRunner.RunAPITest("CheckAddOrUpdateAvailabilities", testClass);
            

            var tests = testRunner.GetTestListFromClass(testClass);
            var faliedTests = new List<MethodInfo>();
            foreach (var methodInfo in tests)
            {
                var didPass = testRunner.RunAPITest(methodInfo.Name, testClass);
                if (didPass == false)
                {
                    faliedTests.Add(methodInfo);
                }
            }
           
            Console.WriteLine("Done Running all tests");
            Console.WriteLine(Environment.NewLine + "Tests that failed are:");
            //Console.WriteLine("             Test                Test File");
            Console.WriteLine("{0}{1,10}", "Test", "Test File");
            foreach (MethodInfo faliedTest in faliedTests)
            {
                Console.WriteLine("{0,4}    {1,-10}", faliedTest.Name, faliedTest.ReflectedType.Name);
                //Console.WriteLine(faliedTest.Name + " in " + faliedTest.ReflectedType.Name);
            }
            Console.ReadKey();
        }
    }
}
