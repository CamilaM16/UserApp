using System;
using System.Data.SqlClient;
using System.Data.Entity;

namespace user_app.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(): base("name=UserContext") { }

        public DbSet<User> Users { get; set; }
    }
}