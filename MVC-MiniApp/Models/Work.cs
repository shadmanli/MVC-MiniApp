namespace MVC_MiniApp.Models
{
    public class Work:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
        public ICollection<WorkImage> Images { get; set; }

    }
}
