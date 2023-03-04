using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace source.Models
{
    public class TravelContext : DbContext
    {

        public TravelContext(DbContextOptions<TravelContext> options)
         : base(options)
        {
        }



        public DbSet<CategoryTour> CategoryTours { set; get; } = default!;
        public DbSet<Role> Roles { set; get; } = default!;
        public DbSet<Account> Accounts { set; get; } = default!;
        public DbSet<Tour> Tours { set; get; } = default!;
        public DbSet<TourImage> TourImages { set; get; } = default!;
        public DbSet<Hotel> Hotels { set; get; } = default!;

        public DbSet<HotelImg> HotelImages { set; get; } = default!;

        public DbSet<Transport> Transports { set; get; } = default!;

        public DbSet<TransportImage> TransportImages { set; get; } = default!;
        public DbSet<OrderTour> OrderTours { set; get; } = default!;
        public DbSet<OrderHotel> OrderHotels { set; get; } = default!;
        public DbSet<OrderTransport> OrderTransports { set; get; } = default!;








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryTour>()
                .Property(b => b.id)
                .HasDefaultValue("newid()");
            modelBuilder.Entity<Role>()
              .Property(b => b.id)
              .HasDefaultValue("newid()");


            modelBuilder.Entity<Account>()
             .Property(b => b.id)
             .HasDefaultValue("newid()");

            modelBuilder.Entity<Account>()
           .Property(b => b.createdAt)
           .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Tour>()
                       .Property(b => b.id)
                       .HasDefaultValue("newid()");

            modelBuilder.Entity<Tour>()
              .Property(b => b.createdAt)
         .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<TourImage>()
                         .Property(b => b.id)
                         .HasDefaultValue("newid()");

            modelBuilder.Entity<Hotel>()
          .Property(b => b.id)
          .HasDefaultValue("newid()");

            modelBuilder.Entity<Hotel>()
              .Property(b => b.createdAt)
         .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<HotelImg>()
                         .Property(b => b.id)
                         .HasDefaultValue("newid()");

            modelBuilder.Entity<Transport>()
        .Property(b => b.id)
        .HasDefaultValue("newid()");

            modelBuilder.Entity<Transport>()
              .Property(b => b.createdAt)
         .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<TransportImage>()
                         .Property(b => b.id)
                         .HasDefaultValue("newid()");


            modelBuilder.Entity<OrderTour>()
            .Property(b => b.id)
            .HasDefaultValue("newid()");

            modelBuilder.Entity<OrderTour>()
               .Property(b => b.createdAt)
          .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<OrderHotel>()
            .Property(b => b.id)
            .HasDefaultValue("newid()");

            modelBuilder.Entity<OrderHotel>()
               .Property(b => b.createdAt)
          .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<OrderTransport>()
            .Property(b => b.id)
            .HasDefaultValue("newid()");

            modelBuilder.Entity<OrderTransport>()
               .Property(b => b.createdAt)
          .HasDefaultValueSql("getdate()");





        }

    }
}