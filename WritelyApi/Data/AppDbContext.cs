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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure Journal properties.
            builder.Entity<Journal>()
                .Property(j => j.Title)
                .HasMaxLength(80)
                .IsRequired();

            // Configure Entry properties.
            builder.Entity<Entry>()
                .Property(e => e.Title)
                .HasMaxLength(80)
                .IsRequired();

            builder.Entity<Entry>()
                .Property(e => e.Tags)
                .HasMaxLength(80);

            builder.Entity<Entry>()
                .Property(e => e.Body)
                .HasMaxLength(3000)
                .IsRequired();

            // Specify entity relationships.
            builder.Entity<Journal>()
                .HasMany(journal => journal.Entries)
                .WithOne(entry => entry.Journal);
        }
    }
}