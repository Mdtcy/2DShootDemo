namespace LWShootDemo.BuffSystem.Events
{
    public class OnKillArgs : DamageArgs
    {
        public OnKillArgs(ref DamageInfo damageInfo) : base(ref damageInfo)
        {
        }
    }
}