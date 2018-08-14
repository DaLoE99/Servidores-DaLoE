using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoomBang_RetroServer.Game.Spaces
{
    class PathFinding
    {
        public void Start()//24 min: 00;00
        {
            int[] NewX = GetX(Data);
            int[] NewY = GetY(Data);
            int[] NewPositions = GetPositions(Data);
            try
            {
                X = NewX;
                Y = NewY;
                Position = NewPositions;
                if (WalkThread != null)
                {
                    if (!WalkThread.IsAlive)
                    {
                        WalkThread = new Thread(new ThreadStart(Start));
                        try
                        {
                            WalkThread.Start();
                        }
                        catch (Exception ex)
                        {
                            Output.WriteLine(ex.ToString());
                            WalkThread.Abort();
                        }
                    }
                }
                else
                {
                    WalkThread = new Thread(new ThreadStart(Start));
                    try
                    {
                        WalkThread.Start();
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(ex.ToString());
                        WalkThread.Abort();
                    }
                }
            }
            catch { }

        }
    }
}