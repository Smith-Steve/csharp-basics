using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using HelloWorld.Models;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Computer myComputer = new Computer()
            {
                Motherboard = "ZG90",
                HasWifi = true,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 94.87m,
                VideoCard = "RTX260"
            };

            Console.Write(myComputer.Motherboard);

        }
    }
}