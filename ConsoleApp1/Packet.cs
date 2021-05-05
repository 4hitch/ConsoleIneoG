using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public sealed class Packet
    {
        private string[] Packets;
        private List<NugetPacket> Nugets;
        public Packet(string[] packets)
        {
            Packets = packets;
            Compare();
        }

        private void Compare()
        {
            Nugets = new List<NugetPacket>();
            CutEnds();
            for(int i=0;i<Packets.Length;i++)
            {
                var r = Search(Packets[i]);
                if (r != null)
                    Nugets.Add(r);
            }    
        }

        private void CutEnds()
        {
            for(int i=0;i<Packets.Length;i++)
                Packets[i] = Packets[i].Replace(".nupkg", "");
        }

        private NugetPacket Search(string packet)
        {
            string full = packet;
            int firstDot = packet.IndexOf(".");
            if (firstDot < 0)
                return null;
            var name = packet.Substring(0, firstDot);
            packet = packet.Substring(++firstDot);
            int next;
            double version = 0;
            double actual = 1;
            while ( (next = packet.IndexOf(".")) >=  0)
            {
                var v = packet.Substring(0, next);
                int r;
                if(Int32.TryParse(v, out r))
                {
                    version += r * actual;
                }
                actual /= 100.0;
                packet = packet.Substring(++next);
            }
            if(!string.IsNullOrWhiteSpace(packet))
            {
                int r;
                if (Int32.TryParse(packet, out r))
                    version += r * actual;
            }
            return new NugetPacket() { Packet = name, Version = version, FullName = full + ".nupkg" };

            }

        public string GetPacket(string name)
        {
            var f = Nugets.OrderByDescending(x=> x.Version).FirstOrDefault(x => x.Packet.Equals(name));
            return f != null ? f.FullName : String.Empty;
        }

        public List<string> GetDistinctName()
        {
            return Nugets.Select(x => x.Packet).Distinct().ToList();
        }


        private class NugetPacket
        {
            public string FullName;
            public string Packet;
            public double Version;
        }
    }
}
