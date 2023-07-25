namespace GameMain
{
    public class ProjectileTable : SOTableList<ProjectileProp>
    {
        public override int StartId => IdUtility.ProjectileStartId();
    }
}