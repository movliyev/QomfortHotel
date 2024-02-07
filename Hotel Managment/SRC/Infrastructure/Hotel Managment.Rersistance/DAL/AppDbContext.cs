using Hotel_Managment.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Rersistance.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Servicee> Servicees { get; set; }
        public DbSet<RoomImage> RooImages { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<HomeAbout> HomeAbouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Employee> Employes { get; set; }
        public DbSet<Setting> Settings { get; set; }

    }
}
