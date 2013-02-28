using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Regression.Library.Infrastructure;
using Regression.Library.Utilities;
using Regression.Tests.Coverage.BusinessMode;
using Regression.Tests.Fixtures;
using mb.Web.Tests.Automation.APITests.AvailableMethods;

namespace RegressionTestRunner.Models
{
    public class RegressionTestTypes : TestClassReflector
    {

        public static AppointmentsTests AppointmentsTests()
        {
            return /*GetTypeOf(*/new AppointmentsTests
            {
                Session = new ThreadLocal<Session>().Value,
                TestHook = new ThreadLocal<TestHook>().Value,
            };
        }

        public static AutoEmailsTests AutoEmailsTests()
        {
            return /*GetTypeOf(*/new AutoEmailsTests();
        }
        public static ClassesTests ClassesTests()
        {
            return /*GetTypeOf(*/new ClassesTests();
        }
        public static ClientsTests ClientsTests()
        {
            return /*GetTypeOf(*/new ClientsTests();
        }
        public static DataMaintenanceTests DataMaintenanceTests()
        {
            return /*GetTypeOf(*/new DataMaintenanceTests();
        }
        public static LanguageSettingsOptionsTests LanguageSettingsOptionsTests()
        {
            return /*GetTypeOf(*/new LanguageSettingsOptionsTests();
        }
        public static ProductManagementTests ProductManagementTests()
        {
            return /*GetTypeOf(*/new ProductManagementTests();
        }
        public static ReportsTests ReportsTests()
        {
            return /*GetTypeOf(*/new ReportsTests();
        }
        public static RetailTests RetailTests()
        {
            return /*GetTypeOf(*/new RetailTests();
        }
        public static SetupTests SetupTests()
        {
            return /*GetTypeOf(*/new SetupTests();
        }
        public static StaffTesting StaffTesting()
        {
            return /*GetTypeOf(*/new StaffTesting();
        }
        public static WorkshopsPageTests WorkshopsPageTests()
        {
            return /*GetTypeOf(*/new WorkshopsPageTests();
        }

    }
}
