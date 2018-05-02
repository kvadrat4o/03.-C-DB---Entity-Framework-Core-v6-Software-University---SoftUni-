namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            var username = data[1];

            using (PhotoShareContext db = new PhotoShareContext())
            {
                var user = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.FriendsAdded.Count == 0)
                {
                    return $"No friends for this user. :(";
                }

                var friends = user.FriendsAdded
                    .OrderBy(f => f.Friend.Username)
                    .Select(f => "-" + f.Friend.Username);

                return "Friends: " + Environment.NewLine + string.Join(Environment.NewLine, friends);
            }
        }
    }
}
