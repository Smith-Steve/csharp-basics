using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using HelloWorld.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.Common;
using HelloWorld.Data;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();
            DataContextEF entityFramework = new DataContextEF();


            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "ZG90",
                HasWifi = true,
                HasLTE = true,
                ReleaseDate = DateTime.Now,
                Price = 94.87m,
                VideoCard = "RTX260"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            string sql = @$"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                CPUCores,
                VideoCard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price
            ) VALUES (
                '{myComputer.Motherboard}',
                '{myComputer.CPUCores}',
                '{myComputer.VideoCard}',
                '{myComputer.HasWifi}',
                '{myComputer.HasLTE}',
                '{myComputer.ReleaseDate:yyyy-MM-dd}',
                '{myComputer.Price}')";

                bool result = dapper.ExecuteSql(sql);

                string sqlSelect = @"SELECT 
                Computer.Motherboard,
                Computer.CPUCores,
                Computer.VideoCard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Price FROM TutorialAppSchema.Computer";

                IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
                int recordNo = 1;
                foreach (var computer in computers)
                {
                    Console.WriteLine("'" + computer.Motherboard +
                        "','" + computer.HasWifi +
                        "','" + computer.HasLTE +
                        "','" + computer.ReleaseDate +
                        "','" + computer.Price +
                        "','" + computer.VideoCard);
                }

                IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
                if (computersEf != null)
                Console.WriteLine("Computer from Entity Framework");
                foreach(Computer computerEf in computersEf)
                {
                                    {
                    Console.WriteLine("'" + computerEf.Motherboard +
                        "','" + computerEf.HasWifi +
                        "','" + computerEf.HasLTE +
                        "','" + computerEf.ReleaseDate +
                        "','" + computerEf.Price +
                        "','" + computerEf.VideoCard);
                }
                }
            }
    }
}