using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using Microsoft.EntityFrameworkCore;

public class PostsController : Controller
{

    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }

    private SocialMediaContext db;
    public PostsController(SocialMediaContext context)
    {
        db = context;
    }

    [HttpGet("/posts/new")]
    public IActionResult New()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }
        return View("New");
    }

    [HttpPost("/posts/create")]
    public IActionResult Create(Post newPost)
    {
        if(!loggedIn || uid == null) // add || uid == null to remove lines
        {
            return RedirectToAction("Index", "Users");
        }
        if (!ModelState.IsValid)
        {
            return New();
        }

        newPost.UserId = (int)uid;

        db.Posts.Add(newPost);
        //db doesnt update until we run save changes
        db.SaveChanges();

        return RedirectToAction("All");
    }

    [HttpGet("/posts")]
    public IActionResult All()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }
        List<Post> allPosts = db.Posts
        .OrderByDescending(o => o.CreatedAt)
        .Include(v => v.Author)
        .Include(v => v.PostLikes)
        .ToList()
        ;

        return View("All", allPosts);

    }

    [HttpGet("/posts/{onePostId}")]
    public IActionResult GetOnePost(int onePostId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }

        Post? Post = db.Posts
        .Include(p => p.Author)
        .Include(v => v.PostLikes)
            .ThenInclude(vs => vs.User)
        .FirstOrDefault(v => v.PostId == onePostId);

        if (Post == null)
        {
            return RedirectToAction("All");
        }
        return View("ViewOne", Post);
    }

    [HttpPost("/posts/{deletedPostId}/delete")]
    public IActionResult DeletePost(int deletedPostId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }

        Post? post = db.Posts.FirstOrDefault(p => p.PostId == deletedPostId);

        if (post != null && post.UserId == uid)
        {
            db.Posts.Remove(post);
            db.SaveChanges();
        }
        return RedirectToAction("All");
    }

    [HttpGet("/posts/{postId}/edit")]
    public IActionResult Edit(int postId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }

        Post? post = db.Posts.FirstOrDefault(p => p.PostId == postId);

        if(post == null || post.UserId != uid)
        {
            return RedirectToAction("All");
        }
        return View("Edit", post);
    }

    [HttpPost("/posts/{postId}/update")]
    public IActionResult Update(Post editedPost, int postId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Index", "Users");
        }

        if (ModelState.IsValid == false)
        {
            return Edit(postId);
        }

        Post? dbPost = db.Posts.FirstOrDefault(post => post.PostId == postId);

        if (dbPost == null || dbPost.UserId != uid)
        {
            return RedirectToAction("All");
        }

        dbPost.ImgUrl = editedPost.ImgUrl;
        dbPost.Thought = editedPost.Thought;
        dbPost.UpdatedAt = DateTime.Now;

        db.Posts.Update(dbPost);
        db.SaveChanges();

        return RedirectToAction("GetOnePost", new { onePostId = dbPost.PostId });
        // return Redirect($"/posts/{dbPost.PostId}");
        // return RedirectToAction("GetOnePost");
        // return RedirectToAction("Index", "Users");
    }

    [HttpPost("/posts/{postId}/like")]
    public IActionResult Like(int postId)
    {
        if (!loggedIn || uid == null)
        {
            return RedirectToAction("Index", "Users");
        }

        UserPostLike? existingLike = db.UserPostLikes
            .FirstOrDefault(l => l.PostId == postId && l.UserId == (int)uid);

            if (existingLike == null)
            {
                UserPostLike newLike = new UserPostLike()
                {
                    UserId = (int)uid,
                    PostId = postId
                };

            db.UserPostLikes.Add(newLike);
            }
            else
            {
                db.UserPostLikes.Remove(existingLike);
            }

        db.SaveChanges();
        return RedirectToAction("All");
    }
}