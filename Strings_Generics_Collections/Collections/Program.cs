using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new LinkedList<string>() {"Hello", "Cruel", "World"};
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"count = {list.Count}");
            Console.WriteLine("-- remove two items");

            list.Remove("Cruel");
            list.Remove("World");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"count = {list.Count}");
        }
    }
}
