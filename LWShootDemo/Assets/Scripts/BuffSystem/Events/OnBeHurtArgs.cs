namespace LWShootDemo.BuffSystem.Events
{
    public class OnBeHurtArgs : DamageArgs
    {
        public OnBeHurtArgs(ref DamageInfo damageInfo) : base(ref damageInfo)
        {
        }
    }
}