namespace MVC_MiniApp.Models
{
    public class WorkImage : BaseEntity
    {
        public string Image {  get; set; }
        public int WorkId { get; set; }
        public Work Work { get; set; }
        public bool IsMain { get; set; }

    }
}
