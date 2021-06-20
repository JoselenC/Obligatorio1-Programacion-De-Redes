using System;

namespace Server.Exceptions
{
    public class AlreadyExistThisPost : Exception
    {
        public AlreadyExistThisPost() :
            base("This post already exist")
        {
        }
    }
}