using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RegressionTestRunner.Models;

namespace RegressionTestRunner.TestRunner
{
    /*
     *          Task task1 = Task.Factory.StartNew(() => testRunner.RunSingleTest(tests["test1"]));
                Task task2 = Task.Factory.StartNew(() => testRunner.RunSingleTest(tests["test2"]));
                Task task3 = Task.Factory.StartNew(() => testRunner.RunSingleTest(tests["test3"]));
                Task.WaitAll(task1, task2, task3);
                Console.WriteLine("All threads complete");
     */
    public static class ThreadInvoker 
    {

        public static void RunMultipleThreads(int threadCount, IRunnable testRunner, Dictionary<String, TestResult> tests )
        {
            Console.WriteLine("About to run" + threadCount + " threads");
            var tasks = new List<Task>();
            try
            {
                for (int i = 0; i < threadCount; i++)
                {
                    int number = i + 1;
                    tasks.Add(Task.Factory.StartNew(() => testRunner.RunSingleTest(tests["test" + number])));
                }
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine("OMGER!!!!!!!!!!!!!!!!!!!!!!! " + e);
            }
            Console.WriteLine("All threads complete");
        }
     
        
    }
}
