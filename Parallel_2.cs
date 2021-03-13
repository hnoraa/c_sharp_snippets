// https://www.c-sharpcorner.com/UploadFile/f9f215/parallel-programming-part-2/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTasks
{
    class Program
    {
        private const int SLEEP = 1000;

        static void Main(string[] args)
        {
            // create 2 simple tasks and use Wait() until the have completed

            // create and start simple task
            Task task = new Task(new Action(Workload));
            task.Start();

            // wait for the task
            Console.WriteLine("Waiting for task to complete...");
            task.Wait();
            Console.WriteLine("Task complete...");

            // create and start another task
            task = new Task(new Action(Workload));
            task.Start();
            Console.WriteLine($"Waiting {(SLEEP * 2) / 1000} seconds for task to complete...");
            task.Wait(SLEEP * 2);
            Console.WriteLine("Task complete...");

            // WaitAll()
            Task t1 = new Task(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    Console.WriteLine($"Task -- Iteration: {i}");
                    Thread.Sleep(SLEEP);
                }

                Console.WriteLine("Task t1 complete...");
            });
            Task t2 = new Task(() =>
            {
                Console.WriteLine("Task t2 complete...");
            });

            // start tasks
            t1.Start();
            t2.Start();

            // wait on both tasks to complete
            Console.WriteLine("Waiting for t1 & t2 to stop...");
            Task.WaitAll(t1, t2);
            Console.WriteLine("Tasks t1 & t2 complete...");

            // waiting for one or many tasks to complete
            Task tx1 = new Task(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    Console.WriteLine($"Task -- Iteration: {i}");
                    Thread.Sleep(SLEEP);
                }

                Console.WriteLine("Task tx1 complete...");
            });
            Task tx2 = new Task(() =>
            {
                Console.WriteLine("Task tx2 complete...");
            });

            // start tasks
            tx1.Start();
            tx2.Start();

            // wait for the first task to complete
            Console.WriteLine("Waiting for tasks tx1 & tx2 to complete...");
            int taskIdx = Task.WaitAny(tx1, tx2);
            Console.WriteLine($"Task Completed - array index: {taskIdx}");

            // exception handling
            Task tt1 = new Task(() =>
            {
                NullReferenceException exception = new NullReferenceException();
                exception.Source = "tt1";
                throw exception;
            });
            Task tt2 = new Task(() => {
                throw new IndexOutOfRangeException();
            });
            Task tt3 = new Task(() =>
            {
                Console.WriteLine("Task tt3");
            });

            // start tasks
            tt1.Start();
            tt2.Start();
            tt3.Start();

            // wait for all to complete in try catch
            try
            {

            }
            catch(AggregateException ex)
            {
                // loop through exceptions
                foreach (Exception inner in ex.InnerExceptions)
                {
                    Console.WriteLine($"Exception: {inner.GetType()} from {inner.Source}");
                }
            }

            Console.WriteLine("Press any key to end");
            Console.ReadKey();
        }

        static void Workload()
        {
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine($"Task -- Iteration: {i}");
                Thread.Sleep(SLEEP);
            }
        }
    }
}
