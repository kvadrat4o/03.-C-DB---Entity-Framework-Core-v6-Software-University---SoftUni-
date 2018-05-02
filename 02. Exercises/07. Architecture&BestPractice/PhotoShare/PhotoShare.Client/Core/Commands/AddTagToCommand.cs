namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            using (PhotoShareContext db = new PhotoShareContext())
            {
                var albumName = data[1];
                var tagName = data[2];
                if (!db.Albums.Any(a => a.Name == albumName) || !db.Tags.Any(t => t.Name == tagName))
                {
                    throw new ArgumentException($"Either tag or album do not exist!");
                }
                Album album = db.Albums
                    .Include(a => a.AlbumTags)
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .FirstOrDefault(a => a.Name == albumName);
                Tag tag = db.Tags
                    .FirstOrDefault(t => t.Name == tagName);
                AlbumTag albumTag = new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                };
                db.AlbumTags.Add(albumTag);
                db.SaveChanges();
                return $"Tag {tag} added to {album}!";
            }
            throw new NotImplementedException();
        }
    }
}
