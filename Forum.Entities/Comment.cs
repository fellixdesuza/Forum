using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

      
        public Post Post { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
