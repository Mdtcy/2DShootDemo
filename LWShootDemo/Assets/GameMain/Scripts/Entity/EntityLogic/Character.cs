using Animancer;

namespace GameMain
{
    public class Character : EntityLogicBase
    {
        public AnimancerComponent CachedAnimancer { get; private set; }
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            CachedAnimancer = GetComponent<AnimancerComponent>();
        }
    }
}