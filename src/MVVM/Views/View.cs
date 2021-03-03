using System;
using System.ComponentModel;
using ConsoleMVVMWatch.Bindings;

namespace ConsoleMVVMWatch.MVVM.Views
{
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
                Console.Clear();
                Console.Write(Text);
            }
            else
            {
                Console.Write(new string('\b', Text.Length - buf.Length));
                Console.Write(Text.Remove(0, buf.Length));
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
            Console.ReadKey();
        }
    }
}