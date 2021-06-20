using System;

namespace DomainObjects.Exceptions
{
    public class InvalidNameLength: Exception
    {
        public InvalidNameLength() :
            base("Invalid empty name")
        { }
    }
}