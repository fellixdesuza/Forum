using Forum.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Forum.Models
{
    public class CommentForUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
