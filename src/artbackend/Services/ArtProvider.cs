using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace artbackend.Services
{
    public class ArtProvider
    {
        private static readonly HttpClient client = new HttpClient();
        
        public async Task<MemoryStream> GetArt(string imageId)
        {
            using (var stream = await client.GetStreamAsync($"https://art-explorer.kyma.fun/art/{imageId}"))
            {
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                ms.Position = 0;
                return ms;
            }
        }
    }
}