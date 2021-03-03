using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Input;
using ConsoleMVVMWatch.Annotations;
using static System.Console;

namespace ConsoleMVVMWatch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            View view = new View(new ViewModel(new Model()));

            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.DarkGreen;

            view.Show();
        }
    }

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

    internal sealed class ViewModel : INotifyPropertyChanged
    {
        public const string Midnight = "00:00:00";
        private string _time = Midnight;
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }
        public ViewModel(Model model)
            => model.TimeChanged += ModelOnTimeChanged;

        private void ModelOnTimeChanged(DateTime obj)
            => Time = obj.ToLongTimeString();

        [CanBeNull]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        => PropertyChanged?
            .Invoke(
                this, 
                new PropertyChangedEventArgs(propertyName));
    }

    internal class View
    {
        private object DataContext { get; }

        private string _text, _oldText;
        public string Text
        {
            get => _text;
            set
            {
                if (string.Equals(_text, value))
                    return;
                _oldText = _text?.Remove(_text.Length - 1);
                _text = value;
                Update();
            }
        }

        private void Update()
        {
            var buf = _oldText ?? string.Empty;

            while (buf.Length > 0 && !Text.StartsWith(buf))
                buf = buf.Remove(buf.Length - 1);

            if (buf.Length == 0)
            {
                Clear();
                Write(Text);
            }
            else
            {
                Write(new string('\b', Text.Length - buf.Length));
                Write(Text.Remove(0, buf.Length));
            }
        }

        public View(ViewModel dataContext)
        {
            DataContext = dataContext;
            var binding = new Binding("Time");
            SetBinding(nameof(Text), binding);
        }

        private void SetBinding(string propertyName, Binding binding)
        {
            SetPropertyBinding(propertyName, binding);
            if (DataContext is INotifyPropertyChanged notify)
                notify.PropertyChanged += (s, e)
                    => SetPropertyBinding(propertyName, binding);
        }

        private void SetPropertyBinding(string propertyName, Binding binding)
        {
            var sourceProperty = DataContext.GetType()
                .GetProperty(binding.DataContextPropertyName);
            var targetProperty = GetType()
                .GetProperty(propertyName);
            targetProperty?.SetValue(
                this,
                sourceProperty?.GetValue(DataContext));
        }
        public void Show()
        {
            Update();
            ReadKey();
        }
    }

    internal class Binding
    {
        public string DataContextPropertyName { get; }

        public Binding(string dataContextPropertyName)
            => DataContextPropertyName = dataContextPropertyName;
    }
}