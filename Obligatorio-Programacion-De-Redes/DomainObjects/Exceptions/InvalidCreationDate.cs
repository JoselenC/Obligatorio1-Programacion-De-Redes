using System;

namespace DomainObjects.Exceptions
{
    public class InvalidCreationDate: Exception
    {
        public InvalidCreationDate() :
            base("Invalid format date")
        { }
    }
}