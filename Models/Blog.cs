using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeNivelatorio.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Image { get; set; }
        [Range(1,5)]
        public int Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
    }
}
