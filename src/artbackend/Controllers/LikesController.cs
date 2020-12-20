using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using artbackend.Database;
using Microsoft.EntityFrameworkCore;
using artbackend.ViewModel;
using System.Linq;
using System;

namespace artbackend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly LikesContext context;

        public LikesController(LikesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<LikesForImageViewModel>> GetLikesForImage([FromQuery] string imageId, [FromQuery] string imageVersion, [FromQuery] string email)
        {
            var likes = context.Likes.Where(like => like.ImageId == imageId && like.ImageVersion == imageVersion);
            return new OkObjectResult(new LikesForImageViewModel{
                IsLiked = email != null && await likes.AnyAsync(like => like.Email == email),
                LikesCount = await likes.CountAsync(),
            });
        }
        
        [HttpGet]
        [Route("liked")]
        public async Task<ActionResult<IEnumerable<ImageViewModel>>> GetLikedImages([FromQuery] string email)
        {
            return new ObjectResult(await context.Likes
                .Where(like => like.Email == email)
                .Select(like => new ImageViewModel { ImageId = like.ImageId, ImageVersion = like.ImageVersion })
                .ToListAsync());
        }
        
        [HttpPost]
        public async Task<ActionResult<Like>> CreateLike([FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await context.Likes.FirstOrDefaultAsync(l => like.Equals(l)) != null) {
                return Conflict();
            }

            await context.Likes.AddAsync(like);
            await context.SaveChangesAsync();

            return like;
        }
        
        [HttpDelete]
        public async Task<ActionResult<Like>> DeleteLike([FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbLike = await context.Likes.FirstOrDefaultAsync(l => like.Equals(l));
            if (dbLike == null) {
                return NotFound();
            }

            context.Likes.Remove(dbLike);
            await context.SaveChangesAsync();

            return dbLike;
        }
    }
}
