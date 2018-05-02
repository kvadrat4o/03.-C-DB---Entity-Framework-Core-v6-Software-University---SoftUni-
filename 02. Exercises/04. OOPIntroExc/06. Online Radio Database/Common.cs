using System;

public class Common
{
    public class Chars
    {
        public const char InputSpliter = ';';

        public const char DurationSpliter = ':';
    }

    public class Ints
    {
        public const int Zero = 0;

        public const int One = 1;

        public const int Two = 2;

        public const int ValidDataLength = 3;

        public const int ValidArtistLength = 20;

        public const int ValidTitleLength = 30;

        public const int ValidMinutes = 14;

        public const int ValidSeconds = 59;
    }

    public class Messages
    {
        public const string InvalidData = "Invalid song.";

        public const string InvalidDurationFormat = "Invalid song length.";

        public const string InvalidArtist = "Artist name should be between 3 and 20 symbols.";

        public const string InvalidTitle = "Song name should be between 3 and 30 symbols.";

        public const string InvalidMinutes = "Song minutes should be between 0 and 14.";

        public const string InvalidSeconds = "Song seconds should be between 0 and 59.";

        public static void SongAdded()
        {
            Console.WriteLine("Song added.");
        }

        public static void SongsAdded(int count)
        {
            Console.WriteLine($"Songs added: {count}");
        }

        public static void TotalDuration(int hh, int mm, int ss)
        {
            Console.WriteLine($"Playlist length: {hh}h {mm}m {ss}s");
        }

        public static void Exception(string exception)
        {
            Console.WriteLine(exception);
        }
    }
}