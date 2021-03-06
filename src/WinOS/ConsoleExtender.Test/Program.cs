using static System.Console;

namespace ConsoleExtender.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            const string testString =
                "KKkk шшШШ:11:22:33:44:55:66:77:88:99:00";

            ConsoleHelper.SetCurrentFont(
                "Consolas", 
                12);
            WriteLine(testString);
            ReadKey();

            Clear();

            ConsoleHelper.SetCurrentFont(
                "Lucida Console", 
                32);
            WriteLine(testString);
            ReadKey();

            Clear();

            ConsoleHelper.SetCurrentFont(
                "MS Gothic",
                48);
            WriteLine(testString);
            ReadKey();

            Clear();
            ReadKey();
        }
    }
}
