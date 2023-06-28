using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Communication
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Online> Onlines { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}
