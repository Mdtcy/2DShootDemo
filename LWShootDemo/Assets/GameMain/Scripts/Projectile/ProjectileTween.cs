using GameFramework;

namespace GameMain
{
    public class ProjectileTween : IReference
    {
        public void Clear()
        {
        }

        public static ProjectileTween Create()
        {
            var projectileTween = ReferencePool.Acquire<ProjectileTween>();
            return projectileTween;
        }

        public void ReleaseToPool()
        {
            ReferencePool.Release(this);
        }
    }
}