using System;
using ConsoleMVVMWatch.MVVM;
using ConsoleMVVMWatch.MVVM.Views;
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
}