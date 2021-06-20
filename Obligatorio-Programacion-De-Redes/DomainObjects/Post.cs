using System.Collections.Generic;
using System.Text.RegularExpressions;
using DomainObjects.Exceptions;

namespace DomainObjects
{
    public class Post
    {
        public List<Theme>  Themes { get; set; }
        public string CreationDate { get; set; }
        public File File { get; set; }
        public int Id { get; set; }
        
        public string ThemeName { get; set; }

        public bool SameName(string name)
        {
            return Name == name;
        }
        
        public override bool Equals(object? obj)
        {
            return ((Post) obj).Name == Name;
        }
        
        public string Name {get; set; }
        public void ValidateName(string vName)
        {
            if (vName.Length<1)
                throw new InvalidNameLength();
        }
        
        public void ValidateCreationDate(string date)
        {
            bool goodFormat = false;
            Regex regex = new Regex(@"\b\d{2}(/|-|.|\s)\d{2}(/|-|.|\s)(\d{4})");
            var match = regex.Match(date);
            if (!match.Success)
                throw new InvalidCreationDate();
        }
        
    }
}