using System.ComponentModel.DataAnnotations;

namespace AiGallery.Data
{
    public class UserImage
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = default!;
    }
}



