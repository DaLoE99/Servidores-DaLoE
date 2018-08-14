namespace BoomBang.Game.Characters
{
    using BoomBang;
    using BoomBang.Storage;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class CharacterInfo
    {
        /* private scope */ bool bool_0;
        /* private scope */ double double_0;
        /* private scope */ double double_1;
        /* private scope */ double double_2;
        internal int int_0;
        internal int int_1;
        /* private scope */ int int_10;
        internal int int_11;
        /* private scope */ int int_12;
        /* private scope */ int int_13;
        /* private scope */ int int_14;
        /* private scope */ int int_2;
        /* private scope */ int int_3;
        /* private scope */ int int_4;
        /* private scope */ int int_5;
        internal int int_6;
        /* private scope */ int int_7;
        /* private scope */ int int_8;
        /* private scope */ int int_9;
        /* private scope */ string string_0;
        /* private scope */ string string_1;
        internal string string_10;
        /* private scope */ string string_2;
        internal string string_3;
        /* private scope */ string string_4;
        internal string string_5;
        internal string string_6;
        internal string string_7;
        internal string string_8;
        internal string string_9;
        internal uint uint_0;
        internal uint uint_1;
        /* private scope */ uint uint_10;
        /* private scope */ uint uint_11;
        /* private scope */ uint uint_12;
        internal uint uint_2;
        /* private scope */ uint uint_3;
        /* private scope */ uint uint_4;
        /* private scope */
        /* private scope */ uint uint_5;
        /* private scope */ uint uint_6;
        /* private scope */ uint uint_7;
        internal uint uint_8;
        internal uint uint_9;

        public CharacterInfo(SqlDatabaseClient MySqlClient, uint uint_13, uint SessionId, string Username, string Email, string Motto, uint AvatarType, string AvatarColors, string City, uint Age, uint Staff, uint Vip, int GoldCoins, int SilverCoins, int SentKisses, int SentCocktails, int SentRoses, int SentCoconuts, int SentUppercuts, int ReceivedKisses, int ReceivedCocktails, int ReceivedRoses, int ReceivedCoconuts, int ReceivedUppercuts, string Hobby1, string Hobby2, string Hobby3, string Wish1, string Wish2, string Wish3, uint GreenVotes, uint BlueVotes, uint OrangeVotes, uint MonthsRegistered, uint WonRings, uint WonCoconuts, uint RingLevel, uint CocoLevel, uint NinjaLevel, uint AllowChanges, double TimestampLastOnline, double ActivityPointsLastUpdate)
        {
            this.uint_1 = SessionId;
            this.uint_0 = uint_13;
            this.string_0 = Username;
            this.string_1 = Email;
            this.string_2 = Motto;
            this.uint_2 = AvatarType;
            this.string_3 = AvatarColors;
            this.string_4 = City;
            this.uint_3 = Age;
            this.uint_4 = Staff;
            this.uint_5 = Vip;
            this.uint_6 = AllowChanges;
            this.int_0 = GoldCoins;
            this.int_1 = SilverCoins;
            this.int_2 = SentKisses;
            this.int_3 = SentCocktails;
            this.int_4 = SentRoses;
            this.int_5 = SentCoconuts;
            this.int_6 = SentUppercuts;
            this.int_7 = ReceivedKisses;
            this.int_8 = ReceivedCocktails;
            this.int_9 = ReceivedRoses;
            this.int_10 = ReceivedCoconuts;
            this.int_11 = ReceivedUppercuts;
            this.string_5 = Hobby1;
            this.string_6 = Hobby2;
            this.string_7 = Hobby3;
            this.string_8 = Wish1;
            this.string_9 = Wish2;
            this.string_10 = Wish3;
            this.int_12 = (int) GreenVotes;
            this.int_13 = (int) BlueVotes;
            this.int_14 = (int) OrangeVotes;
            this.uint_7 = MonthsRegistered;
            this.uint_8 = WonRings;
            this.uint_9 = WonCoconuts;
            this.uint_10 = RingLevel;
            this.uint_11 = CocoLevel;
            this.uint_12 = NinjaLevel;
            this.bool_0 = false;
            this.double_0 = UnixTimestamp.GetCurrent();
            this.double_2 = 900.0;
            this.double_1 = TimestampLastOnline;
        }

        public void SynchronizeStatistics(SqlDatabaseClient MySqlClient)
        {
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("Timestamp", UnixTimestamp.GetCurrent());
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET ultimo_login = @Timestamp WHERE id = @id LIMIT 1");
        }

        public void UpdateAvatar(SqlDatabaseClient MySqlClient, uint Type, string Colors)
        {
            if (string.IsNullOrEmpty(Colors) && !Type.Equals(null))
            {
                this.uint_2 = Type;
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("AvatarType", this.uint_2);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET tipo_avatar = @AvatarType WHERE id = @id LIMIT 1");
            }
            else if (!string.IsNullOrEmpty(Colors) && Type.Equals(null))
            {
                this.string_3 = Colors;
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("AvatarColors", this.string_3);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET colores_avatar = @AvatarColors WHERE id = @id LIMIT 1");
            }
            else if (!string.IsNullOrEmpty(Colors) && !Type.Equals(null))
            {
                this.uint_2 = Type;
                this.string_3 = Colors;
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("AvatarType", this.uint_2);
                MySqlClient.SetParameter("AvatarColors", this.string_3);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET colores_avatar = @AvatarColors, tipo_avatar = @AvatarType WHERE id = @id LIMIT 1");
            }
        }

        public void UpdateCocktails(SqlDatabaseClient MySqlClient, bool Sent)
        {
            if (Sent)
            {
                this.int_3++;
            }
            else
            {
                this.int_8++;
            }
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("SentCocktails", this.int_3);
            MySqlClient.SetParameter("ReceivedCocktails", this.int_8);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET cocteles_enviados = @SentCocktails, cocteles_recibidos = @ReceivedCocktails WHERE id = @id LIMIT 1");
        }

        public void UpdateCoconuts(SqlDatabaseClient MySqlClient, bool Sent)
        {
            if (Sent)
            {
                this.int_5++;
            }
            else
            {
                this.int_10++;
            }
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("SentCoconuts", this.int_5);
            MySqlClient.SetParameter("ReceivedCoconuts", this.int_10);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET cocos_enviados = @SentCoconuts, cocos_recibidos = @ReceivedCoconuts WHERE id = @id LIMIT 1");
        }

        public void UpdateGames(SqlDatabaseClient MySqlClient, bool WonRing = false, bool WonCoconut = false, bool WonShuriken = false, bool LoseShuriken = false)
        {
            if (WonRing)
            {
                this.uint_8++;
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("WonRings", this.uint_8);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET rings_ganados = @WonRings WHERE id = @id LIMIT 1");
            }
            else if (WonCoconut)
            {
                this.uint_9++;
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("WonCoconuts", this.uint_9);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET cocos_ganados = @WonCoconuts WHERE id = @id LIMIT 1");
            }
            else if (WonShuriken)
            {
                if (!LoseShuriken)
                {
                    this.uint_12 += 5;
                }
                else
                {
                    this.uint_12 += 2;
                }
                MySqlClient.SetParameter("id", this.uint_0);
                MySqlClient.SetParameter("NinjaPoints", this.uint_12);
                MySqlClient.ExecuteNonQuery("UPDATE usuarios SET nivel_ninja = @NinjaPoints WHERE id = @id LIMIT 1");
            }
        }

        public void UpdateGoldCreditsBalance(SqlDatabaseClient MySqlClient, int Amount)
        {
            this.int_0 += Amount;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("credits", int_0);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET creditos_oro = @credits WHERE id = @id LIMIT 1");
        }

        public void UpdateGoldCreditsBalance2(SqlDatabaseClient MySqlClient, uint ID)
        {
            
            MySqlClient.SetParameter("id", ID);
            MySqlClient.SetParameter("credits", int_0);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET creditos_oro = @credits WHERE id = @id LIMIT 1");
        }




        public void UpdateHobbies(SqlDatabaseClient MySqlClient, uint LabelId, string mHobby)
        {
            switch (LabelId)
            {
                case 1:
                    this.string_5 = mHobby;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Hobby", this.string_5);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET hobby_1 = @Hobby WHERE id = @id LIMIT 1");
                    return;

                case 2:
                    this.string_6 = mHobby;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Hobby", this.string_6);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET hobby_2 = @Hobby WHERE id = @id LIMIT 1");
                    return;

                case 3:
                    this.string_7 = mHobby;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Hobby", this.string_7);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET hobby_3 = @Hobby WHERE id = @id LIMIT 1");
                    return;
            }
            this.string_5 = mHobby;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("Hobby", this.string_5);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET hobby_1 = @Hobby WHERE id = @id LIMIT 1");
        }

        public void UpdateKisses(SqlDatabaseClient MySqlClient, bool Sent)
        {
            if (Sent)
            {
                this.int_2++;
            }
            else
            {
                this.int_7++;
            }
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("SentKisses", this.int_2);
            MySqlClient.SetParameter("ReceivedKisses", this.int_7);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET besos_enviados = @SentKisses, besos_recibidos = @ReceivedKisses WHERE id = @id LIMIT 1");
        }

        public void UpdateMotto(SqlDatabaseClient MySqlClient, string Motto)
        {
            this.string_2 = Motto;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("Motto", Motto);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET bocadillo = @Motto WHERE id = @id LIMIT 1");
        }

        public void UpdateRoses(SqlDatabaseClient MySqlClient, bool Sent)
        {
            if (Sent)
            {
                this.int_4++;
            }
            else
            {
                this.int_9++;
            }
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("SentRoses", this.int_4);
            MySqlClient.SetParameter("ReceivedRoses", this.int_9);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET flores_enviadas = @SentRoses, flores_recibidas = @ReceivedRoses WHERE id = @id LIMIT 1");
        }

        public void UpdateSilverCreditsBalance(SqlDatabaseClient MySqlClient, int Amount)
        {
            this.int_1 += Amount;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("credits", this.int_1);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET creditos_plata = @credits WHERE id = @id LIMIT 1");
        }

        public void UpdateUppercuts(SqlDatabaseClient MySqlClient, bool Sent)
        {
            if (Sent)
            {
                this.int_6++;
            }
            else
            {
                this.int_11++;
            }
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("SentUppercuts", this.int_6);
            MySqlClient.SetParameter("ReceivedUppercuts", this.int_11);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET uppercuts_enviados = @SentUppercuts, uppercuts_recibidos = @ReceivedUppercuts WHERE id = @id LIMIT 1");
        }

        public void UpdateVotes(SqlDatabaseClient MySqlClient, uint ColorId, int Vote)
        {
            switch (ColorId)
            {
                case 1:
                    this.int_12 += Vote;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("GreenVotes", this.int_12);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET votos_legal = @GreenVotes WHERE id = @id LIMIT 1");
                    return;

                case 2:
                    this.int_13 += Vote;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("BlueVotes", this.int_13);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET votos_sexy = @BlueVotes WHERE id = @id LIMIT 1");
                    return;

                case 3:
                    this.int_14 += Vote;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("OrangeVotes", this.int_14);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET votos_simpatico = @OrangeVotes WHERE id = @id LIMIT 1");
                    return;
            }
            this.int_12 += Vote;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("GreenVotes", this.int_12);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET votos_legal = @GreenVotes WHERE id = @id LIMIT 1");
        }

        public void UpdateWishes(SqlDatabaseClient MySqlClient, uint LabelId, string mWish)
        {
            switch (LabelId)
            {
                case 1:
                    this.string_8 = mWish;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Wish", this.string_8);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET deseo_1 = @Wish WHERE id = @id LIMIT 1");
                    return;

                case 2:
                    this.string_9 = mWish;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Wish", this.string_9);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET deseo_2 = @Wish WHERE id = @id LIMIT 1");
                    return;

                case 3:
                    this.string_10 = mWish;
                    MySqlClient.SetParameter("id", this.uint_0);
                    MySqlClient.SetParameter("Wish", this.string_10);
                    MySqlClient.ExecuteNonQuery("UPDATE usuarios SET deseo_3 = @Wish WHERE id = @id LIMIT 1");
                    return;
            }
            this.string_8 = mWish;
            MySqlClient.SetParameter("id", this.uint_0);
            MySqlClient.SetParameter("Wish", this.string_8);
            MySqlClient.ExecuteNonQuery("UPDATE usuarios SET deseo_1 = @Wish WHERE id = @id LIMIT 1");
        }

        public uint Age
        {
            get
            {
                return this.uint_3;
            }
        }

        public uint AllowChanges
        {
            get
            {
                return this.uint_6;
            }
            set
            {
                this.uint_6 = value;
            }
        }

        public string AvatarColors
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public uint AvatarType
        {
            get
            {
                return this.uint_2;
            }
            set
            {
                this.uint_2 = value;
            }
        }

        public int BlueVotes
        {
            get
            {
                return this.int_13;
            }
        }

        public double CacheAge
        {
            get
            {
                return (UnixTimestamp.GetCurrent() - this.double_0);
            }
        }

        public string City
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public uint CocoLevel
        {
            get
            {
                return this.uint_11;
            }
            set
            {
                this.uint_11 = value;
            }
        }

        public string Email
        {
            get
            {
                return this.string_1;
            }
        }

        public int GoldCoins
        {
            get
            {
                return this.int_0;
            }
            set
            {
                this.int_0 = value;
            }
        }

        public int GreenVotes
        {
            get
            {
                return this.int_12;
            }
        }

        public bool HasLinkedSession
        {
            get
            {
                return (this.uint_1 > 0);
            }
        }

        public string Hobby1
        {
            get
            {
                return this.string_5;
            }
            set
            {
                this.string_5 = value;
            }
        }

        public string Hobby2
        {
            get
            {
                return this.string_6;
            }
            set
            {
                this.string_6 = value;
            }
        }

        public string Hobby3
        {
            get
            {
                return this.string_7;
            }
            set
            {
                this.string_7 = value;
            }
        }

        public bool IsMuted
        {
            get
            {
                return this.bool_0;
            }
        }

        public uint MonthsRegistered
        {
            get
            {
                return this.uint_7;
            }
        }

        public string Motto
        {
            get
            {
                return this.string_2;
            }
        }

        public uint NinjaLevel
        {
            get
            {
                return this.uint_12;
            }
        }

        public int OrangeVotes
        {
            get
            {
                return this.int_14;
            }
        }

        public int ReceivedCocktails
        {
            get
            {
                return this.int_8;
            }
            set
            {
                this.int_8 = value;
            }
        }

        public int ReceivedCoconuts
        {
            get
            {
                return this.int_10;
            }
            set
            {
                this.int_10 = value;
            }
        }

        public int ReceivedKisses
        {
            get
            {
                return this.int_7;
            }
            set
            {
                this.int_7 = value;
            }
        }

        public int ReceivedRoses
        {
            get
            {
                return this.int_9;
            }
            set
            {
                this.int_9 = value;
            }
        }

        public int ReceivedUppercuts
        {
            get
            {
                return this.int_11;
            }
            set
            {
                this.int_11 = value;
            }
        }

        public uint RingLevel
        {
            get
            {
                return this.uint_10;
            }
            set
            {
                this.uint_10 = value;
            }
        }

        public int SentCocktails
        {
            get
            {
                return this.int_3;
            }
            set
            {
                this.int_3 = value;
            }
        }

        public int SentCoconuts
        {
            get
            {
                return this.int_5;
            }
            set
            {
                this.int_5 = value;
            }
        }

        public int SentKisses
        {
            get
            {
                return this.int_2;
            }
            set
            {
                this.int_2 = value;
            }
        }

        public int SentRoses
        {
            get
            {
                return this.int_4;
            }
            set
            {
                this.int_4 = value;
            }
        }

        public int SentUppercuts
        {
            get
            {
                return this.int_6;
            }
            set
            {
                this.int_6 = value;
            }
        }

        public uint SessionId
        {
            get
            {
                return this.uint_1;
            }
        }

        public int SilverCoins
        {
            get
            {
                return this.int_1;
            }
            set
            {
                this.int_1 = value;
            }
        }

        public uint Staff
        {
            get
            {
                return this.uint_4;
            }
        }


       

        public double TimeSinceLastActivityPointsUpdate
        {
            get
            {
                return this.double_2;
            }
            set
            {
                this.double_2 = value;
            }
        }

        public double TimestampLastOnline
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        public uint UInt32_0
        {
            get
            {
                return this.uint_0;
            }
        }

        public string Username
        {
            get
            {
                return this.string_0;
            }
        }

        public uint Vip
        {
            get
            {
                return this.uint_5;
            }
            set
            {
                this.uint_5 = value;
            }
        }

        public string Wish1
        {
            get
            {
                return this.string_8;
            }
            set
            {
                this.string_8 = value;
            }
        }

        public string Wish2
        {
            get
            {
                return this.string_9;
            }
            set
            {
                this.string_9 = value;
            }
        }

        public string Wish3
        {
            get
            {
                return this.string_10;
            }
            set
            {
                this.string_10 = value;
            }
        }

        public uint WonCoconuts
        {
            get
            {
                return this.uint_9;
            }
        }

        public uint WonRings
        {
            get
            {
                return this.uint_8;
            }
        }
    }
}

