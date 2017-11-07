using System;

using System.Data;
using System.Data.SqlClient;
using System.IO;

public class Program
{
    static void Main(string[] args)
    {
        var connection = new SqlConnection(@"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");
        var minionArgs = Console.ReadLine().Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var villainArgs = Console.ReadLine().Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var minionName = minionArgs[1];
        var minionAge = int.Parse(minionArgs[2]);
        var townName = minionArgs[3];
        connection.Open();
        using (connection)
        {

            string minionsQuery = @"INSERT INTO Minions
                                    VALUES(@minionName,@minionAge)";
            var minionsCommand = new SqlCommand(minionsQuery, connection);
            minionsCommand.Parameters.AddWithValue("@minionName", minionName);
            minionsCommand.Parameters.AddWithValue("@minionAge", minionAge);
            minionsCommand.ExecuteNonQuery();
            var townQuery = @"INSERT INTO Towns
                            VALUES(@TownName)";
            var townCommand = new SqlCommand(townQuery, connection);
            townCommand.Parameters.AddWithValue("@townName", townName);
            townCommand.ExecuteNonQuery();
        }

    }

    private static bool CheckIfTownExists(string townName, SqlConnection dbcon)
    {
        string query = "SELECT COUNT(*) FROM Towns WHERE TownName=@townName";

        SqlCommand cmd = new SqlCommand(query, dbcon);
        cmd.Parameters.AddWithValue("@townName", townName);
        if ((int)cmd.ExecuteScalar() == 0)
        {
            return false;
        }

        return true;
    }
}
