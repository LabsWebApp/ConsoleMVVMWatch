using System;
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
        public ViewModel(Model model)
        {

        }
    }
}