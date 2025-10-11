namespace MVC_MiniApp.ViewModels.Team
{
    public class TeamCreateVM
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public IFormFile UploadImage {  get; set; }
    }
}
