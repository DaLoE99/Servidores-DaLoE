namespace BoomBang.Communication.Incoming
{
    using BoomBang.Communication;
    using BoomBang.Game.Sessions;
    using System;
    using System.Runtime.CompilerServices;

    public delegate void ProcessRequestCallback(Session Client, ClientMessage Message);
}

