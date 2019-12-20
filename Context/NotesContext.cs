using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Context
{
    public class NotesContext : DbContext
    {
        public NotesContext()
        {            
            Database.EnsureCreated();
        }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-RM1NBDJ;Database=NotesApp;Trusted_Connection=True;");
        }
    }
}
