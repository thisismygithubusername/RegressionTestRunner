﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionTestRunner.Models
{
    public class TestFailure
    {
        public TestToRun Test { get; set; }
        public Exception exception { get; set; }

    }
}
