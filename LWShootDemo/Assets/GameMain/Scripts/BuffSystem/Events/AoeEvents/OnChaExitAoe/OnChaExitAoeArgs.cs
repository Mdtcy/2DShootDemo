using GameFramework;

namespace GameMain
{
    public class OnChaExitAoeArgs : BaseAoeEventActArgs
    {
        public Character Character;
        
        public static OnChaExitAoeArgs Create(Character character)
        {
            var args = ReferencePool.Acquire<OnChaExitAoeArgs>();
            args.Character = character;
            return args;
        }
    }
}