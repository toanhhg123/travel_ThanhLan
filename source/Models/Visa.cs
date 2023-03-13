using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class Visa
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();

        public string title { set; get; } = "";
        public string mainImg { set; get; } = "";
        public DateTime? createdAt { set; get; }

        [Column(TypeName = "ntext")]
        public string? info { set; get; }

    }




}