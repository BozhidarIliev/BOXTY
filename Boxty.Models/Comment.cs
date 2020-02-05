using System;
using System.ComponentModel.DataAnnotations;

namespace Boxty.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; }

        [Required]
        public DateTime CommentedOn { get; set; }

    }
}