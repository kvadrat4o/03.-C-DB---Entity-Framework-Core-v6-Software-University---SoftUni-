using System;

public class Song
{
    private string artist;

    private string title;

    private TimeSpan duration;

    public Song(string artist, string title, TimeSpan duration)
    {
        this.Artist = artist;
        this.Title = title;
        this.Duration = duration;
    }

    public string Artist
    {
        get
        {
            return this.artist;
        }
        private set
        {
            Validator.IfInvalidArtistNameException(value);
            this.artist = value;
        }
    }

    public string Title
    {
        get
        {
            return this.title;
        }
        private set
        {
            Validator.IfInvalidSongNameException(value);
            this.title = value;
        }
    }

    public TimeSpan Duration
    {
        get
        {
            return this.duration;
        }
        private set
        {
            this.duration = value;
        }
    }
}