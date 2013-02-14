using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regression.Tests.Fixtures;
using RegressionTestRunner.TestRunner;

namespace RegressionTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var testRunner = new TestRuntime();
            testRunner.RunFirstThreeTests();
        }
    }
}
