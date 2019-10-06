using System;
using System.Drawing;

namespace Prims_Find
{
    class Program
    {
        static void Main(string[] args)
        {
            var thing = Graph<Point>.Maze(3, 5, 402);

            Console.ReadKey();
        }
    }
}
