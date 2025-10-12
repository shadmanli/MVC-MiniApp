namespace MVC_MiniApp.ViewModels.Contact
{
    public class ContactCreateVM
    {
        public string Title { get; set; }
        public IFormFile UploadImage { get; set; }
        public string Description { get; set; }
    }
}
