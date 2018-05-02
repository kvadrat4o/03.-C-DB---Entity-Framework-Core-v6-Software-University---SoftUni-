namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            int albumId = int.Parse(data[1]);
            string username = data[2];
            string permissionInput = data[3];

            using (PhotoShareContext db = new PhotoShareContext())
            {
                Album album = db.Albums
                    .Find(albumId);

                if (album == null)
                {
                    throw new ArgumentException($"Album with Id {albumId} not found!");
                }

                User user = db.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                Role permission;
                if (!Enum.TryParse(permissionInput, out permission))
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                AlbumRole newAlbumRole = new AlbumRole()
                {
                    Album = album,
                    User = user,
                    Role = permission
                };

                if (db.AlbumRoles.Any(ar => ar.Album == newAlbumRole.Album && ar.User == newAlbumRole.User))
                {
                    throw new ArgumentException($"Album {album.Name} already shared to {user.Username} with role {permission}");
                }

                db.AlbumRoles.Add(newAlbumRole);
                db.SaveChanges();

                return $"Username {user.Username} added to album {album.Name} ({permission})";
            }
        }
    }
}
