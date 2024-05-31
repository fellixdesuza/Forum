using Forum.Entities;
using Forum.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Forum.Models
{
    public class CommentForGetDto
    {
    
        public int Id { get; set; }

        public string Content { get; set; }

    
        public DateTime CreatedAt { get; set; }

     
        public int PostId { get; set; }


        public Post Post { get; set; }

        public string UserId { get; set; }

        public UserDto User { get; set; }

    }
}
