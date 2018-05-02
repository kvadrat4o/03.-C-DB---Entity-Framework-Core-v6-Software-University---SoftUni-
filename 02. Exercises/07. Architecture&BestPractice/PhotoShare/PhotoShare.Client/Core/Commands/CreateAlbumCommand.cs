namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string [] data)
        {
            using (PhotoShareContext db = new PhotoShareContext())
            {
                var username = data[1];
                var album = data[2];
                var bgColor = data[3];
                var tags = data.Skip(4).ToArray();
                User user = db.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (!db.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (db.Albums.Any(a => a.Name == album))
                {
                    throw new ArgumentException($"Album {album} exists!");
                }
                Color color;
                if (!Enum.TryParse(bgColor, out color))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }
                for (int i = 0; i < tags.Length; i++)
                {
                    if (!db.Tags.Any(t => t.Name == tags[i]))
                    {
                        throw new ArgumentException($"Invalid tags!");
                    }
                }
                var al = new Album()
                {
                    Name = album,
                    BackgroundColor = color,
                    AlbumRoles = new List<AlbumRole>()
                    {
                        new AlbumRole()
                        {
                            User = user,
                            Role = Role.Owner
                        }
                    },
                    AlbumTags = tags.Select(t => new AlbumTag()
                    {
                        Tag = db.Tags
                            .FirstOrDefault(ct => ct.Name == t)
                    })
                    .ToArray()
                };
                db.Albums.Add(al);
                db.SaveChanges();
                return $"Album {album} successfully created!";
            }
        }
    }
}
