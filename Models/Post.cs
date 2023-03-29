#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using SocialMedia.Models;

public class Post
{
    [Key]// Primary Key
    public int PostId { get; set; } // __ID , __ needs to match the name of the class

    [Display(Name = "Visual Image (Img URL)")]
    public string ImgUrl { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(1, ErrorMessage = "must be more than one character.")]
    public string Thought { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Foreign keys

    public int UserId { get; set; } //must match primary key
    public User? Author { get; set; }

    public List<UserPostLike> PostLikes { get; set; } = new List<UserPostLike>();
    public List<UserPostDislike> PostDislikes { get; set; } = new List<UserPostDislike>();
    public List<UserPostComment> PostComments { get; set; } = new List<UserPostComment>();
}