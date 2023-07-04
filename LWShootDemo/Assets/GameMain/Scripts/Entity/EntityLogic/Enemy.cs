using Animancer;

namespace GameMain
{
    public class Enemy : EntityLogicBase
    {
        public AnimancerComponent CachedAnimancer { get; private set; }
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            CachedAnimancer = GetComponent<AnimancerComponent>();
        }
    }
}