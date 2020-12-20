// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using artbackend.Services;
// using System;
// using System.IO;

// namespace artbackend.Controllers
// {
    
//     [ApiController]
//     // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     [AllowAnonymous]
//     [Route("[controller]")]
//     public class ImagesController : ControllerBase
//     {
//         private readonly MinioService minio;
//         private readonly ArtProvider artProvider;

//         public ImagesController(MinioService minio, ArtProvider artProvider)
//         {
//             this.minio = minio;
//             this.artProvider = artProvider;
//         }

//         [HttpGet]
//         public async Task<ActionResult<Stream>> GetImage([FromQuery] string imageId) {
//             MemoryStream image = await minio.GetImage(imageId);
//             if (image == null) {
//                 image = await artProvider.GetArt(imageId);
//                 await minio.CreateImage(image, imageId);
//             }
//             return new FileStreamResult(image, "image/jpg");
//         }
//     }
// }
