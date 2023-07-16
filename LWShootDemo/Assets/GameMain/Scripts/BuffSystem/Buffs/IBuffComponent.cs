namespace LWShootDemo.BuffSystem.Buffs
{
    public interface IBuffComponent 
    {
        public void Update(float elapseSeconds, float realElapseSeconds);
    }
}