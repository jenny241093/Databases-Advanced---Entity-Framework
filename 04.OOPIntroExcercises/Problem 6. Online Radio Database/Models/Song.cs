using System;


public class Song
{
    private string artistName;
    private string songName;
    private int minutes;
    private int seconds;
    private string duration;

    public Song(string artistName, string songName, string duration)
    {
        this.ArtistName = artistName;
        this.SongName = songName;
        this.Minutes = minutes;
        this.Seconds = seconds;
        this.Duration = duration;

    }

    public string ArtistName
    {
        get { return this.artistName; }
    private    set
        {
            if (value == null || value.Length < 3 || value.Length > 20)
            {
                throw new InvalidArtistNameException();
            }
           this. artistName = value;
        }
    }

    public string SongName
    {
        get { return this.songName; }
      private  set
        {
            if (value == null || value.Length < 3 || value.Length > 30)
            {
                throw new InvalidSongNameException();
            }
            this.songName = value;
        }
    }

    public int Minutes
    {
        get { return this.minutes; }
     private   set
        {
            if (value <0 || value>14)
            {
                throw new InvalidSongMinutesException();
            }
            this.minutes = value;
        }
    }

    public int Seconds
    {
        get { return this.seconds; }
       private set
        {
            if (value < 0 || value > 59)
            {
                throw new InvalidSongSecondsException();
            }
            this.seconds = value;
        }
    }

    private string Duration
    {

        set
        {
            string[] timeParams = value.Split(':');
            int minutes;
            int seconds;
            try
            {
                minutes = int.Parse(timeParams[0]);
                seconds = int.Parse(timeParams[1]);
            }
            catch (Exception )
            {
              throw new InvalidSongLengthException();
            }
            this.Minutes = minutes;
            this.Seconds = seconds;
        }
    }
}

