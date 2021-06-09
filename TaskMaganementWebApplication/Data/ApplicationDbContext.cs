using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskMaganementWebApplication.Models;

namespace TaskMaganementWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
    public class ApplicationTaskDbContext :  DbContext
    {
        public ApplicationTaskDbContext(DbContextOptions<ApplicationTaskDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationTasks> ApplicationTasks { get; set; }
    }
    
}
