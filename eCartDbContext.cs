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
        public DbSet<Category> Categories { get; set; }

        public eCartDbContext(DbContextOptions<eCartDbContext> context) : base(context)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data with Users
            modelBuilder.Entity<User>().HasData(new User[]
            {
        new User {Id = 1, Name = "Jack Smith", Address="123 Street", Phone="615-445-8855",Email="jack@yahoo.com", Uid="4d5256236asd6", isSeller = true},
        new User {Id = 2, Name = "Mike Daniel", Address="856 Road", Phone="615-645-2415",Email="Mdaniel@gmail.com", Uid="wLR4RTEAKyUr6K8w9ufj1IZM1hC3", isSeller = false},
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
        new Item { Id = 1, Name = "Iphone", Image="https://phones.mintmobile.com/wp-content/uploads/2023/09/Apple_iPhone_15_Pro_Titanium_Blue_PDP_Image.png", Description="First Item in the store", Quantity=10, UserId=1, Price = 14.99M, CategoryId=4},
        new Item { Id = 2, Name = "Headset", Image="https://pdp.com/cdn/shop/files/052-003_PS5_GAMBITWIRELESS_LEFTQUARTERSHOT.png?v=1696884972&width=640", Description="Second Item in the store", Quantity=12, UserId=2, Price = 40.99M, CategoryId=2},
        new Item { Id = 3, Name = "Camera", Image="https://i1.adis.ws/i/canon/15_eos_90d_bk_thefront_ef-s18-135mm_3.5-5.6isusm_square_6bd191e26825499fb5fe88e57a763f7a", Description="Third Item in the store", Quantity=15, UserId=1, Price = 414.99M, CategoryId=5},

    });
            modelBuilder.Entity<Payment>().HasData(new Payment[]
    {
         new Payment {Id = 1, Type = "Credit Card"},
         new Payment {Id = 2, Type = "Cash"},

    });
            modelBuilder.Entity<Category>().HasData(new Category[]
{
         new Category {Id = 1, CategoryName = "Laptops", CategoryImage="https://p2-ofp.static.pub/ShareResource/na/subseries/hero/lenovo-3i-chromebook-15-inch.png"},
         new Category {Id = 2, CategoryName = "Headphones", CategoryImage="https://i5.walmartimages.com/seo/Beats-Studio-Pro-Wireless-Headphones-Black_482fa5f9-4478-43a6-84ef-5abc2de71109.2d23bdd7708ce99a60e99d6a3750171c.png" },
         new Category {Id = 3, CategoryName = "Appliances", CategoryImage="https://d12mivgeuoigbq.cloudfront.net/magento-media/members/h0026-nicks-appliance/floating_appliances.png"},
         new Category {Id = 4, CategoryName = "Phones", CategoryImage="https://img.xfinitymobile.com/image/upload/c_fit,f_auto,q_auto,fl_lossy/v1696609940/client/v2/images/Apple-iPhone-15-Pro-BAU-Shop-Apple-LP-Placement/Shop_Banner_1280.png"},
         new Category {Id = 5, CategoryName = "Cameras", CategoryImage="https://img.bbystatic.com/BestBuy_US/store/ee/2017/cam/pr/sol-11219-nikon/sol-11219-nikon-d610-dslr-camera-rev.png"},

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

