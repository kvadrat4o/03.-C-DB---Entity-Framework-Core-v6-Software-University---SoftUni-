using System;

public class OnlineRadio
{
    static PlayList playList = new PlayList();

    public static void Main()
    {
        var potentialPlayList = Convert.ToInt32(Console.ReadLine());
        for (int count = Common.Ints.Zero; count < potentialPlayList; count++)
        {
            try
            {
                var currentSong = Console.ReadLine().Split(Common.Chars.InputSpliter);
                Validator.IfInvalidSongData(currentSong);

                var artist = currentSong[Common.Ints.Zero];
                var title = currentSong[Common.Ints.One];
                var songDuration = currentSong[Common.Ints.Two].Split(Common.Chars.DurationSpliter);
                Validator.IfInvalidSongLength(songDuration);

                var minutes = Validator.IfInvalidMinutesFormat(songDuration[Common.Ints.Zero]);
                Validator.IfInvalidSongMinutesException(minutes);

                var seconds = Validator.IfInvalidSecondsFormat(songDuration[Common.Ints.One]);
                Validator.IfInvalidSongSecondException(seconds);

                var duration = new TimeSpan(Common.Ints.Zero, minutes, seconds);
                var newSong = new Song(artist, title, duration);
                playList.Add(newSong);
                Common.Messages.SongAdded();
            }
            catch (Exception ex)
            {
                Common.Messages.Exception(ex.Message);
            }
        }

        var number = playList.Count;
        var hh = playList.TotalDuration.Hours;
        var mm = playList.TotalDuration.Minutes;
        var ss = playList.TotalDuration.Seconds;

        Common.Messages.SongsAdded(number);
        Common.Messages.TotalDuration(hh, mm, ss);
    }
}