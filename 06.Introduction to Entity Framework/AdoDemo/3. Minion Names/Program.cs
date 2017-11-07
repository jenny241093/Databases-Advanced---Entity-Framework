using System;
using System.Data;
using System.Data.SqlClient;


public class Program
{
    static void Main(string[] args)
    {
        var connection = new SqlConnection(@"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");
        var villainId = int.Parse(Console.ReadLine());
        connection.Open();
        using (connection)
        {
            var villainQuery = @"SELECT [Name] FROM Villains WHERE Id=@villainId";
            var villainCommand = new SqlCommand(villainQuery,connection);
            villainCommand.Parameters.AddWithValue("@villainId",villainId);
            var reader=villainCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Villain: {reader[0]}");
            }
  reader.Dispose();
            string minionsQuery = @"SELECT Name,Age FROM Minions AS m
            JOIN MinionsVillains AS MV ON mv.MinionId = m.Id
            WHERE mv.VillainId = @villainId";
            var minionsCommand=new SqlCommand(minionsQuery,connection);
            minionsCommand.Parameters.AddWithValue("@villainId", villainId);
            reader = minionsCommand.ExecuteReader();
            int counter = 1;
            while (reader.Read())
            {
                Console.WriteLine($"{counter} {reader[0]}-{reader[1]}");
                counter++;
            }
        }
    }
}
