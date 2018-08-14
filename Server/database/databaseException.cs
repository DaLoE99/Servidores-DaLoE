using System;

namespace Boombang.database
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string sMessage) : base(sMessage) { }
    }
}
