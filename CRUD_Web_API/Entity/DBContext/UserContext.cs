﻿using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRUD_Web_API.Entity.DBContext
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> User { get; set; }

    }
}

