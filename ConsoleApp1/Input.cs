using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Input
    {
        private Packet Packets;
        public Input(string address)
        {
            var f = LoadFile(address);
            Packets = new Packet(f);

        }

        private string[] LoadFile(string address)
        {
            return new FileReader(address).ReadFile();
        }

        public void OutputNewVersion()
        {
            var l = Packets.GetDistinctName();
            foreach(var el in l)
            {
                var r = Packets.GetPacket(el);
                if (!string.IsNullOrWhiteSpace(r))
                    Console.WriteLine(r);
            }
        }
    }
}
