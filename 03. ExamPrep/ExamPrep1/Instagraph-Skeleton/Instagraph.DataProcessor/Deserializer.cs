using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public const string FailureMessage = "Error: Invalid data.";
        public const string SuccessMessage = "Successfully imported {0}";
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            Picture[] deserializedPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var pictures = new List<Picture>();

            foreach (var picture in deserializedPictures)
            {
                bool isValid = !String.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;

                bool pictureExists = context.Pictures.Any(p => p.Path == picture.Path) ||
                    pictures.Any(p => p.Path == picture.Path);

                if (!isValid || pictureExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                pictures.Add(picture);
                sb.AppendLine(String.Format(SuccessMessage, $"Picture {picture.Path}"));
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();

            return result;
        }
        
        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<User> users = new List<User>();
            var deserializedUsers = JsonConvert.DeserializeObject<User[]>(jsonString);
            foreach (var user in deserializedUsers)
            {
                bool passIsValid = !String.IsNullOrWhiteSpace(user.Password) && user.Password.Length <= 20;
                bool usernameIsValid = !String.IsNullOrWhiteSpace(user.Username) || user.Username.Length <= 30;
                bool picIsvalid = !String.IsNullOrWhiteSpace(user.ProfilePicture.Path) || user.ProfilePicture.Size > 0;
                bool userExists = users.Any(u => u.Username == user.Username);
                if (!passIsValid || !usernameIsValid || !picIsvalid || context.Pictures.FirstOrDefault(p => p.Path == user.ProfilePicture.Path) == null || users.Any(u => u.Username == user.Username) || userExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                User userV = new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    ProfilePicture = user.ProfilePicture
                };
                users.Add(userV);
                sb.AppendLine($"Successfully imported User {user.Username}.");
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();



            return sb.ToString();
            throw new NotImplementedException();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();



            return sb.ToString();
            throw new NotImplementedException();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();



            return sb.ToString();
            throw new NotImplementedException();
        }

        private static bool IsValid(Object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

    }
}
