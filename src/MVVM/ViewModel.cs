using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ConsoleMVVMWatch.Annotations;

namespace ConsoleMVVMWatch.MVVM
{
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
}