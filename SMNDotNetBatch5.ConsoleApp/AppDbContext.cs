using Microsoft.EntityFrameworkCore;
using SMNDotNetBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMNDotNetBatch5.ConsoleApp
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=WINDOWS-1ISKG05\\SQLEXPRESS; Initial Catalog=DotNetTrainingBatch5;Trusted_Connection=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
