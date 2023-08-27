using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SRM.Models;

namespace SRM.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }
        public DbSet<StudentResourceMapModel> StudentResourceMap { get; set;}

    }
}
