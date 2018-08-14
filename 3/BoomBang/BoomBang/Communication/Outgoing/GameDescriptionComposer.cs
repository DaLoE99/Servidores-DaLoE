namespace BoomBang.Communication.Outgoing
{
    using BoomBang.Communication;
    using System;

    public static class GameDescriptionComposer
    {
        public static ServerMessage Compose(uint GameId)
        {
            ServerMessage message = new ServerMessage(FlagcodesOut.GAMES, ItemcodesOut.GAME_DESC, false);
            switch (GameId)
            {
                case 1:
                    message.AppendParameter(0, false);
                    message.AppendParameter("-1", false);
                    message.AppendParameter(2, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n100 cr\x00e9ditos y 1 Victoria.", false);
                    message.AppendParameter(100, false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(3, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n10 monedas de plata.", false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(1, false);
                    return message;

                case 2:
                    message.AppendParameter(0, false);
                    message.AppendParameter("-1", false);
                    message.AppendParameter(2, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n100 cr\x00e9ditos y 1 Victoria.", false);
                    message.AppendParameter(100, false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(3, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n10 monedas de plata.", false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(1, false);
                    return message;

                case 3:
                    message.AppendParameter(0, false);
                    message.AppendParameter("-1", false);
                    message.AppendParameter(2, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n100 cr\x00e9ditos y 1 Victoria.", false);
                    message.AppendParameter(100, false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(3, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n10 monedas de plata.", false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(1, false);
                    return message;

                case 4:
                    message.AppendParameter(0, false);
                    message.AppendParameter("-1", false);
                    message.AppendParameter(2, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n100 cr\x00e9ditos y 1 Victoria.", false);
                    message.AppendParameter(100, false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(3, false);
                    message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n10 monedas de plata.", false);
                    message.AppendParameter(0, false);
                    message.AppendParameter(1, false);
                    return message;
            }
            message.AppendParameter(0, false);
            message.AppendParameter("-1", false);
            message.AppendParameter(2, false);
            message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n100 cr\x00e9ditos y 1 Victoria.", false);
            message.AppendParameter(100, false);
            message.AppendParameter(0, false);
            message.AppendParameter(3, false);
            message.AppendParameter("Si ganas, no se te cobrar\x00e1 la partida y conseguir\x00e1s:\n10 monedas de plata.", false);
            message.AppendParameter(0, false);
            message.AppendParameter(1, false);
            return message;
        }
    }
}

