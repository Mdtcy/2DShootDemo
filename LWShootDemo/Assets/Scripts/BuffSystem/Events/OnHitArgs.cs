namespace LWShootDemo.BuffSystem.Events
{
    public class OnHitArgs : DamageArgs
    {
        public OnHitArgs(ref DamageInfo damageInfo) : base(ref damageInfo)
        {
        }
    }
}