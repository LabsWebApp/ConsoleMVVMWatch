using System;
using System.Timers;
using static System.Console;

namespace ConsoleMVVMWatch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            View view = new View(new ViewModel(new Model()));
            view.Show();
        }
    }

    internal class Model
    {
        private Timer _timer;
        public event Action<DateTime> TimeChanged;

        public Model()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
            => TimeChanged?.Invoke(e.SignalTime);
    }

    internal class View
    {
        public View(ViewModel viewModel)
        {

        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }

    internal class ViewModel
    {
        public string Time { get; set; }
        public ViewModel(Model model)
        {
            model.TimeChanged += ModelOnTimeChanged;
        }

        private void ModelOnTimeChanged(DateTime obj)
        {
            Time = obj.ToShortTimeString();
        }
    }
}