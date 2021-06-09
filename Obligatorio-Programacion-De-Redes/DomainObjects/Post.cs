using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public string Name { get; set; }
        public List<Theme>  Themes { get; set; }
        public string CreationDate { get; set; }
        public File File { get; set; }
        public int Id { get; set; }

        public bool SameName(string name)
        {
            return Name == name;
        }
        
        public override bool Equals(object? obj)
        {
            return ((Post) obj).Name == Name;
        }
    }
}