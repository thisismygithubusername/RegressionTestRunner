using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionTestRunner.Models
{
    public class TestToRun
    {
        public TestToRun() { }

        public string TestName { get; set; }

        public string TestClass { get; set; }

        public string TestType { get; set; }

        public override string ToString()
        {

            return "Test Name: " + TestName + System.Environment.NewLine +
                   "Test Class: " + TestClass + System.Environment.NewLine +
                   "Test Type: " + TestType + System.Environment.NewLine;
        }
    }
}
