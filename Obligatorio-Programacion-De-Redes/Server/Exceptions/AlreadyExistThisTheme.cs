using System;

namespace Server.Exceptions
{
    public class AlreadyExistThisTheme: Exception
    {
        public AlreadyExistThisTheme() :
            base("This theme already exist")
        {
        }
    }
}