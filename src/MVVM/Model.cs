using System;
using System.Timers;

namespace ConsoleMVVMWatch.MVVM
{
    internal class Model
    {
        public event Action<DateTime> TimeChanged;

        public Model()
        {
            var timer = new Timer(1000);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
            => TimeChanged?.Invoke(e.SignalTime);
    }
}