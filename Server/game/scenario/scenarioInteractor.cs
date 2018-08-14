using System;
using System.Collections.Generic;
using System.Threading;

using Boombang.utils;

namespace Boombang.game.scenario
{
    public class scenarioInteractor
    {
        internal scenarioInstance mScenarioInstance;
        private Thread mThread;
        private const int FrameTime = 750;

        public scenarioInteractor(int scenario_id, scenarioInstance sInstance)
        {
            mScenarioInstance = sInstance;
            if (!mScenarioInstance.scenarioInfo.es_publica)
            {
                //mSandObjetos = Environment.Game.Objetos.GetIslandObjetosSand(scenario_id);
                //mWaterObjetos = Environment.Game.Objetos.GetIslandObjetosWater(scenario_id);
            }

            mThread = new Thread(scenarioWorker);
            mThread.Start();
        }

        void scenarioWorker()
        {
            while (true)
            {
                try
                {
                    frameTime.Start();
                    double numberMilli = frameTime.GetTime();
                    if (numberMilli < FrameTime) Thread.Sleep(FrameTime - (int)numberMilli);
                }
                catch (ThreadAbortException)
                {
                    Console.WriteLine("[DEBUG] Area destruida.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("[ERROR] Ha ocurrido un error en el thread de las areas. Stack trace: " + e.ToString());
                }
            }
        }

        public void destroyInteractor()
        {
            mThread.Abort();
            mThread = null;
        }
    }
}
