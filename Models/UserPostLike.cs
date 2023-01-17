#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models;

public class UserPostLike
{
    [Key]
    public int UserPostLikeId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Foreign Keys

    public int UserId { get; set; }
    public User? User { get; set; }

    public int PostId { get; set; }
    public Post? Post { get; set; }
}