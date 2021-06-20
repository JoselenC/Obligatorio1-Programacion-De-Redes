using System.Collections.Generic;
using DomainObjects.Exceptions;

namespace DomainObjects
{
    public class Post
    {
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
        
        public string Name {get; set; }
        public void SetName(string vName)
        {
            if (vName.Length<1)
                throw new InvalidNameLength();
        }
    }
}