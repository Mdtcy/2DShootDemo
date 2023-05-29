namespace LWShootDemo.BuffSystem.Events
{
    public class OnBeKilledArgs : DamageArgs
    {
        public OnBeKilledArgs(ref DamageInfo damageInfo) : base(ref damageInfo)
        {
        }
    }
}