using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    public class Post
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string Title { get; set; }


        [Required]
        public string Content { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey(nameof(IdentityUser))]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public State State { get; set; }

        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }

}
