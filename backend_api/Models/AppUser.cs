using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_api.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios = new List<Portfolio>();
    }
}