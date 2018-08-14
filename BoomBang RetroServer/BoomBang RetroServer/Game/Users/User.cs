using BoomBang_RetroServer.Database;
using BoomBang_RetroServer.Game.Spaces.Areas;
using BoomBang_RetroServer.Game.Spaces.Salas;
using BoomBang_RetroServer.Sessions;
using BoomBang_RetroServer.Sockets.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Users
{
    public class User
    {
        public int ID;
        public string UserName;
        public string Password;
        public string EMail;
        public string MessageBox;
        public string ActualIP;
        public string Country;
        public string City;
        public string Colors;
        public string Hobby1;
        public string Hobby2;
        public string Hobby3;
        public string Wish1;
        public string Wish2;
        public string Wish3;
        public string Place = "Flower Power";
        public DateTime LastLogin;
        public int Hat = 0;
        public int Time;
        public int Age;
        public int Avatar;
        public int Votes1;
        public int Votes2;
        public int Votes3;
        public int GoldCoins;
        public int SilverCoins;
        public int SendedKisses;
        public int SendedDrinks;
        public int SendedFlowers;
        public int SendedUppercuts;
        public int SendedCoconuts;
        public int ReceivedKisses;
        public int ReceivedDrinks;
        public int ReceivedFlowers;
        public int ReceivedUppercuts;
        public int ReceivedCoconuts;
        public int SelectedUppercut;
        public int SelectedCoconut;
        public int UppercutLevel;
        public int CoconutLevel;
        public int Concursos;
        public int Packets;
        public DateTime Packetperiod;
        public bool NinjaLevel;
        public int PresentTime;

        public int ChestCount;

        public bool MOD;
        public bool VIP;
        public bool ReadedNews;

        public SpaceUser SpaceUser = null;
        public object SpaceInstance = null;

        public void AddGoldCoins(int Coins)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@Coins", Coins);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET GoldCoins = GoldCoins+@Coins WHERE ID = @ID LIMIT 1");
            }
            this.GoldCoins += Coins;
            if (SessionsManager.Online(ID))
            {
                SessionsManager.GetSession(ID).SendMessage(new ServerMessage(new byte[] { 162 }, new object[] { Coins }));
            }
        }
        public void RemoveGoldCoins(int Coins)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@Coins", Coins);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET GoldCoins = GoldCoins-@Coins WHERE ID = @ID LIMIT 1");
            }
            this.GoldCoins -= Coins;
            if (SessionsManager.Online(ID))
            {
                SessionsManager.GetSession(ID).SendMessage(new ServerMessage(new byte[] { 161 }, new object[] { Coins }));
            }
        }
        public void AddSilverCoins(int Coins)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@Coins", Coins);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SilverCoins = SilverCoins+@Coins WHERE ID = @ID LIMIT 1");
            }
            this.SilverCoins += Coins;
            if (SessionsManager.Online(ID))
            {
                SessionsManager.GetSession(ID).SendMessage(new ServerMessage(new byte[] { 166 }, new object[] { Coins }));
            }
        }
        public void RemoveSilverCoins(int Coins)
        {
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@Coins", Coins);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SilverCoins = SilverCoins-@Coins WHERE ID = @ID LIMIT 1");
            }
            this.SilverCoins -= Coins;
            if (SessionsManager.Online(ID))
            {
                SessionsManager.GetSession(ID).SendMessage(new ServerMessage(new byte[] { 168 }, new object[] { Coins }));
            }
        }
        public void UpdateMessageBox(string MessageBox)
        {
            this.MessageBox = MessageBox;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@MessageBox", MessageBox);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET Message = @MessageBox WHERE ID = @ID LIMIT 1");
            }
        }
        public void UpdateKisses(int SendedAmount = 0, int ReceivedAmount = 0)
        {
            this.ReceivedKisses += ReceivedAmount;
            this.SendedKisses += SendedAmount;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@SendedAmount", SendedAmount);
                DatabaseClient.SetParameter("@ReceivedAmount", ReceivedAmount);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SendedKisses = SendedKisses+@SendedAmount, ReceivedKisses = ReceivedKisses+@ReceivedAmount WHERE ID = @ID");
            }
            if (SessionsManager.Online(ID))
            {
                if (this.SpaceInstance != null)
                {
                    if (this.SpaceInstance is AreaInstance)
                    {
                        AreaInstance Area = (AreaInstance)this.SpaceInstance;
                        Area.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 1, this.SendedKisses, this.ReceivedKisses }));
                    }
                    if (this.SpaceInstance is SalaInstance)
                    {
                        SalaInstance Sala = (SalaInstance)this.SpaceInstance;
                        Sala.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 1, this.SendedKisses, this.ReceivedKisses }));
                    }
                }
            }
        }
        public void UpdateDrinks(int SendedAmount = 0, int ReceivedAmount = 0)
        {
            this.ReceivedDrinks += ReceivedAmount;
            this.SendedDrinks += SendedAmount;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@SendedAmount", SendedAmount);
                DatabaseClient.SetParameter("@ReceivedAmount", ReceivedAmount);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SendedDrinks = SendedDrinks+@SendedAmount, ReceivedDrinks = ReceivedDrinks+@ReceivedAmount WHERE ID = @ID");
            }
            if (SessionsManager.Online(ID))
            {
                if (this.SpaceInstance != null)
                {
                    if (this.SpaceInstance is AreaInstance)
                    {
                        AreaInstance Area = (AreaInstance)this.SpaceInstance;
                        Area.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 2, this.SendedDrinks, this.ReceivedDrinks }));
                    }
                    if (this.SpaceInstance is SalaInstance)
                    {
                        SalaInstance Sala = (SalaInstance)this.SpaceInstance;
                        Sala.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 2, this.SendedDrinks, this.ReceivedDrinks }));
                    }
                }
            }
        }
        public void UpdateFlowers(int SendedAmount = 0, int ReceivedAmount = 0)
        {
            this.ReceivedFlowers += ReceivedAmount;
            this.SendedFlowers += SendedAmount;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@SendedAmount", SendedAmount);
                DatabaseClient.SetParameter("@ReceivedAmount", ReceivedAmount);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SendedFlowers = SendedFlowers+@SendedAmount, ReceivedFlowers = ReceivedFlowers+@ReceivedAmount WHERE ID = @ID");
            }
            if (SessionsManager.Online(ID))
            {
                if (this.SpaceInstance != null)
                {
                    if (this.SpaceInstance is AreaInstance)
                    {
                        AreaInstance Area = (AreaInstance)this.SpaceInstance;
                        Area.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 3, this.SendedFlowers, this.ReceivedFlowers }));
                    }
                    if (this.SpaceInstance is SalaInstance)
                    {
                        SalaInstance Sala = (SalaInstance)this.SpaceInstance;
                        Sala.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 3, this.SendedFlowers, this.ReceivedFlowers }));
                    }
                }
            }
        }
        public void UpdateUppercuts(int SendedAmount = 0, int ReceivedAmount = 0)
        {
            this.ReceivedUppercuts += ReceivedAmount;
            this.SendedUppercuts += SendedAmount;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@SendedAmount", SendedAmount);
                DatabaseClient.SetParameter("@ReceivedAmount", ReceivedAmount);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SendedUppercuts = SendedUppercuts+@SendedAmount, ReceivedUppercuts = ReceivedUppercuts+@ReceivedAmount WHERE ID = @ID");
            }
            if (SessionsManager.Online(ID))
            {
                if (this.SpaceInstance != null)
                {
                    if (this.SpaceInstance is AreaInstance)
                    {
                        AreaInstance Area = (AreaInstance)this.SpaceInstance;
                        Area.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 4, this.SendedUppercuts, this.ReceivedUppercuts }));
                    }
                    if (this.SpaceInstance is SalaInstance)
                    {
                        SalaInstance Sala = (SalaInstance)this.SpaceInstance;
                        Sala.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 4, this.SendedUppercuts, this.ReceivedUppercuts }));
                    }
                }
            }
        }
        public void UpdateCoconuts(int SendedAmount = 0, int ReceivedAmount = 0)
        {
            this.ReceivedCoconuts += ReceivedAmount;
            this.SendedCoconuts += SendedAmount;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@SendedAmount", SendedAmount);
                DatabaseClient.SetParameter("@ReceivedAmount", ReceivedAmount);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET SendedCoconuts = SendedCoconuts+@SendedAmount, ReceivedCoconuts = ReceivedCoconuts+@ReceivedAmount WHERE ID = @ID");
            }
            if (SessionsManager.Online(ID))
            {
                if (this.SpaceInstance != null)
                {
                    if (this.SpaceInstance is AreaInstance)
                    {
                        AreaInstance Area = (AreaInstance)this.SpaceInstance;
                        Area.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 5, this.SendedCoconuts, this.ReceivedCoconuts }));
                    }
                    if (this.SpaceInstance is SalaInstance)
                    {
                        SalaInstance Sala = (SalaInstance)this.SpaceInstance;
                        Sala.SendToAll(new ServerMessage(new byte[] { 146 }, new object[] { this.ID, 5, this.SendedCoconuts, this.ReceivedCoconuts }));
                    }
                }
            }
        }
        public void Banº(DateTime Expiration, bool Permanent = false, string Reason = "Incumplir las Normas.")
        {
            if (!this.MOD)
            {
                using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
                {
                    DatabaseClient.ExecuteScalar("UPDATE boombang_users SET BanType = '" + ((Permanent) ? 0 : 1) + "', BanReason ? '" + Reason + "', BanExpiration = '" + Utils.Time.ToString(Expiration) + "' WHERE ID = '" + ID);
                }
                if (SessionsManager.Online(this.ID))
                {
                    Session Session = SessionsManager.GetSession(this.ID);
                    Session.SendMessage(new ServerMessage(new byte[] { 185 }, new object[] { new TimeSpan(Expiration.Ticks - DateTime.Now.Ticks).TotalSeconds, Reason }));
                    Session.End();
                }
            }
        }
        public void UpdateSecurityData(string IP)
        {
            this.ActualIP = IP;
            this.LastLogin = DateTime.Now;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@IP", this.ActualIP);
                DatabaseClient.SetParameter("@LastLogin", Utils.Time.ToString(this.LastLogin));
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET LastIP = @IP, LastLogin = @LastLogin WHERE ID = @ID LIMIT 1");
            }
        }
        public void UpdateSecurityData(int Avatar = 3, string Colors = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")
        {
            this.Avatar = Avatar;
            this.Colors = Colors;
            using (DatabaseClient DatabaseClient = DatabaseManager.GetClient())
            {
                DatabaseClient.SetParameter("@ID", this.ID);
                DatabaseClient.SetParameter("@Avatar", this.Avatar);
                DatabaseClient.SetParameter("@Colors", this.Colors);
                DatabaseClient.ExecuteScalar("UPDATE boombang_users SET Avatar = @Avatar, Colors = @Colors WHERE ID = @ID LIMIT 1");
            }
        }
    }
}
