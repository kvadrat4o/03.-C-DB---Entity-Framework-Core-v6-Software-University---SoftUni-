namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        //Password
        //Email
        //ProfilePicture
        //FirstName
        //LastName
        //BornTown
        //CurrentTown
        //Age





        public static string Execute(string[] data)
        {
            using (PhotoShareContext db = new PhotoShareContext())
            {
                var username = data[1];
                var property = data[2].ToLowerInvariant();
                var newValue = data[3];
                if (db.Users.Any(u => u.Username != username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                var user = db.Users.SingleOrDefault(u => u.Username == username);
                switch (property)
                {
                    case "password":
                        bool hasDigits = false;
                        bool hasLower = false;
                        for (int i = 0; i < newValue.Length; i++)
                        {
                            if (Char.IsDigit(newValue[i]))
                            {
                                hasDigits = true;
                            }
                            if (Char.IsLower(newValue[i]))
                            {
                                hasLower = true;
                            }
                        }

                        if (!hasLower && !hasDigits)
                        {
                            throw new ArgumentException($"Value {newValue} not valid.\nInvalid Password");
                        }
                        var password = db.Users.Where(u => u.Username == username).Select(u => u.Password).ToString();
                        password = newValue;
                    break;
                    case "age":
                        var age = int.Parse(newValue);
                        if (age <= 0)
                        {
                            throw new ArgumentException($"Value {age} not valid.\nInvalid Age!");
                        }
                        var userAge = 0;
                        int.TryParse(db.Users.Where(u => u.Username == username).Select(u => u.Age).ToString(), out userAge);
                        userAge = age;
                        break;
                    case "firstname":
                        bool hasInvalidChars = true;
                        int count = 0;
                        for (int i = 0; i < newValue.Length; i++)
                        {
                            if (Char.IsPunctuation(newValue[i]))
                            {
                                hasInvalidChars = false;
                            }
                            count++;
                        }
                        if (!hasInvalidChars)
                        {
                            throw new ArgumentException($"Value {newValue} not valid.\nInvalid First name character {newValue.Substring(count,1)}!");
                        }
                        var fName = db.Users.Where(u => u.Username == username).Select(u => u.FirstName).ToString();
                        fName = newValue;
                        break;
                    case "lastname":
                        bool hasInvalidChar = true;
                        int index = 0;
                        for (int i = 0; i < newValue.Length; i++)
                        {
                            if (Char.IsPunctuation(newValue[i]))
                            {
                                hasInvalidChars = false;
                            }
                            index++;
                        }
                        if (!hasInvalidChar)
                        {
                            throw new ArgumentException($"Value {newValue} not valid.\nInvalid Last name character {newValue.Substring(index, 1)}!");
                        }
                        var lName = db.Users.Where(u => u.Username == username).Select(u => u.LastName).ToString();
                        lName = newValue;
                        break;
                    case "borntown":
                        if (!db.Towns.Any(t => t.Name == newValue))
                        {
                            throw new ArgumentException($"Town {newValue} not found!");
                        }
                        var bTown = db.Users.Where(u => u.Username == username).Select(u => u.BornTown).ToString();
                        bTown = newValue;
                        break;
                    case "currenttown":
                        if (!db.Towns.Any(t => t.Name == newValue))
                        {
                            throw new ArgumentException($"Town {newValue} not found!");
                        }
                        var cTown = db.Users.Where(u => u.Username == username).Select(u => u.CurrentTown).ToString();
                        cTown = newValue;
                        break;
                    case "email":
                        if (!newValue.Contains("@"))
                        {
                            throw new ArgumentException($"Value {newValue} not valid.\nInvalid Email address!");
                        }
                        var email = db.Users.Where(u => u.Username == username).Select(u => u.Email).ToString();
                        email = newValue;
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                        break;
                }
                db.SaveChanges();
                string result = $"User {user} {property} is {newValue}.";
                return result;
            }
        }
    }
}
