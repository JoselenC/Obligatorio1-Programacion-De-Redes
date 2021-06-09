namespace DataAccess.DtoOjects
{
    public class FileThemeDto
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        
        public FileDto File { get; set; }
        
        public int ThemeId { get; set; }
        
        public ThemeDto Theme { get; set; }
    }
}