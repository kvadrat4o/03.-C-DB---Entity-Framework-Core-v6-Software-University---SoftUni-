namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            string albumName = data[1];
            string pictureTitle = data[2];
            string pictureFilePath = data[3];

            using (PhotoShareContext db = new PhotoShareContext())
            {
                Album album = db.Albums
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }
                Picture picture = new Picture()
                {
                    Album = album,
                    Title = pictureTitle,
                    Path = pictureFilePath
                };

                db.Pictures.Add(picture);
                db.SaveChanges();
            }

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
