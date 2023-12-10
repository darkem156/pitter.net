using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitter.Models;

namespace Pitter.Controllers
{
    [Route("api/follow")]
    [ApiController]
    public class FollowAPIController : ControllerBase
    {
        private PitterContext db = new PitterContext();
        
        // POST api/follow/{id}
        [HttpPost("{id}")]
        public IActionResult Post(string id)
        {
            string userID = HttpContext.Items["userId"].ToString();
            var follows = db.Follows.Find(userID, id);

            if (follows != null) Conflict();

            var follow = new Follow
            {
                IdFollower = userID,
                IdFollowing = id,
            };
            
            db.Follows.Add(follow);
            db.SaveChanges();
            
            return Accepted();
        }

        // DELETE api/follow/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string userID = HttpContext.Items["userId"].ToString();
            var follow = db.Follows.Find(userID, id);

            if (follow == null) NotFound();

            db.Follows.Remove(follow);
            db.SaveChanges();
            return Accepted();
        }
    }
}
