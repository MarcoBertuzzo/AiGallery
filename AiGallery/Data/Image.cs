using System.ComponentModel.DataAnnotations;

namespace AiGallery.Data
{
    public class Image
    {
        [Key]
        public int StripId { get; set; }
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = default!;
        public string RelativePath { get; set; } = default!;
        public string Description_Eng { get; set; } = default!;
        public string Description_Ita { get; set; } = default!;
    }
}
