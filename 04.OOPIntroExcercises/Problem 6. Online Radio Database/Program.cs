using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    public static void Main()
    {
        var inputCount = int.Parse(Console.ReadLine());
        SongBase data = new SongBase();
        for (int i = 0; i < inputCount; i++)
        {

            string[] songParams = Console.ReadLine().Split(';');
            string artistName = songParams[0];
            string songName = songParams[1];
            string duration = songParams[2];
            try
            {
                Song song = new Song(artistName, songName, duration);
                data.AddSong(song);
                Console.WriteLine("Song added.");
            }
            catch (InvalidSongException ice)
            {
                Console.WriteLine(ice.Message);

            }
        }
        Console.WriteLine(data);


    }
}
