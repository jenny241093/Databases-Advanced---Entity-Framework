using System;
using System.Data.SqlClient;

    public class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");
        connection.Open();
            using (connection)
            {
                var str = @"SELECT v.Name,Count(*)FROM Villains as v
                JOIN MinionsVillains  mv on mv.VillainId = v.Id
                GROUP BY v.Name
                    HAVING COUNT(*) > 3
                ORDER BY COUNT(*)DESC";
                var command=new SqlCommand(str,connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }
            }
        }
    }
