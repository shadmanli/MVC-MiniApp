namespace MVC_MiniApp.ViewModels.Slider
{
    public class SliderUIVM
    {
        public string Image { get; set; }
        public List<SliderInfoUIVM> Infos { get; set; } = new();
    }
}
