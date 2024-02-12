using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QomfortHotelFinal.Models;

namespace QomfortHotelFinal.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Servicee> Servisees { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<HomeAbout> HomeAbouts { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }

    }
}
