using System.ComponentModel.DataAnnotations;

namespace AiGallery.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }       
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;        
        public string Note { get; set; } = default!;
    }
}



