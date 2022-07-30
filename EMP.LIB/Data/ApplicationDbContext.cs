using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMP.LIB.Models;


namespace EMP.LIB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<EmpDetails> EmpDetails { get; set; }

        public DbSet<EmpSalarys> EmpSalarys { get; set; }

        public DbSet<Users> Users { get; set; }

    }
}
