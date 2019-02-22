using System;

namespace Dev.EntityFrameworkCore.MongoDb.Exceptions
{
    public class DbContextOptionsException : Exception
    {
        public DbContextOptionsException(string message) : base(message) { }
    }
}
