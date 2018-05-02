using System;
using System.Collections.Generic;

public class PlayList
{
    private TimeSpan totalDuration;

    private static ICollection<Song> SongList;

    public PlayList()
    {
        SongList = new List<Song>();
    }

    public TimeSpan TotalDuration
    {
        get
        {
            return this.totalDuration;
        }
    }

    public void Add(Song song)
    {
        SongList.Add(song);
        this.totalDuration += song.Duration;
    }

    public int Count
    {
        get
        {
            return SongList.Count;
        }
    }
}