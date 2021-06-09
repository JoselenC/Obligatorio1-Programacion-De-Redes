using System.Collections.Generic;

namespace Domain
{
    public class Theme
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> Posts { get; set; }
        public int Id { get; set; }
        
        public override bool Equals(object? obj)
        {
            return ((Theme) obj).Name == Name;
        }
    }
}