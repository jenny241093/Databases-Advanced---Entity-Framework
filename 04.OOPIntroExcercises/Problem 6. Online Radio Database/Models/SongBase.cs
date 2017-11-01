using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SongBase
{
    private int songCount;
    private long totalDurationInSeconds;

    public void AddSong(Song song)
    {
        this.songCount++;
        long totalSeconds = song.Seconds + song.Minutes * 60;
        this.totalDurationInSeconds += totalSeconds;
    }

    public SongBase()
    {

    }
    public override string ToString()
    {
        var timeSpan = TimeSpan.FromSeconds(this.totalDurationInSeconds);
        int hr = timeSpan.Hours;
        int mn = timeSpan.Minutes;
        int sec = timeSpan.Seconds;
        
        var time = string.Format("{0}h {1}m {2}s", hr, mn, sec);
        string output = string.Format("Songs added: {0}{1}Playlist length: {2}", this.songCount, Environment.NewLine, time);
        return output;
    }
}

