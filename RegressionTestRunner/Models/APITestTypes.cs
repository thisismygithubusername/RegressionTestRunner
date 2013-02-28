using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APITest.Library.APITests;
using APITest.Library.APITests.Tests;

namespace RegressionTestRunner.Models
{
    public class APITestTypes {

        public static AppointmentTests AppointmentTests()
        {
            return new AppointmentTests();
        }

        public static ClassTests ClassTests()
        {
            return new ClassTests();
        }

        public static ClientTests ClientTests()
        {
            return new ClientTests();
        }

        public static DataTests DataTests()
        {
            return new DataTests();
        }

        public static SaleTests SaleTests()
        {
            return new SaleTests();
        }

        public APITestSuite GetTestClassInstanceFromString(string testClass) 
        {
            MethodInfo info = GetType().GetMethod(testClass);
            var testfile = info.Invoke(null, null);
            return (APITestSuite) testfile;
        }
    }




}
