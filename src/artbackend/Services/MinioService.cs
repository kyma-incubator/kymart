// using System.IO;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
// using Minio;

// namespace artbackend.Services
// {
//     public class MinioService
//     {
//         private const string ImageKey = "original.png";
        
//         private readonly MinioClient minio;

//         public MinioService(IConfiguration configuration)
//         {
//             var minioSection = configuration.GetSection("minio");
//             minio = new MinioClient(minioSection["host"], minioSection["user"], minioSection["password"]);
//         }

//         private async Task CreateBucketIfNotExist(string bucketName)
//         {
//             if (!await minio.BucketExistsAsync(bucketName))
//             {
//                 await minio.MakeBucketAsync(bucketName);
//             }
//         }

//         public async Task<MemoryStream> GetImage(string imageId)
//         {
//             if (!await minio.BucketExistsAsync(imageId)) {
//                 return null;
//             }

//             MemoryStream result = null;
//             try {
//                 await minio.GetObjectAsync(imageId, ImageKey, source => {
//                     result = new MemoryStream();
//                     source.CopyTo(result);
//                     result.Position = 0;
//                 });
//             }
//             catch(Minio.Exceptions.ObjectNotFoundException) {}
//             return result;
//         }

//         public async Task CreateImage(Stream source, string imageId) {
//             await CreateBucketIfNotExist(imageId);
//             var destination = new MemoryStream();
//             source.CopyTo(destination);
//             destination.Position = source.Position = 0;
//             await minio.PutObjectAsync(imageId, ImageKey, destination, destination.Length, "image/png");
//         }
//     }
// }
  // "minio": {
  //   "host": "127.0.0.1:9000",
  //   "user": "",
  //   "password": ""
  // },