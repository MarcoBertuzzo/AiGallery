using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AiGallery.Data
{
    public class DbEntities
    {

        [Table("Strip")]
        public class Strip
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Title_Eng { get; set; } = default!;
            [Required]
            public string Title_Ita { get; set; } = default!;
            public int ViewsCounter { get; set; }
            public DateTime? LastView { get; set; }
        }



        [Table("Images")]
        public class Image
        {
            [Key]
            public int StripId { get; set; }
            [Key]
            public int Id { get; set; }
            public string Description_Eng { get; set; } = default!;
            public string Description_Ita { get; set; } = default!;
        }



        [Table("User")]
        public class User
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Note { get; set; } = default!;
        }



        [Table("UserImage")]
        public class UserImage
        {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }
            public string FileName { get; set; } = default!;
        }

    }
}
