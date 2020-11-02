using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RAZAM.Models
{
    public class RazamContext : IdentityDbContext<RazamUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Note> Notes { get; set; }

        public RazamContext() : base("RazamDb") { }

        public static RazamContext Create()
        {
            return new RazamContext();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Note>()
        //        .HasRequired(o => o.Receiver)
        //        .WithMany(c => c.Notes)
        //        .HasForeignKey(o => o.ReceiverId);

        //    modelBuilder.Entity<Note>()
        //        .HasRequired(o => o.Sender)
        //        .WithMany(c => c.Notes)
        //        .HasForeignKey(o => o.SenderId);
        //}
    }
}