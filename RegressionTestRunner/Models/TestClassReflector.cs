using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Regression.Tests.Fixtures;

namespace RegressionTestRunner.Models
{
    public abstract class TestClassReflector
    {
        public LoggedInTestFixture GetTestClassInstanceFromString(string testClass)
        {
            Type type = typeof(RegressionTestTypes);
            MethodInfo info = type.GetMethod(testClass);
            var testfile = info.Invoke(null, null);
            return (LoggedInTestFixture)testfile;
        }
    }
}
