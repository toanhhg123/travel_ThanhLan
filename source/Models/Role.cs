using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class Role
    {
        [Key]
        public string id { set; get; } = default!;

        [Required(ErrorMessage = "Không được bỏ trống trường hợp này")]
        public string RoleName { get; set; } = default!;
        public string? Desc { get; set; } = default!;
    }
}