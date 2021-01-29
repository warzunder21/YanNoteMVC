using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YanNote.Models;

namespace YanNote.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Note { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Rem> Rem { get; set; }
        


    }
}
