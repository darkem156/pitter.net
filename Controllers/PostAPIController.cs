using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pitter.Models;

namespace Pitter.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostAPIController : ControllerBase
    {
        private PitterContext db = new PitterContext();
        
        // GET: api/posts
        [HttpGet]
        public IActionResult Get()
        {
            var posts = db.Posts;
            
            var res = new { posts };
            return Ok(res);
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var post = db.Posts.FindAsync(id).Result;
            if (post == null) return NotFound();
            var res = new { post };
            return Ok(res);
        }
        
        public class PostBody
        {
            public string content { get; set; }
        }

        // POST api/posts
        [HttpPost]
        public IActionResult Post([FromBody] PostBody body)
        {
            ClaimsPrincipal user = HttpContext.User;
            if (!user.Identity.IsAuthenticated) return Unauthorized();
            if (body.content == "") return BadRequest();
            string userID = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var post = new Post
            {
                UserId = userID,
                Content = body.content,
                ResponseToPostId = 14,
            };
            db.Posts.Add(post);
            db.SaveChanges();
            return Created();
        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] PostBody body)
        {
            ClaimsPrincipal user = HttpContext.User;
            if (!user.Identity.IsAuthenticated) return Unauthorized();
            Post post = db.Posts.FindAsync(id).Result;
            if (post == null) return NotFound();
            string userID = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (post.UserId != userID) return Unauthorized();
            if (body.content == "") return BadRequest();
            post.Content = body.content;
            db.Posts.Update(post);
            db.SaveChanges();
            var res = new
            {
                post.Content,
                post.Date,
                post.ResponseToPostId
            };
            return Created();
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            ClaimsPrincipal user = HttpContext.User;
            if (!user.Identity.IsAuthenticated) return Unauthorized();
            Post post = db.Posts.FindAsync(id).Result;
            if (post == null) return NotFound();
            string userID = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (post.UserId != userID) return Unauthorized();
            db.Posts.Remove(post);
            db.SaveChanges();
            return Accepted();
        }
    }
}
