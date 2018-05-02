namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var userUsername = data[1];
            var friendUsername = data[2];

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
                    throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}!");
                }

                if (isTheUserAFriend)
                {
                    throw new InvalidOperationException($"The request is already sent to {friend.Username}!");
                }

                if (isTheFriendAUserFriend)
                {
                    throw new InvalidOperationException($"The request is already sent to {user.Username}!" +
                        Environment.NewLine +
                        $"If you want to accept {friend.Username} as a friend, please insert the command \"AcceptFriend {user.Username} {friend.Username}\"!");
                }

                var newFriendship = new Friendship()
                {
                    User = user,
                    Friend = friend
                };

                user.FriendsAdded.Add(newFriendship);
                db.SaveChanges();

                return $"Friend {friend.Username} added to {user.Username}!";
            }
        }
    }
}
