namespace MVC_MiniApp.ViewModels.Contact
{
    public class ContactEditVM
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExistImage { get; set; }
        public IFormFile? UploadImage { get; set; }

    }
}
