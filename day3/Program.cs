using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delimiterChars = { ' ', ',', 'x', ':' };

            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            Dictionary<(int, int), (int, int)> claimedSpaces = new Dictionary<(int, int), (int, int)>();
            Dictionary<int, bool> rooms = new Dictionary<int, bool>();

            foreach (var l in lines)
            {
                var d = l.Split(delimiterChars);
                var x = int.Parse(d[2]);
                var y = int.Parse(d[3]);
                var xSize = int.Parse(d[5]);
                var ySize = int.Parse(d[6]);
                var nbr = int.Parse(d[0].Substring(1));

                rooms[nbr] = true;

                for (var xi = x; xi < x + xSize; xi++)
                {
                    for (var yi = y; yi < y + ySize; yi++)
                    {
                        if (claimedSpaces.ContainsKey((xi, yi)))
                        {
                            claimedSpaces[(xi, yi)] = (claimedSpaces[(xi, yi)].Item1 + 1, claimedSpaces[(xi, yi)].Item2);
                            rooms[claimedSpaces[(xi, yi)].Item2] = false;
                            rooms[nbr] = false;
                        }
                        else
                        {
                            claimedSpaces[(xi, yi)] = (1, nbr);
                        }
                    }
                }
            }

            Console.WriteLine(claimedSpaces.Where(o => o.Value.Item1 > 1).Count());
            Console.WriteLine(rooms.Where(o => o.Value == true).First().Key);

            Console.ReadKey();
        }
    }
}
