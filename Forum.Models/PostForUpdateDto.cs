
using Forum.Entities;

namespace Forum.Models
{
    public class PostForUpdateDto
    {
        public int Id { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }


        public DateTime CreatedAt { get; set; }


        public string UserId { get; set; }

        public State State { get; set; }

        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
