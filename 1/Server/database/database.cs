using System;

namespace Boombang.database
{
    public class Database
    {
        private readonly string mName;
        private readonly uint mMinPoolSize;
        private readonly uint mMaxPoolSize;

        public string Name
        {
            get { return mName; }
        }

        public uint minPoolSize
        {
            get { return mMinPoolSize; }
        }

        public uint maxPoolSize
        {
            get { return mMaxPoolSize; }
        }

        public Database(string sName, uint minPoolSize, uint maxPoolSize)
        {
            if (sName == null || sName.Length == 0)
                throw new ArgumentException(sName);

            mName = sName;
            mMinPoolSize = minPoolSize;
            mMaxPoolSize = maxPoolSize;
        }
    }
}