using System;
using System.Threading;

namespace _1_Multithread_C_
{
    class ThreadMonitor
    {
        private readonly int[] _durations;
        private readonly IncrementThread[] _threads;

        public ThreadMonitor(int[] durations, IncrementThread[] threads)
        {
            _durations = durations;
            _threads = threads;
        }

        public void Run()
        {
            var start = DateTime.Now;
            var completed = new bool[_threads.Length];
            int done = 0;

            while (done < _threads.Length)
            {
                var elapsed = (DateTime.Now - start).TotalSeconds;

                for (int i = 0; i < _threads.Length; i++)
                {
                    if (!completed[i] && elapsed >= _durations[i])
                    {
                        _threads[i].Stop();
                        completed[i] = true;
                        done++;
                    }
                }

                Thread.Sleep(200);
            }
        }
    }
}