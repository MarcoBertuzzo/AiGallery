using AiGallery.Components;
using Microsoft.EntityFrameworkCore;
using System;
using static AiGallery.Data.DbEntities;

namespace AiGallery.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<DbEntities.Strip> Strips { get; set; }
        public DbSet<DbEntities.Image> Images { get; set; }
        public DbSet<DbEntities.User> Users { get; set; }
        public DbSet<DbEntities.UserImage> UserImages { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { } //constructor that is called automatically
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEntities.Strip>()
               .HasKey(p => p.Id);

            modelBuilder.Entity<DbEntities.Strip>()
                .Property(b => b.Title_Eng)
                .IsRequired();

            modelBuilder.Entity<DbEntities.Strip>()
                .Property(b => b.Title_Ita)
                .IsRequired();

            modelBuilder.Entity<DbEntities.Strip>()
                .Property(b => b.ViewsCounter);

            modelBuilder.Entity<DbEntities.Strip>()
                .Property(b => b.LastView);


            /*
            modelBuilder.Entity<DbEntities.Strip>().HasData(
                new DbEntities.Strip() { Id = 1, Title_Eng = "From a poor man to a rich man", Title_Ita = "Da uomo povero a uomo ricco", ViewsCounter=0 },
                new DbEntities.Strip() { Id = 2, Title_Eng = "From ugly man to handsome man", Title_Ita = "Da uomo brutto a uomo bello", ViewsCounter=0 },
                new DbEntities.Strip() { Id = 3, Title_Eng = "From beautiful woman to ugly woman", Title_Ita = "Da donna brutta a donna bella", ViewsCounter=0 },
            );
            */


            modelBuilder.Entity<DbEntities.Image>()
                .HasKey(p => new { p.StripId, p.Id });

            modelBuilder.Entity<DbEntities.Image>()
                .Property(b => b.Description_Eng);

            modelBuilder.Entity<DbEntities.Image>()
            .Property(b => b.Description_Ita);



            modelBuilder.Entity<DbEntities.User>()
                .HasKey(p => new { p.Id });

            modelBuilder.Entity<DbEntities.User>()
                .Property(b => b.Name);

            modelBuilder.Entity<DbEntities.User>()
            .Property(b => b.Email);



            modelBuilder.Entity<DbEntities.UserImage>()
                .HasKey(p => new { p.Id });

            modelBuilder.Entity<DbEntities.UserImage>()
                .Property(b => b.UserId);

            modelBuilder.Entity<DbEntities.UserImage>()
            .Property(b => b.FileName);

        }
    }
}