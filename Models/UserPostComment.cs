#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models;

public class UserPostComment
{
    [Key]
    public int UserPostCommentId { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(1, ErrorMessage = "must be more than one character.")]
    [Display(Name = "Opinion")]
    public string Opinion { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Foreign Keys
    List<UserPostComment> UserPostComments { get; set; } = new List<UserPostComment>();

    public int UserId { get; set; }
    public User? User { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }
}
