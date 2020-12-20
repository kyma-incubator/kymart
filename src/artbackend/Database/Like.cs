using System.ComponentModel.DataAnnotations;

namespace artbackend.Database
{
    public class Like
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ImageId { get; set; }

        [Required]
        public string ImageVersion { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is Like like)
            {
                return like.Email == Email &&
                    like.ImageId == ImageId &&
                    like.ImageVersion == ImageVersion;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode() ^ ImageId.GetHashCode() ^ ImageVersion.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Email} {ImageId}.{ImageVersion}";
        }
    }
}
