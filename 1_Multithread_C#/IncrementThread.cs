using System;
using System.Threading;

namespace _1_Multithread_C_
{
    class IncrementThread
    {
        private readonly int _id;
        private readonly int _increment;
        private readonly int _duration;
        private readonly CountdownEvent _startSignal;
        private volatile bool _stopped;
        private DateTime _startTime;

        public IncrementThread(int id, int increment, int duration, CountdownEvent startSignal)
        {
            _id = id;
            _increment = increment;
            _duration = duration;
            _startSignal = startSignal;
        }

        public void Stop() => _stopped = true;

        public void Run()
        {
            _startSignal.Wait();
            _startTime = DateTime.Now;

            long sum = 0;
            long count = 0;

            while (!_stopped)
            {
                sum += _increment;
                count++;
            }

            Console.WriteLine($"Потік {_id}: крок={_increment}, план={_duration}с, сума={sum}, операцій={count}");
        }
    }
}