using Forum.Entities;
using System.ComponentModel.DataAnnotations;


namespace Forum.Models
{
    public class PostForAddDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
