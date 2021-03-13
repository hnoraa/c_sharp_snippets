// https://www.c-sharpcorner.com/UploadFile/f9f215/parallel-programming-part-1-introducing-task-programming-l/
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
        static void Main(string[] args)
        {
            // action delegate
            Task task1 = new Task(new Action<object>(HelloConsole), "Task 1");

            // anonymous function
            Task task2 = new Task(delegate (object obj)
            {
                HelloConsole(obj);
            }, "Task 2");

            // lambda expression
            Task task3 = new Task((obj) => HelloConsole(obj), "Task 3");

            // start tasks
            task1.Start();
            task2.Start();
            task3.Start();

            // short-living task
            Task.Factory.StartNew((obj) =>
            {
                HelloConsole(obj);
            }, "Short-Living Task");

            // get task result
            Task<int> task4 = new Task<int>(() =>
            {
                int result = 1;

                for (int i = 1; i < 10; ++i)
                {
                    result *= i;
                }

                return result;
            });

            // start task
            task4.Start();

            // wait for result from task
            Console.WriteLine($"Task Resut: {task4.Result}");

            Console.WriteLine("Section 1 Complete...");
            Console.ReadKey();

            // cancel a task
            // uses a cancellation token
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            // create the task with the token
            Task cancelTask = new Task(() =>
            {
                for (int i = 0; i < 100000; ++i)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancel() Called...");
                        return;
                    }

                    Console.WriteLine($"Loop value: {i}");
                }
            }, token);

            Console.Write("Press any key to start task.\nPress any key again to cancel the running task");
            Console.ReadKey();

            // start cancellable task
            cancelTask.Start();

            // read from console
            Console.ReadKey();

            // cancelling the task
            Console.WriteLine("Cancelling the task");
            cancellationTokenSource.Cancel();

            Console.WriteLine("Complete...");
            Console.ReadKey();
        }

        static void HelloConsole(object message)
        {
            Console.WriteLine($"Hello: {message}");
        }
    }
}
