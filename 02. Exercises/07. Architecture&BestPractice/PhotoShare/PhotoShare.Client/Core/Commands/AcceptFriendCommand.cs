namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var userUsername = data[0];
            var friendUsername = data[1];

            using (PhotoShareContext db = new PhotoShareContext())
            {
                var user = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(f => f.User)
                    .FirstOrDefault(u => u.Username == userUsername);

                if (user == null)
                {
                    throw new ArgumentException($"{userUsername} not found!");
                }
                var friend = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(f => f.User)
                    .FirstOrDefault(u => u.Username == friendUsername);

                if (friend == null)
                {
                    throw new ArgumentException($"{friendUsername} not found!");
                }

                bool isTheUserAFriend = user.FriendsAdded.Any(f => f.Friend == friend);
                bool isTheFriendAUserFriend = friend.FriendsAdded.Any(f => f.Friend == user);

                if (isTheUserAFriend && isTheFriendAUserFriend)
                {
                    throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}");
                }

                if (!isTheFriendAUserFriend)
                {
                    throw new InvalidOperationException($"{friend.Username} has not added {user.Username} as a friend");
                }

                var accpetFriendship = new Friendship()
                {
                    User = user,
                    Friend = friend
                };

                user.FriendsAdded.Add(accpetFriendship);
                db.SaveChanges();

                return $"{user.Username} accepted {friend.Username} as a friend";
            }
        }
    }
}
