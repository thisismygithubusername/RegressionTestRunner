using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionTestRunner.Models
{
    public interface IRunnable
    {
        bool RunSingleTest(TestResult test);
    }
}
