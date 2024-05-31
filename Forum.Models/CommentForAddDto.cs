
using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Forum.Models
{
    public class CommentForAddDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public Post Post { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public IdentityUser User { get; set; }
    }
}
