using UnityEngine;

namespace GameMain
{
    [CreateAssetMenu]
    public class ProjectileTable : SOTableList<ProjectileProp>
    {
        public override int StartId => IdUtility.ProjectileStartId();
    }
}