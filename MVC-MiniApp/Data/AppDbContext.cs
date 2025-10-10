using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Models;

namespace MVC_MiniApp.Data
{
    public class AppDbContext:DbContext
    {
      public DbSet<Slider> Sliders {  get; set; }
     public   DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkImage> workImages { get; set; }
        public DbSet<Recent> Recents { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
