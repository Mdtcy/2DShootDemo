using UnityEngine;

namespace GameMain
{
    [CreateAssetMenu]
    public class ProjectileLauncherTable : SOTableList<ProjectileLauncherProp>
    {
        public override int StartId => IdUtility.ProjectileLauncherStartId();
    }
}