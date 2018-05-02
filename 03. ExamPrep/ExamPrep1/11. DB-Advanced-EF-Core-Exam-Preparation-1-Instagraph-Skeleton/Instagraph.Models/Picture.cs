using System.Collections.Generic;

namespace Instagraph.Models
{
    public class Picture
    {
        public Picture()
        {

        }

        public int Id { get; set; }

        public string Path { get; set; }

        public decimal Size { get; set; }

        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
