using Boombang.game.user;
using Boombang.game.bpad;
using Boombang.game.scenario;

namespace Boombang.game
{
    public class BoomBang
    {
        private userManager mUserManager;
        private bpadManager mFriendManager;
        private managerScenario mManagerScenario;

        public BoomBang()
        {
            mUserManager = new userManager();
            mFriendManager = new bpadManager();
            mManagerScenario = new managerScenario();
        }

        public userManager User
        {
            get {  return mUserManager; }
        }

        public bpadManager bpad
        {
            get { return mFriendManager; }
        }

        public managerScenario areas
        {
            get { return mManagerScenario; }
        }
    }
}
