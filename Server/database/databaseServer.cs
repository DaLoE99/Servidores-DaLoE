using System;

namespace Boombang.database
{
    public class DatabaseServer
    {
        private readonly string mHost;
        private readonly uint mPort;

        private readonly string mUser;
        private readonly string mPassword;

        public string Host
        {
            get { return mHost; }
        }

        public uint Port
        {
            get { return mPort; }
        }

        public string User
        {
            get { return mUser; }
        }

        public string Password
        {
            get { return mPassword; }
        }

        public DatabaseServer(string sHost, uint Port, string sUser, string sPassword)
        {
            if (sHost == null || sHost.Length == 0)
                throw new ArgumentException("sHost");
            if (sUser == null || sUser.Length == 0)
                throw new ArgumentException("sUser");

            mHost = sHost;
            mPort = Port;

            mUser = sUser;
            mPassword = (sPassword != null) ? sPassword : "";
        }

        public override string ToString()
        {
            return mUser + "@" + mHost;
        }
    }
}
