using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BoomBang_RetroServer.Database;
using System.Net.Mail;
using System.Data;
using BoomBang_RetroServer.Utils;

namespace BoomBang_RetroServer.Game.Users
{
    class UserManager
    {
        private static bool CheckEMail(string EMail)
        {
            string Expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+\\.\\w+([-.]\\w+)";
            if (Regex.IsMatch(EMail, Expression))
            {
                if (Regex.Replace(EMail, Expression, String.Empty).Length == 0)
                { return true; }
                else
                { return false; }
            }
            else
            { return false; }
        }
        private static bool CheckUserName(string UserName)
        {
            bool Valid = true;
            string ValidCharacters = "1234567890qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM.,:-?!@=";
            for (int Pointer1 = 0; Pointer1 < UserName.Length; Pointer1++)
            {
                bool CharacterValid = false;
                for(int Pointer2 = 0; Pointer2<ValidCharacters.Length;Pointer2++)
                {
                    if(ValidCharacters[Pointer2] == UserName[Pointer1])
                    {
                        CharacterValid = true;
                    }
                }
                if(CharacterValid == false)
                {
                    Valid = false;
                }
            }
            if(Valid == true)
            {
                if(UserExists(UserName))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool EnviarMail(string Email, string UserName, int Code)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(Email);
            msg.From = new MailAddress("daloe@aquabang.es", "AquaBang Games", System.Text.Encoding.UTF8);
            msg.Subject = "Soporte AquaBang - Recuperar Contraseña";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "<div style='width: 100%; height: 100%; background: url('http://www.boombang.tv/images/client_bg.png') left top repeat-x; color: #fff; background-color:#09C";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("usuario", "gDfg8gG");
            client.Host = "webmail.starholy.com";
            client.EnableSsl = false;

            try
            {
                client.Send(msg);
                return true;
            }
            catch { }
            {
                return false;
            }
        }
        public static bool UserExists(string UserName)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@UserName", UserName);
                int result = Convert.ToInt32(DatabaseClient.ExecuteScalar("SELECT COUNT(*) FROM boombang_users WHERE UserName = @UserName"));
                return (result > 0) ? true : false;
            }
        }
        public static bool IsBanned(int ID)
        {
            if(GetBan(ID) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Ban GetBan(int ID)
        {
            using(DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", ID);
                DataRow UserRow = DatabaseClient.ExecuteScalarRow("SELECT BanType, BanReason, BanExpiration FROM boombang_users WHERE ID = @ID");
                if(Convert.ToInt32(UserRow["BanType"]) != 0)
                {
                    Ban Ban = new Ban();
                    Ban.Reason = Convert.ToString(UserRow["BanReason"]);
                    Ban.Time = Math.Round(new TimeSpan(Time.ToDateTime(Convert.ToString(UserRow["BanExpiration"])).Ticks = DateTime.Now.Ticks).TotalSeconds);
                    Ban.Type = Convert.ToInt32(UserRow["BanType"]);
                    if(Ban.Type == 2)
                    {
                        Ban.Time = 0;
                        return Ban;
                    }
                    else if(Ban.Type == 1)
                    {
                        if(Ban.Time <= 0)
                        {
                            Ban.Time = 0;
                            return null;
                        }
                        return Ban;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public static int CalculateMonths(DateTime Start, DateTime End)
        {
            Start = Start.Date;
            End = End.Date;
            int count = 0;
            while (Start < End)
            {
                Start = Start.AddMonths(1);
                count++;
            }
            return count;
        }
        public static bool Register(string UserName, string Password, int Avatar, string Colors, int Age, string EMail, string IP)
        {
            if(CheckUserName(UserName) && CheckEMail(EMail))
            {
                using(DatabaseClient DatabaseClient = DatabaseManager.GetClient())
                {
                    try
                    {

                        DatabaseClient.SetParameter("@UserName", UserName);
                        DatabaseClient.SetParameter("@Password", Convert.ToBase64String(SHA1.Create().ComputeHash(Utils.Encoding.StringToByteArray(Password))));
                        DatabaseClient.SetParameter("@Avatar", Avatar);
                        DatabaseClient.SetParameter("@Colors", Colors);
                        DatabaseClient.SetParameter("@Age", Age);
                        DatabaseClient.SetParameter("@EMail", EMail);
                        DatabaseClient.SetParameter("@IP", IP);
                        DatabaseClient.SetParameter("@Time", Time.ToString(DateTime.Now));
                        DatabaseClient.ExecuteScalar("INSERT INTO boombang_users (UserName, Passwoed, EMail, Age, RegisterIP, LastIP, Avatar, Colors, Register, LastLogin) VALUES (@UserName, @Password, @EMail, @Age, @IP, @IP, @Avatar, @Colors, @Time, @Time)");
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
        public static bool Register(string UserName, string Password)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@UserName", UserName);
                DatabaseClient.SetParameter("@Password", Convert.ToBase64String(SHA1.Create().ComputeHash(Utils.Encoding.StringToByteArray(Password))));
                int Result = Convert.ToInt32(DatabaseClient.ExecuteScalar("SELECT COUNT(*) FROM boombang_users WHERE UserName = @UserName AND Password = @Password LIMIT 1"));
                return (Result > 0) ? true : false;
            }
        }
        public static User GetUser(int ID)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", ID);
                DataRow UserRow = DatabaseClient.ExecuteScalarRow("SELECT * FROM boombang_users WHERE ID = @ID");

                User UserData = new User();
                //Converts, video 15:44 16-4-2014
            }
        }
          public static int SetTime(User User, string action)
        {
            int time = 0;
            switch(User.Avatar)
            {
                case 1:

                    if (action == "coco") { time = 3750; }
                    else if (action == "receive_upper") { time = 19937; }
                    break;
                case 2:
                    
                    if (action == "coco") { time = 3750; }
                    break;
            }
            return time;
        }
    }
}
