using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Regression.Tests.Fixtures;
using RegressionTestRunner.TestRunner;

namespace RegressionTestRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testRunner = new TestRuntime();
            const string testClass = "AppointmentTests";
            try
            {
                testRunner.RunAPITest("CheckScheduleItems", testClass);
            }
            catch (Exception e )
            {
                Console.WriteLine(e.StackTrace);
            }
            //var tests = testRunner.GetTestListFromClass(testClass);
            //foreach (var memberInfo in tests)
            //{
           //     testRunner.RunAPITest(memberInfo.Name, testClass);
           // }
            Console.ReadKey();
        }
    }
}
