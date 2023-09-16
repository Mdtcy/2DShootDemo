using NodeCanvas.Framework;

namespace GameMain
{
    public class Cond_IsDead : ConditionTask
    {
        protected override bool OnCheck()
        {
            return agent.GetComponent<Character>().IsDead;
        }
    }
}