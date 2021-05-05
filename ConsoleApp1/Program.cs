using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = new Input("D:\\test.txt");
            i.OutputNewVersion();
            Console.ReadKey();
        }
    }
}
