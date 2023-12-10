using System.Net;
using System.Security.Claims;
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
            var post = db.Posts.Find(id);
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
            string userID = HttpContext.Items["userId"].ToString();
            if (string.IsNullOrWhiteSpace(body.content)) return BadRequest();
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
            string userID = HttpContext.Items["userId"].ToString();
            Post post = db.Posts.Find(id);
            if (post == null) return NotFound();
            if (post.UserId != userID) return Unauthorized();
            if (string.IsNullOrWhiteSpace(body.content)) return BadRequest();
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
            Post post = db.Posts.Find(id);
            if (post == null) return NotFound();
            string userID = HttpContext.Items["userId"].ToString();
            if (post.UserId != userID) return Unauthorized();
            db.Posts.Remove(post);
            db.SaveChanges();
            return Accepted();
        }
    }
}
