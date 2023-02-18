using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class CategoryTour
    {
        [Key]
        public string id { set; get; } = Guid.NewGuid().ToString();
        public string name { set; get; } = default!;
    }
}