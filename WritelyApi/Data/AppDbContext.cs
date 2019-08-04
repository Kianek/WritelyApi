using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WritelyApi.Entries;
using WritelyApi.Journals;
using WritelyApi.Users;

namespace WritelyApi.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}