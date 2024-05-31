using Forum.Entities;
using Forum.Models.Identity;

namespace Forum.Models
{
    public class PostForGetDto
    {
       
        public int Id { get; set; }


        public string Title { get; set; }


        public string Content { get; set; }


        public DateTime CreatedAt { get; set; }

      
        public string UserId { get; set; }

        public UserDto User { get; set; }

        public State State { get; set; }

        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
