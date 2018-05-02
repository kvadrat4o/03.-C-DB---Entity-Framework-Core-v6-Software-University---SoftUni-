﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public  class Post
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Caption { get; set; }
        
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        
        public int PictureId { get; set; }
        [Required]
        public Picture Picture { get; set; }
        [Required]
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}