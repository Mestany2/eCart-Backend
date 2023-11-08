using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using eCart_Backend.Models;

namespace eCart_Backend
{
    public class eCartDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Status> Status { get; set; }
        public eCartDbContext(DbContextOptions<eCartDbContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data with Users
            modelBuilder.Entity<User>().HasData(new User[]
            {
        new User {Id = 1, Name = "Jack Smith", Address="123 Street", Phone="615-445-8855",Email="jack@yahoo.com", Uid="4d5256236asd6", isSeller = true},
        new User {Id = 2, Name = "Mike Daniel", Address="856 Road", Phone="615-645-2415",Email="Mdaniel@gmail.com", Uid="4d53246asd6", isSeller = false},
        new User {Id = 3, Name = "Steve Butler", Address="789 Circle", Phone="615-746-3641",Email="sbutler@comcast.net", Uid="4d56a2526sd6", isSeller = true},


            });

            modelBuilder.Entity<Order>().HasData(new Order[]
    {
         new Order {Id = 1, StatusId = 1, OrderDate=new DateTime(2023, 11, 01), PaymentId=1, UserId=1, OrderTotal=159.55M},
         new Order {Id = 2, StatusId = 3, OrderDate=new DateTime(2023, 11, 05), PaymentId=1, UserId=2, OrderTotal=145.01M},
         new Order {Id = 3, StatusId = 2, OrderDate=new DateTime(2023, 11, 03), PaymentId=2, UserId=2, OrderTotal=252.22M},
         new Order {Id = 4, StatusId = 2, OrderDate=new DateTime(2023, 11, 03), PaymentId=2, UserId=3, OrderTotal=314.25M},

    });
            modelBuilder.Entity<Item>().HasData(new Item[]
    {
        new Item { Id = 1, Name = "Iphone", Image="Test", Description="First Item in the store", Quantity=10, UserId=1, Price = 14.99M},
        new Item { Id = 2, Name = "Headset", Image="Test", Description="Second Item in the store", Quantity=12, UserId=2, Price = 40.99M},
        new Item { Id = 3, Name = "Camera", Image="Test", Description="Third Item in the store", Quantity=15, UserId=1, Price = 414.99M},

    });
            modelBuilder.Entity<Payment>().HasData(new Payment[]
    {
         new Payment {Id = 1, Type = "Credit Card"},
         new Payment {Id = 2, Type = "Cash"},

    });
            modelBuilder.Entity<Status>().HasData(new Status[]
    {
         new Status {Id = 1, StatusName = "Open"},
         new Status {Id = 2, StatusName = "Processing"},
         new Status {Id = 3, StatusName = "Shipped"},
         new Status {Id = 4, StatusName = "Complete"},



    });

        }
    }

}

