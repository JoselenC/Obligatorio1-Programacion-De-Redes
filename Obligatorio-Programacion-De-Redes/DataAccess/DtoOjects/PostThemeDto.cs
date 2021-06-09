namespace DataAccess.DtoOjects
{
    public class PostThemeDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        
        public PostDto Post { get; set; }
        
        public int ThemeId { get; set; }
        
        public ThemeDto Theme { get; set; }
        
    }
}