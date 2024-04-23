using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using HelloWorld.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            IDbConnection dbConnection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

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

string sqlInsert = @$"INSERT INTO TutorialAppSchema.Computer (
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
        int result = dbConnection.Execute(sqlInsert);}
    
    string sqlSelect = @"SELECT 	Motherboard,
	CPUCores,
	VideoCard,
	HasWifi,
	HasLTE,
	ReleaseDate,
	Price FROM TutorialAppSchema.Computer";
    }
}