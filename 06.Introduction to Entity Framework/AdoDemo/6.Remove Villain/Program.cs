using System;
using System.Data.SqlClient;


public class Program
{
    static void Main(string[] args)
    {
        var connection = new SqlConnection(@"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");
        connection.Open();
        var villainId = int.Parse(Console.ReadLine());
        using (connection)
        {
            try
            {
                var mvQuery = @"DELETE FROM MinionsVillains WHERE VillainId=@villainId";
                var mvCommand = new SqlCommand(mvQuery, connection);
                mvCommand.Parameters.AddWithValue("@villainId", villainId);
                int minionsReleased = mvCommand.ExecuteNonQuery();

                string nameQuery = @"SELECT [Name] FROM Villains WHERE Id=@villainId";
                var nameCommand = new SqlCommand(nameQuery, connection);
                nameCommand.Parameters.AddWithValue("@villainId", villainId);
                var reader = nameCommand.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Dispose();
                    throw new ArgumentException("No such villain was found.");
                }
                

                string villainName = Convert.ToString(reader[0]);
            
                
                
                string villainQuery = @"DELETE FROM Villains WHERE Id=@villainId";
                var villainCommand = new SqlCommand(villainQuery, connection);
                villainCommand.Parameters.AddWithValue("@villainId", villainId);
                villainCommand.ExecuteNonQuery();
                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{minionsReleased} minions were released.");



            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }
    }
}
