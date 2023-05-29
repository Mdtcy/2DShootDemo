namespace LWShootDemo.BuffSystem.Events
{
    public class DamageArgs : BuffArgs
    {
        public DamageInfo DamageInfo;

        public DamageArgs(ref DamageInfo damageInfo)
        {
            DamageInfo = damageInfo;
        }
    }
}