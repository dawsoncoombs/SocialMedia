#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialMedia.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "is required!")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "is required!")]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Profile Picture")]
    public string ProfileImg { get; set; }

    [Required(ErrorMessage = "is required!")]
    [MinLength(8, ErrorMessage = "must be at least 8 characters!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped] // doesnt add colum to db
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "doesnt match password!")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Foreign Keys

    List<Post> UserPosts { get; set; } = new List<Post>();

    public List<UserPostLike> UserLikes { get; set; } = new List<UserPostLike>();
    public List<UserPostDislike> UserDislikes { get; set; } = new List<UserPostDislike>();

}