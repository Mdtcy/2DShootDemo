namespace GameMain
{
    public class ProjectileLauncherTable : SOTableList<ProjectileLauncherProp>
    {
        public override int StartId => IdUtility.ProjectileLauncherStartId();
    }
}