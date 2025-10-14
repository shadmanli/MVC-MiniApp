using MVC_MiniApp.Models;

namespace MVC_MiniApp.ViewModels.Work
{
    public class WorkDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }
        public List<WorkImageVM> Images { get; set; }
    }
}
