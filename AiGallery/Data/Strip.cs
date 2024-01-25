namespace AiGallery.Data
{
    public class Strip
    {
        public int ImageStripId { get; set; }
        public string Title_Eng { get; set; }
        public string Title_Ita { get; set; }
        public string Path { get; set; }
        public int ViewsCounter { get; set; }
        public DateTime LastView { get; set; }
    }
    
}
