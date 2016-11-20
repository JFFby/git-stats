using System;
using Git.Stats.Infrastructure;

namespace Git.Stats.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = GitStatsFactory.ResolveHandler();
            var nl = Environment.NewLine;

            do
            {
                System.Console.Write("Enter command: ");
                var command = System.Console.ReadLine();
                System.Console.WriteLine($"{nl}Command accepted, it can takes some time...{nl}");
                var result = handler.Execute(command);
                System.Console.WriteLine(result);
                System.Console.ReadKey();

            } while (true);
        }
    }
}