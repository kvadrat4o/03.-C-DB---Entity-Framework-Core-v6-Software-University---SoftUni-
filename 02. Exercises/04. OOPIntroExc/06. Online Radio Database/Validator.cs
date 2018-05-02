using System;

public class Validator
{
    public static void IfInvalidSongData(string[] songData)
    {
        if (songData.Length != Common.Ints.ValidDataLength)
        {
            throw new Exception(Common.Messages.InvalidData);
        }
    }

    public static void IfInvalidSongLength(string[] songLength)
    {
        if (songLength.Length != Common.Ints.Two)
        {
            throw new Exception(Common.Messages.InvalidDurationFormat);
        }
    }

    public static int IfInvalidMinutesFormat(string minutes)
    {
        var parsedMinutes = Common.Ints.Zero;
        if (int.TryParse(minutes, out parsedMinutes))
        {
            return parsedMinutes;
        }
        else throw new Exception(Common.Messages.InvalidDurationFormat);
    }

    public static int IfInvalidSecondsFormat(string minutes)
    {
        var parsedSeconds = Common.Ints.Zero;
        if (int.TryParse(minutes, out parsedSeconds))
        {
            return parsedSeconds;
        }
        else throw new Exception(Common.Messages.InvalidDurationFormat);
    }

    public static void IfInvalidArtistNameException(string artist)
    {
        if (artist.Length < Common.Ints.ValidDataLength || artist.Length > Common.Ints.ValidArtistLength)
        {
            throw new Exception(Common.Messages.InvalidArtist);
        }
    }

    public static void IfInvalidSongNameException(string title)
    {
        if (title.Length < Common.Ints.ValidDataLength || title.Length > Common.Ints.ValidTitleLength)
        {
            throw new Exception(Common.Messages.InvalidTitle);
        }
    }

    public static void IfInvalidSongMinutesException(int minutes)
    {
        if (minutes < Common.Ints.Zero || minutes > Common.Ints.ValidMinutes)
        {
            throw new Exception(Common.Messages.InvalidMinutes);
        }
    }

    public static void IfInvalidSongSecondException(int seconds)
    {
        if (seconds < Common.Ints.Zero || seconds > Common.Ints.ValidSeconds)
        {
            throw new Exception(Common.Messages.InvalidSeconds);
        }
    }
}