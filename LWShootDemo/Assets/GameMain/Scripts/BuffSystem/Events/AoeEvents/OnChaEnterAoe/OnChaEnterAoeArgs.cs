using GameFramework;

namespace GameMain
{
    public class OnChaEnterAoeArgs : BaseAoeEventActArgs
    {
        public Character Character;
        
        public static OnChaEnterAoeArgs Create(Character character)
        {
            var args = ReferencePool.Acquire<OnChaEnterAoeArgs>();
            args.Character = character;
            return args;
        }
    }
}