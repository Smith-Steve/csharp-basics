using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text.Json;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            DataContextDapper dapper = new DataContextDapper(configuration);


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
                //Write to file.
                // File.WriteAllText("log.txt", sql);

                // using StreamWriter openFile = new("log.txt", append: true);
                // openFile.Write("\n" + sql + "\n");
                // openFile.Close();

                string computersJson = File.ReadAllText("Computers.json");
                Console.WriteLine(computersJson);

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

                if (computers != null)
                {
                    foreach (Computer computer in computers)
                    {
                        Console.WriteLine(computer.Motherboard);
                    }
                }
        }
    }
}