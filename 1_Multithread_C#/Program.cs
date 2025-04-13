using System;
using System.Threading;

namespace _1_Multithread_C_
{
    class Program
    {
        static void Main()
        {
            const int threadCount = 4;
            const int minExecutionTime = 2;
            const int maxExecutionTime = 10;
            const int minIncrementValue = 1;
            const int maxIncrementValue = 10;

            var rand = new Random();
            var executionTimes = new int[threadCount];
            var incrementValues = new int[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                executionTimes[i] = rand.Next(minExecutionTime, maxExecutionTime + 1);
                incrementValues[i] = rand.Next(minIncrementValue, maxIncrementValue + 1);
            }

            Console.WriteLine("Параметри потоків:");
            for (int i = 0; i < threadCount; i++)
            {
                Console.WriteLine($"Потік {i}: крок = {incrementValues[i]}, час = {executionTimes[i]} сек");
            }

            var startSignal = new CountdownEvent(1);
            var threads = new IncrementThread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new IncrementThread(i, incrementValues[i], executionTimes[i], startSignal);
                new Thread(threads[i].Run).Start();
            }

            var monitor = new ThreadMonitor(executionTimes, threads);
            new Thread(monitor.Run).Start();

            Console.WriteLine("\n>>> Початок виконання!");
            startSignal.Signal();
        }
    }
}