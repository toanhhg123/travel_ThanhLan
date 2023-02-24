using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace source.Config
{
    public class ConfigRoles
    {
        public string RoleAdmin { get; set; } = default!;
        public string RoleCustomer { get; set; } = default!;
        public string RoleHr { get; set; } = default!;

    }
  
}