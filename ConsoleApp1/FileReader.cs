using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    internal sealed class FileReader
    {
        private readonly string Address;
        public FileReader(string address)
        {
            Address = address;
        }

        public string[] ReadFile()
        {
            if (!File.Exists(Address))
                return new string[0];
            return File.ReadAllLines(Address);
        }

    }
}
