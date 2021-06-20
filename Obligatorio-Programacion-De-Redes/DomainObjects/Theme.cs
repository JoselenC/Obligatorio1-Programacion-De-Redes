using System.Collections.Generic;
using DomainObjects.Exceptions;

namespace DomainObjects
{
    public class Theme
    {
       public string Name {get; set; }

       public void ValidateName(string vName)
       {
           if (vName.Length <1)
               throw new InvalidNameLength();
       }

       public string Description { get; set; }
        public List<Post> Posts { get; set; }
        public int Id { get; set; }
        
        public override bool Equals(object? obj)
        {
            return ((Theme) obj).Name == Name;
        }
    }
}