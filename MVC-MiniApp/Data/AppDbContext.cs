using Microsoft.EntityFrameworkCore;
using MVC_MiniApp.Models;

namespace MVC_MiniApp.Data
{
    public class AppDbContext:DbContext
    {
        DbSet<Slider> Sliders {  get; set; }
        DbSet<SliderInfo> SliderInfos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
